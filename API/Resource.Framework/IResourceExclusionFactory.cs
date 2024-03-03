using InquestSpider.CommonCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InquestSpider.Resource.Framework
{
    public interface IResourceExclusionFactory
    {
        Task<IEnumerable<string>> GetAll(ISettings settings);
    }
}
