using BrassLoon.DataClient.MongoDB;
using InquestSpider.Resource.Data.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InquestSpider.Resource.Data.Internal
{
    public class ResourceExclusionDataService : IResourceExclusionDataService
    {
        private const string _collectionName = "ResourceExclusion";
        private readonly IDbProvider _dbProvider;

        public ResourceExclusionDataService(IDbProvider dbProvider)
        {
            _dbProvider = dbProvider;
        }

        public async Task Delete(ISettings settings, string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));
            FilterDefinition<ResourceExclusionData> filter = Builders<ResourceExclusionData>.Filter.Eq(r => r.ResourceExclusionId, id);
            await (await _dbProvider.GetCollection<ResourceExclusionData>(settings, _collectionName)).DeleteOneAsync(filter);
        }

        public async Task<IEnumerable<ResourceExclusionData>> GetAll(ISettings settings)
        {
            FilterDefinition<ResourceExclusionData> filter = Builders<ResourceExclusionData>.Filter.Empty;
            return (await (await _dbProvider.GetCollection<ResourceExclusionData>(settings, _collectionName)).FindAsync(filter)).ToEnumerable();
        }

        public Task Save(ISettings settings, ResourceExclusionData data)
        {
            return !string.IsNullOrEmpty(data.ResourceExclusionId) switch
            {
                true => Update(settings, data),
                _ => Create(settings, data)
            };
        }

        private async Task Update(ISettings settings, ResourceExclusionData data)
        {
            FilterDefinition<ResourceExclusionData> filter = Builders<ResourceExclusionData>.Filter.Eq(r => r.ResourceExclusionId, data.ResourceExclusionId);
            data.UpdateTimestamp = DateTime.UtcNow;
            await (await _dbProvider.GetCollection<ResourceExclusionData>(settings, _collectionName)).ReplaceOneAsync(filter, data);
        }

        private async Task Create(ISettings settings, ResourceExclusionData data)
        {
            await (await _dbProvider.GetCollection<ResourceExclusionData>(settings, _collectionName)).InsertOneAsync(data);
        }
    }
}
