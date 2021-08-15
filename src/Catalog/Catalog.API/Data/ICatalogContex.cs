using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public interface ICatalogContex
    {
        IMongoCollection<Product> Products { get; }
    }
}
