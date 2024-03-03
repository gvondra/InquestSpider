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
    public class ResourceExclusionSaver : IResourceExclusionSaver
    {
        private readonly IResourceExclusionDataService _dataService;

        public ResourceExclusionSaver(IResourceExclusionDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task Save(ISettings settings, IEnumerable<string> exclustions)
        {
            DataSettings dataSettings = new DataSettings(settings);
            List<ResourceExclusionData> existingExclusion = (await _dataService.GetAll(dataSettings)).ToList();
            foreach (ResourceExclusionData data in existingExclusion.Where(d => !exclustions.Any(e => string.Equals(e, d.Expression, StringComparison.OrdinalIgnoreCase))))
            {
                await _dataService.Delete(dataSettings, data.ResourceExclusionId);
            }
            foreach (string exclusion in exclustions.Where(e => !existingExclusion.Exists(d => string.Equals(e, d.Expression, StringComparison.OrdinalIgnoreCase))))
            {
                ResourceExclusionData data = new ResourceExclusionData
                {
                    Expression = exclusion
                };
                await _dataService.Save(dataSettings, data);
            }
        }
    }
}
