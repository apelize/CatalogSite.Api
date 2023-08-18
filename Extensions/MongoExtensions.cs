using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Settings;
using Entities;
using Repositories;

namespace Extensions;

public static partial class Extensions
{
    public static IServiceCollection AddMongo(this IServiceCollection services)
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));

        services.AddSingleton(serviceProvider =>
        {
            var configuration = serviceProvider.GetService<IConfiguration>();
            var mongoDBSettings = configuration!.GetSection(nameof(MongoDBSettings)).Get<MongoDBSettings>();
            var mongoClient = new MongoClient(mongoDBSettings!.ConnectionString);

            return mongoClient.GetDatabase("Products");
        });

        return services;
    }
    public static IServiceCollection AddMongoRepository<T>(this IServiceCollection services, string collectionName) where T : Product
    {
        services.AddSingleton<IRepository<T>>(serviceProvider =>
        {
            var database = serviceProvider.GetService<IMongoDatabase>();
            return new MongoRepository<T>(database!, collectionName);
        });
        return services;
    }
}