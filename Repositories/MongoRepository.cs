using Entities;
using MongoDB.Driver;

namespace Repositories;

public class MongoRepository<T> : IRepository<T> where T : Product
{
    private readonly IMongoCollection<T> _dbCollection;
    private readonly FilterDefinitionBuilder<T> _filterBuilder = Builders<T>.Filter;
    public MongoRepository(IMongoDatabase database, string collectionName)
    {
        _dbCollection = database.GetCollection<T>(collectionName);
    }
    public async Task CreateAsync(T entity)
    {
        await _dbCollection.InsertOneAsync(entity);
    }

    public async Task DeleteAsync(Guid Id)
    {
        FilterDefinition<T> filter = _filterBuilder.Eq(item => item._id, Id);
        await _dbCollection.DeleteOneAsync(filter);
    }

    public async Task<T> GetAsync(Guid Id)
    {
        FilterDefinition<T> filter = _filterBuilder.Eq(item => item._id, Id);
        return await _dbCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbCollection.Find(_filterBuilder.Empty).ToListAsync();
    }
}