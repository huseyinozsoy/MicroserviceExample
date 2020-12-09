using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Commands;
using Ordering.Application.Queries;
using Ordering.Application.Responses;

namespace Ordering.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{username}")]
        public async Task<ActionResult<IEnumerable<OrderResponse>>> GetOrderByUserName(string username)
        {
            var query = new GetOrderByUserNameQuery(username);
            var orders = await _mediator.Send(query);
            return Ok(orders);
        }
        [HttpPost]
        public async Task<ActionResult<OrderResponse>> CheckoutOrder([FromBody]CheckoutOrderCommand order)
        {
            var command = await _mediator.Send(order);
            return Ok(command);
        }
    }
}