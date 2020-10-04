using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace HocWeb.Models
{
    [BsonIgnoreExtraElements]
    public class BrandModels
    {
        [BsonId]

        public ObjectId BrandID { get; set; }


        [BsonElement("ID")]
        public string ID { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }
    }
}