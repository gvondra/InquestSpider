using InquestSpider.CommonCore;
using InquestSpider.Resource.Data;
using InquestSpider.Resource.Data.Models;
using InquestSpider.Resource.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InquestSpider.Resource.Core
{
    public class ResourceFactory : IResourceFactory
    {
        private readonly IResourceDataService _dataService;
        private readonly ResourceUrlHasher _urlHasher;

        public ResourceFactory(IResourceDataService dataService, ResourceUrlHasher urlHasher)
        {
            _dataService = dataService;
            _urlHasher = urlHasher;
        }

        public IResource Create(string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));
            return Create(new ResourceData
            {
                Url = url,
                Hash = _urlHasher.HashUrl(url)
            });
        }

        public async Task<IEnumerable<IResource>> GetAll(ISettings settings)
        {
            return (await _dataService.GetAll(new DataSettings(settings)))
                .Select<ResourceData, IResource>(Create);
        }

        public async Task<IResource> GetByUrl(ISettings settings, string url)
        {
            return (await _dataService.GetByHash(new DataSettings(settings), _urlHasher.HashUrl(url)))
                .Select<ResourceData, IResource>(Create)
                .FirstOrDefault(r => string.Equals(url, r.Url, StringComparison.OrdinalIgnoreCase));
        }

        private Resource Create(ResourceData data) => new Resource(data, _dataService);
    }
}
