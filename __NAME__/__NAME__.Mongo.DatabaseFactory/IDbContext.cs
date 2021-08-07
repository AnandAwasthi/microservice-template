
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace __NAME__.Mongo.DatabaseFactory
{
    public interface IDbContext
    {
        IMongoDatabase Get();
        IMongoCollection<T> GetCollection<T>(string collectionName);
    }
}
