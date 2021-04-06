using System;
using Bosch.API.Model.Domain;
using MongoDB.Driver;

namespace Bosch.API.Data.Interface
{
    public interface IMongoDbDataContext
    {
        IMongoCollection<Product> Product { get; }
    }
}
