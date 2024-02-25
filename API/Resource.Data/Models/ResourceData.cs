using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace InquestSpider.Resource.Data.Models
{
    public class ResourceData
    {
        [BsonId]
        public string ResourceId { get; set; }
        [BsonRequired]
        public string Url { get; set; }
        [BsonDefaultValue("")]
        public string Status { get; set; }
        [BsonRequired]
        public byte[] Hash { get; set; }
        [BsonIgnoreIfNull]
        public Dictionary<string, string> Headers { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreateTimestamp { get; set; } = DateTime.UtcNow;
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime UpdateTimestamp { get; set; } = DateTime.UtcNow;
    }
}
