using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HocWeb.Models
{
 
        [BsonIgnoreExtraElements]
        public class ProductModels
        {
            [BsonId]

            public ObjectId ProductID { get; set; }


            [BsonElement("Name")]
            public string Name { get; set; }

            [BsonElement("Code")]
            public string Code { get; set; }
            [BsonElement("MetaTitle")]
            public string MetaTitle { get; set; }
            [BsonElement("Description")]
            public string Description { get; set; }
            [BsonElement("Image")]
            public string Image { get; set; }
            [BsonElement("Price")]
            public string Price { get; set; }
            [BsonElement("PromotionPrice")]
            public string PromotionPrice { get; set; }
            [BsonElement("BrandID")]
            public string BrandID { get; set; }

            [BsonElement("CategoryID")]
            public string CategoryID { get; set; }
            [BsonElement("Warranty")]
            public string Warranty { get; set; }

            [BsonElement("CreatedDate")]
            public DateTime? CreatedDate { get; set; }

            [BsonElement("CreatedBy")]
            public string CreatedBy { get; set; }

        [BsonElement("ModifiedDate")]
        public DateTime? ModifiedDate { get; set; }

        [BsonElement("ModifiedBy")]
        public string ModifiedBy { get; set; }
        [BsonElement("MetaDescription")]
            public string MetaDescription { get; set; }

            [BsonElement("Status")]
            public bool Status { get; set; }


            [BsonElement("ViewCounts")]
            public string ViewCounts { get; set; }
        }
 
}