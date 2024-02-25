using InquestSpider.CommonCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InquestSpider.Resource.Framework
{
    public interface IResourceFactory
    {
        IResource Create(string url);
        Task<IEnumerable<IResource>> GetAll(ISettings settings);
        Task<IResource> GetByUrl(ISettings settings, string url);
    }
}
