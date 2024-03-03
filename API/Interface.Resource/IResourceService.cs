using System.Collections.Generic;
using System.Threading.Tasks;

namespace InquestSpider.Interface.Resource
{
    public interface IResourceService
    {
        Task<IAsyncEnumerable<Models.Resource>> GetAll(ISettings settings);
        Task<Models.Resource> GetByUrl(ISettings settings, string url);
        Task<Models.Resource> Save(ISettings settings, Models.Resource resource);
    }
}
