using System.Threading.Tasks;

namespace InquestSpider.CommonCore
{
    public interface ISettings
    {
        Task<string> GetConnectionString();
        Task<string> GetDatabaseName();
    }
}
