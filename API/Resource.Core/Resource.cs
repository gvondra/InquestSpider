using InquestSpider.CommonCore;
using InquestSpider.Resource.Data;
using InquestSpider.Resource.Data.Models;
using InquestSpider.Resource.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InquestSpider.Resource.Core
{
    public class Resource : IResource
    {
        private readonly ResourceData _data;
        private readonly IResourceDataService _dataService;

        public Resource(ResourceData data, IResourceDataService dataService)
        {
            _data = data;
            _dataService = dataService;
        }

        public string ResourceId => _data.ResourceId;

        public string Url => _data.Url;

        public string Status { get => _data.Status; set => _data.Status = value; }

        public byte[] Hash => _data.Hash;

        public Dictionary<string, string> Headers { get => _data.Headers; set => _data.Headers = value; }

        public DateTime CreateTimestamp => _data.CreateTimestamp;

        public DateTime UpdateTimestamp => _data.UpdateTimestamp;

        public Task Save(ISettings settings) => _dataService.Save(new DataSettings(settings), _data);
    }
}
