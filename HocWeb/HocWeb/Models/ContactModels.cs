using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HocWeb.Models
{
    [BsonIgnoreExtraElements]
    public class ContactModels
    {
        [BsonId]

        public ObjectId ContactID { get; set; }

        [BsonElement("Content")]
        public string Content { get; set; }
        [BsonElement("Status")]
        public bool Status { get; set; }
    }
}