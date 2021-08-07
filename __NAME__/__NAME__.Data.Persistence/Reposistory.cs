
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;
using MongoDB.Driver;
using __NAME__.ServiceBus;


namespace __NAME__.Data.Persistence
{
    public static class Reposistory
    {
        public static async Task InsertOneAsync<T>(this IMongoCollection<T> mongoCollection, T command, ITransitBus transitBus = null) where T : class
        {
            await mongoCollection.InsertOneAsync(command);
        }
        public static async Task InsertOrReplaceOneAsync<T>(this IMongoCollection<T> mongoCollection, T command, Expression<Func<T, bool>> predicate, ITransitBus transitBus = null) where T : class
        {
            if ((mongoCollection.AsQueryable().Where(predicate).Any()))
            {
                await mongoCollection.ReplaceOneAsync(predicate, command);
            }
            else
            {
                await mongoCollection.InsertOneAsync(command);
            }
            if (transitBus != null)
            {
                await transitBus.Publish<T>(command);
            }
        }

    }
}
