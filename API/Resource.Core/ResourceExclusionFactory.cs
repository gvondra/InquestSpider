using InquestSpider.CommonCore;
using InquestSpider.Resource.Data;
using InquestSpider.Resource.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InquestSpider.Resource.Core
{
    public class ResourceExclusionFactory : IResourceExclusionFactory
    {
        private readonly IResourceExclusionDataService _dataService;

        public ResourceExclusionFactory(IResourceExclusionDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<IEnumerable<string>> GetAll(ISettings settings)
        {
            return (await _dataService.GetAll(new DataSettings(settings)))
                .Select(e => e.Expression);
        }
    }
}
