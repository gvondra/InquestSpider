using InquestSpider.CommonCore;
using System.Threading.Tasks;

namespace ResourceRPC
{
    public class CoreSettings : ISettings
    {
        public Task<string> GetConnectionString()
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetDatabaseName()
        {
            throw new System.NotImplementedException();
        }
    }
}
