using BrassLoon.DataClient.MongoDB;
using InquestSpider.Resource.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InquestSpider.Resource.Data
{
    public interface IResourceDataService
    {
        Task<IEnumerable<ResourceData>> GetAll(ISettings settings);
        Task<IEnumerable<ResourceData>> GetByHash(ISettings settings, byte[] hash);
        Task Save(ISettings settings, ResourceData data);
    }
}
