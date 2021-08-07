
using System;
using System.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace __NAME__.Mongo.DatabaseFactory
{
    public class DbContext : ConfigurationSection, IDbContext
    {
        private static DatabaseConfiguration databaseConfiguration = (DatabaseConfiguration)ConfigurationManager.GetSection("DatabaseConfiguration");
        private IMongoDatabase _database;
        public IMongoDatabase Get()
        {
              return _database != null ? _database : Initialize();
        }
        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return Get().GetCollection<T>(collectionName);
        }

        

        private IMongoDatabase Initialize()
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
            IMongoDatabase database = null;
            try
            {
                if (databaseConfiguration.Name.Length == 0)
                {
                    throw new Exception("Database name not defined in DatabaseConfiguration section of config.");
                }

                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(databaseConfiguration.ConnectionString));
                if (databaseConfiguration.IsSSL)
                {
                    settings.SslSettings = new SslSettings { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };
                }
                var mongoClient = new MongoClient(settings);
                database = mongoClient.GetDatabase(databaseConfiguration.DatabaseName);
                _database = database;
            }
            catch (Exception ex)
            {
                throw new Exception("Can not access to db server.", ex);
            }
            return database;
        }

      
    }
}
