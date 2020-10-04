using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HocWeb.Models
{
    [BsonIgnoreExtraElements]
    public class OrderDetailModels
    {
        [BsonId]

        public ObjectId OrderDetailID { get; set; }


        [BsonElement("ProductID")]
        public ObjectId ProductID { get; set; }

        [BsonElement("OrderID")]
        public ObjectId OrderID { get; set; }

        [BsonElement("Quantity")]
        public string Quantity { get; set; }

        [BsonElement("Price")]
        public string Price { get; set; }
    }
}