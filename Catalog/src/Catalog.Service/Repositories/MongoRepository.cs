using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Service.Entities;
using MongoDB.Driver;

namespace Catalog.Service.Repositories
{

    public class MongoRepository<T> : IRepository<T> where T : IEntity
    {
        
        private readonly IMongoCollection<T> dbCollection;
        private readonly FilterDefinitionBuilder<T> filterbuilder = Builders<T>.Filter;

        public MongoRepository(IMongoDatabase database, string collectionName)
        {
            dbCollection = database.GetCollection<T>(collectionName);
        }


        public async Task<IReadOnlyCollection<T>> GetAllAsync()
        {
            return await dbCollection.Find(filterbuilder.Empty).ToListAsync();
        }

        public async Task<T> GetAsync(Guid id)
        {
            FilterDefinition<T> filter = filterbuilder.Eq(x => x.Id, id);
            return await dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            await dbCollection.InsertOneAsync(entity);
        }


        public async Task UpdateAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            FilterDefinition<T> filter = filterbuilder.Eq(x => x.Id, entity.Id);
            await dbCollection.ReplaceOneAsync(filter, entity);
        }

        public async Task RemoveAsync(Guid id)
        {
            FilterDefinition<T> filter = filterbuilder.Eq(x => x.Id, id);
            await dbCollection.DeleteOneAsync(filter);
        }


    }

}
