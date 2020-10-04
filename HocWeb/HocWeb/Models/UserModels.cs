using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HocWeb.Models
{
    [BsonIgnoreExtraElements]
    public class UserModels
    {
        [BsonId]
    
        public ObjectId UserID { get; set; }


        [BsonElement("UserName")]
        public string UserName { get; set; }

        [BsonElement("Passwords")]
        public string Passwords { get; set; }
        [BsonElement("FirstName")]
        public string FirstName { get; set; }
        [BsonElement("LastName")]
        public string LastName { get; set; }
        [BsonElement("Position")]
        public string Position { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; }
        [BsonElement("Phone")]
        public string Phone { get; set; }
        [BsonElement("CMND")]  
       public string CMND { get; set; }

        [BsonElement("Address")]
        public string Address { get; set; }

        [BsonElement("CreatedDate")]
        public DateTime? CreatedDate { get; set; }

        [BsonElement("CreatedBy")]
        public string CreatedBy { get; set; }

        [BsonElement("ModifiedDate")]
        public DateTime? ModifiedDate { get; set; }

        [BsonElement("ModifiedBy")]
        public string ModifiedBy { get; set; }
        [BsonElement("CodeChangePass")]
        public string CodeChangePass { get; set; }

        [BsonElement("Status")]
        public bool Status { get; set; }

    }
}