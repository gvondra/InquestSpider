using BrassLoon.DataClient.MongoDB;
using InquestSpider.Resource.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InquestSpider.Resource.Data
{
    public interface IResourceExclusionDataService
    {
        Task<IEnumerable<ResourceExclusionData>> GetAll(ISettings settings);
        Task Save(ISettings settings, ResourceExclusionData data);
        Task Delete(ISettings settings, string id);
    }
}
