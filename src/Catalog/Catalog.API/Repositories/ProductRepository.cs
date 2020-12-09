using Catalog.API.Data.Interfaces;
using Catalog.API.Entities;
using Catalog.API.Repositories.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private ICatalogContext _context;
        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }
        public async Task AddProduct(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            var deleteResult = await _context.Products.DeleteOneAsync(filter: g => g.Id == id);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            var products = await _context.Products.Find(p => p.Category == categoryName).ToListAsync();
            return products;
        }

        public async Task<Product> GetProductById(string id)
        {
            var product = await _context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
            return product;
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            var products = await _context.Products.Find(p => p.Name.Contains(name)).ToListAsync();
            return products;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products = await _context.Products.Find(p => true).ToListAsync();
            return products;
        }

        public async Task<IEnumerable<Product>> GetProductsWithCount(int count)
        {
            if (count > 3 )
            {
                count = 3;
            }
            var products = await _context.Products.Find(p => true).Limit(count).ToListAsync();
            return products;
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult = await _context.Products.ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
