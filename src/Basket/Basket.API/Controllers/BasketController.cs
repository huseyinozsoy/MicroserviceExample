using AutoMapper;
using Basket.API.Entities;
using Basket.API.Repositories.Interfaces;
using EventBusRabbitMQ.Common;
using EventBusRabbitMQ.Events;
using EventBusRabbitMQ.Producer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController: ControllerBase
    {
        private IBasketRepository _repo;
        private ILogger<BasketController> _logger;
        private IMapper _mapper;
        private EventBusRabbitMQProducer _eventBus;
        public BasketController(IBasketRepository repo, ILogger<BasketController> logger, IMapper mapper, EventBusRabbitMQProducer eventBus)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
            _eventBus = eventBus;
        }
        [HttpGet("{username}")]
        public async Task<ActionResult<BasketCart>> GetBasket(string username)
        {
            var basket = await _repo.GetBasket(username);
            return Ok(basket ?? new BasketCart(username));
        }
        [HttpPost]
        public async Task<ActionResult<BasketCart>> UpdateBasket([FromBody]BasketCart basketcart)
        {
            var basket = await _repo.UpdateBasket(basketcart);
            return Ok(basket);
        }
        [HttpDelete("{username}")]
        public async Task<ActionResult> DeleteBasket(string username)
        {
            return Ok(await _repo.DeleteBasket(username));
        }
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            var basket = await _repo.GetBasket(basketCheckout.UserName);
            if (basket == null)
            {
                return BadRequest();
            }
            var basketRemoved = await _repo.DeleteBasket(basket.UserName);
            if (!basketRemoved)
            {
                return BadRequest();
            }
            var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
            eventMessage.RequestId = Guid.NewGuid();
            eventMessage.TotalPrice = basket.TotalPrice;

            try
            {
                _eventBus.PublishBasketCheckout(EventBusConstants.BasketCheckoutQueue, eventMessage);
            }
            catch (Exception)
            {
                _logger.LogError($"eventMessage couldnt send");
                throw;
            }
            return Accepted();
        }
    }
}
