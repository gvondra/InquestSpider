using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InquestSpider.Interface.Resource.Models
{
    public class Resource
    {
        public string ResourceId { get; set; }
        public string Url { get; set; }
        public string Status { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public DateTime? CreateTimestamp { get; set; }
        public DateTime? UpdateTimestamp { get; set; }

        internal static Resource Create(Protos.Resource resource)
        {
            Resource result = new Resource
            {
                ResourceId = resource.ResourceId,
                Url = resource.Url,
                Status = resource.Status,
                CreateTimestamp = resource.CreateTimestamp?.ToDateTime(),
                UpdateTimestamp = resource.UpdateTimestamp?.ToDateTime()
            };
            result.Headers = resource.Headers?.ToDictionary(
                    (KeyValuePair<string, string> pair) => pair.Key,
                    (KeyValuePair<string, string> pair) => pair.Value)
                ?? new Dictionary<string, string>();
            return result;
        }

        internal Protos.Resource ToProto()
        {
            Protos.Resource result = new Protos.Resource
            {
                ResourceId = ResourceId ?? string.Empty,
                Url = Url ?? string.Empty,
                Status = Status ?? string.Empty,
                CreateTimestamp = CreateTimestamp.HasValue ? Timestamp.FromDateTime(CreateTimestamp.Value) : null,
                UpdateTimestamp = UpdateTimestamp.HasValue ? Timestamp.FromDateTime(UpdateTimestamp.Value) : null,
            };
            if (Headers != null)
            {
                foreach (KeyValuePair<string, string> pair in Headers)
                {
                    result.Headers.Add(pair.Key, pair.Value);
                }
            }
            return result;
        }
    }
}
