using Catalog.API.Data;
using Catalog.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Catalog.API.Repositories
{
    public class ProductRepository: IProductRepository
    {
        private readonly ICatalogContex _context;

        public ProductRepository(ICatalogContex contex)
        {
            _context = contex ?? throw new ArgumentNullException(nameof(contex));
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context
                .Products
                .Find(p => true)
                .ToListAsync();
        }

        public async Task<Product> GetProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);

            return await _context
                .Products
                .Find(filter)
                .FirstOrDefaultAsync();
        }


        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.ElemMatch(p => p.Name, name);
            return await _context
                .Products
                .Find(filter)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, categoryName);
            return await _context
                .Products
                .Find(filter)
                .ToListAsync();
        }

        public async Task CreateProduct(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }
        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult = await _context.Products.ReplaceOneAsync(filter: g=> g.Id == product.Id,replacement: product);
            return updateResult.IsAcknowledged 
                && updateResult.ModifiedCount > 0;
        }
        public async Task<bool> DeleteProduct(string id)
        {
            var deleteResults = await _context.Products.DeleteOneAsync(filter: p => p.Id == id);
            return deleteResults.IsAcknowledged 
                && deleteResults.DeletedCount > 0;
        }


    }
}
