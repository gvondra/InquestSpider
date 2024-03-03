using MongoDB.Bson.Serialization.Attributes;
using System;

namespace InquestSpider.Resource.Data.Models
{
    public class ResourceExclusionData
    {
        [BsonId]
        public string ResourceExclusionId { get; set; }
        [BsonRequired]
        public string Expression { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreateTimestamp { get; set; } = DateTime.UtcNow;
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime UpdateTimestamp { get; set; } = DateTime.UtcNow;
    }
}
