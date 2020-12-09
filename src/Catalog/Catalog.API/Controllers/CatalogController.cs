using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.API.Entities;
using Catalog.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Catalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private IProductRepository _repo;
        private ILogger<CatalogController> _logger;
        public CatalogController(IProductRepository repo, ILogger<CatalogController> logger)
        {
            _repo = repo;
            _logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery]int? count)
        {
            if (count != null)
            {
                var countedProducts = await _repo.GetProductsWithCount(count.Value);
                return Ok(countedProducts);
            }
            var products = await _repo.GetProducts();
            return Ok(products);
        }
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var product = await _repo.GetProductById(id);
            if (product == null)
            {
                _logger.LogError($"Product witd id {id}, not found.");
                return NotFound();
            }
            return Ok(product);
        }
        [Route("[action]/{category}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category)
        {
            var products = await _repo.GetProductByCategory(category);
            return Ok(products);
        }
        /*
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByName(string name)
        {
            var products = await _repo.GetProductByName(name);
            return Ok(products);
        }
        */
        [HttpPost]
        public async Task<ActionResult> AddProduct([FromBody]Product product)
        {
            await _repo.AddProduct(product);
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> UpdateProduct([FromBody]Product product)
        {
            return Ok(await _repo.UpdateProduct(product));
        }
        [HttpDelete("{id:length(24)}")]
        public async Task<ActionResult> DeleteProduct(string id)
        {
            return Ok(await _repo.DeleteProduct(id));
        }
    }
}