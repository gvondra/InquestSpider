using System.Threading.Tasks;

namespace InquestSpider.CommonCore
{
    public class DataSettings : BrassLoon.DataClient.MongoDB.ISettings
    {
        private readonly ISettings _settings;

        public DataSettings(ISettings settings)
        {
            _settings = settings;
        }

        public Task<string> GetConnectionString() => _settings.GetConnectionString();

        public Task<string> GetDatabaseName() => _settings.GetDatabaseName();
    }
}
