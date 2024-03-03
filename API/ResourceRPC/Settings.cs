using System;

namespace ResourceRPC
{
    public class Settings
    {
        public string LogApiBaseAddress { get; set; }
        public Guid? LoggingDomainId { get; set; }
        public Guid? LoggingClientId { get; set; }
        public string LoggingClientSecret { get; set; }
    }
}
