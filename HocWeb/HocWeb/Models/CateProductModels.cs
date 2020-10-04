using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HocWeb.Models
{
    [BsonIgnoreExtraElements]
    public class CateProductModels
    {
        [BsonId]

        public ObjectId CateProductID { get; set; }


        [BsonElement("ID")]
        public string ID { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("MetaTitle")]
        public string MetaTitle { get; set; }

        [BsonElement("CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [BsonElement("CreatedBy")]
        public string CreatedBy { get; set; }

        [BsonElement("ModifiedDate")]
        public DateTime ModifiedDate { get; set; }

        [BsonElement("ModifiedBy")]
        public string ModifiedBy { get; set; }

        [BsonElement("MetaDescription")]
        public string MetaDescription { get; set; }

        [BsonElement("Status")]
        public bool Status { get; set; }
    }
}