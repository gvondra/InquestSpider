using InquestSpider.CommonCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InquestSpider.Resource.Framework
{
    public interface IResourceExclusionSaver
    {
        Task Save(ISettings settings, IEnumerable<string> exclustions);
    }
}
