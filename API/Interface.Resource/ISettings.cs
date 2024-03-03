using System.Threading.Tasks;

namespace InquestSpider.Interface.Resource
{
    public interface ISettings
    {
        string BaseAddress { get; }

        Task<string> GetToken();
    }
}
