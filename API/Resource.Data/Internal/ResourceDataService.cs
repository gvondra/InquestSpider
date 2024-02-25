using BrassLoon.DataClient.MongoDB;
using InquestSpider.Resource.Data.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InquestSpider.Resource.Data.Internal
{
    public class ResourceDataService : IResourceDataService
    {
        private const string _collectionName = "Resource";
        private readonly IDbProvider _dbProvider;

        public ResourceDataService(IDbProvider dbProvider)
        {
            _dbProvider = dbProvider;
        }

        public async Task<IEnumerable<ResourceData>> GetAll(ISettings settings)
        {
            FilterDefinition<ResourceData> filter = Builders<ResourceData>.Filter.Empty;
            return (await (await _dbProvider.GetCollection<ResourceData>(settings, _collectionName)).FindAsync(filter)).ToEnumerable();
        }

        public async Task<IEnumerable<ResourceData>> GetByHash(ISettings settings, byte[] hash)
        {
            FilterDefinition<ResourceData> filter = Builders<ResourceData>.Filter.Eq(r => r.Hash, hash);
            return (await (await _dbProvider.GetCollection<ResourceData>(settings, _collectionName)).FindAsync(filter)).ToEnumerable();
        }

        public Task Save(ISettings settings, ResourceData data)
        {
            return !string.IsNullOrEmpty(data.ResourceId) switch
            {
                true => Update(settings, data),
                _ => Create(settings, data)
            };
        }

        private async Task Update(ISettings settings, ResourceData data)
        {
            FilterDefinition<ResourceData> filter = Builders<ResourceData>.Filter.Eq(r => r.ResourceId, data.ResourceId);
            data.UpdateTimestamp = DateTime.UtcNow;
            await (await _dbProvider.GetCollection<ResourceData>(settings, _collectionName)).ReplaceOneAsync(filter, data);
        }

        private async Task Create(ISettings settings, ResourceData data)
        {
            await (await _dbProvider.GetCollection<ResourceData>(settings, _collectionName)).InsertOneAsync(data);
        }
    }
}
