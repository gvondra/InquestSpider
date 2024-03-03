using Microsoft.Extensions.Options;

namespace ResourceRPC
{
    public class SettingsFactory
    {
        private readonly IOptions<Settings> _options;

        public SettingsFactory(IOptions<Settings> options)
        {
            _options = options;
        }

        public CoreSettings CreateCore() => new CoreSettings();
    }
}
