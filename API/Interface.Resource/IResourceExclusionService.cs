using System.Collections.Generic;
using System.Threading.Tasks;

namespace InquestSpider.Interface.Resource
{
    public interface IResourceExclusionService
    {
        Task<List<string>> GetAll(ISettings settings);
        Task Save(ISettings settings, IEnumerable<string> exclusions);
    }
}
