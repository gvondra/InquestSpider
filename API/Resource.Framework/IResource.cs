using InquestSpider.CommonCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InquestSpider.Resource.Framework
{
    public interface IResource
    {
        string ResourceId { get; }
        string Url { get; }
        string Status { get; set; }
        byte[] Hash { get; }
        Dictionary<string, string> Headers { get; set; }
        DateTime CreateTimestamp { get; }
        DateTime UpdateTimestamp { get; }

        Task Save(ISettings settings);
    }
}
