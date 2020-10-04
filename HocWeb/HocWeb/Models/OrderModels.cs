using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HocWeb.Models
{
    [BsonIgnoreExtraElements]
    public class OrderModels
    {
        [BsonId]

        public ObjectId OrderID { get; set; }


        [BsonElement("CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [BsonElement("CustomerID")]
        public ObjectId CustomerID { get; set; }
        [BsonElement("ShipName")]
        public string ShipName { get; set; }
        [BsonElement("ShipMoblie")]
        public string ShipMoblie { get; set; }
        [BsonElement("ShipAddress")]
        public string ShipAddress { get; set; }
        [BsonElement("ShipEmail")]
        public string ShipEmail { get; set; }
        [BsonElement("Status")]
        public string Status { get; set; }
    }
}