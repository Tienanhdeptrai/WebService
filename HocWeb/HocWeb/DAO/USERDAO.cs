using System;
using System.Collections.Generic;
using System.Linq;
using HocWeb.App_Start;
using MongoDB.Driver;
using HocWeb.Models;
using MongoDB.Bson;

namespace HocWeb.DAO
{
    public class USERDAO
    {
        private MongoDBContext dBContext;
        private IMongoCollection<UserModels> UserCollection;

        public USERDAO()
        {
            dBContext = new MongoDBContext();
            UserCollection = dBContext.database.GetCollection<UserModels>("User");
            
        }
        public UserModels GetByID(string userName)
        {
            return UserCollection.AsQueryable<UserModels>().SingleOrDefault(x => x.UserName == userName);
        }
        public int Login(string userName, string password)
        {

            var result = UserCollection.AsQueryable<UserModels>().SingleOrDefault(x => x.UserName == userName);

            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.Status == false)
                {
                    return -1;
                }
                else
                {
                    if (result.Passwords == password)
                    {
                        if (result.Position == "2" || result.Position == "3")
                        {
                            return 2;
                        }
                        else
                        {
                            return 1;
                        }
                    }
                    else
                        return -2;
                }
            }

        }

        public UserModels GetByEmail(string Email)
        {
            return UserCollection.AsQueryable<UserModels>().SingleOrDefault(x => x.Email == Email);
        }

        public bool CheckUserName(string username)
        {
            return UserCollection.AsQueryable<UserModels>().Count(x => x.UserName == username) > 0;
        }
        public bool CheckUserEmail(string email)
        {
            return UserCollection.AsQueryable<UserModels>().Count(x => x.Email == email) > 0;
        }

        public int ChangePass(string id, string newpass)
        {
            var result = UserCollection.AsQueryable().SingleOrDefault(x => x.UserID.ToString() == id);

            if (result.Passwords == newpass)
            {
                return 1;
            }
            else
            {
                var filter = Builders<UserModels>.Filter.Eq("_id", ObjectId.Parse(id));
                var update = Builders<UserModels>.Update
                .Set("Passwords", newpass);
                UserCollection.UpdateOne(filter, update);
                return 2;
            }

        }
        public int doimatkhau(string id, string newpass, string code)
        {
            var result = UserCollection.AsQueryable().SingleOrDefault(x => x.UserID.ToString() == id);
            if (result.CodeChangePass == code)
            {
                if (result.Passwords == newpass)
                {
                    return 1;
                }
                else
                {
                    var filter = Builders<UserModels>.Filter.Eq("_id", ObjectId.Parse(id));
                    var update = Builders<UserModels>.Update
                    .Set("Passwords", newpass);
                    UserCollection.UpdateOne(filter, update);
                    return 2;
                }
            }
            else
            {
                return 3;
            }
        }
        public List<UserModels> ListAllPaging()
        {
            List<UserModels> model = UserCollection.AsQueryable<UserModels>().ToList();
            return model;
        }
        public IEnumerable<UserModels> GetAll()
        {
            return UserCollection.Find(x => true).ToList();
    
        }
        public IEnumerable<UserModels> GetUser(string gmail)
        {
            return UserCollection.Find(x => x.Email == gmail).ToList();
        }
        public UserModels ViewDetail(string  id)
        {
             var Uid = new ObjectId(id);
             return UserCollection.AsQueryable<UserModels>().SingleOrDefault(x => x.UserID == Uid);
        }
        public bool Insert(UserModels models)
        {
            try
            {
                UserCollection.InsertOne(models);    
            }
            catch { }
            if(CheckUserName(models.UserName)==true)
            {
                return true;
            }
            return false;
        }
        public bool UpdateDetail(UserModels models)
        {
            try
            {
                var filter = Builders<UserModels>.Filter.Eq("_id", ObjectId.Parse(models.UserID.ToString()));
                var update = Builders<UserModels>.Update
                    .Set("FirstName", models.FirstName)
                    .Set("LastName", models.LastName)
                    .Set("Email", models.Email)
                    .Set("Phone", models.Phone)
                    .Set("Address", models.Address)
                    .Set("CMND", models.CMND)
                    .Set("Position", models.Position)
                    .Set("ModifiedBy", models.ModifiedBy)
                    .Set("ModifiedDate", DateTime.Now);              
                var result = UserCollection.UpdateOne(filter, update);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Update(UserModels models, string id)
        {
            try
            {
                var filter = Builders<UserModels>.Filter.Eq("_id", ObjectId.Parse(id));
                var update = Builders<UserModels>.Update
                    .Set("UserName", models.UserName)
                    .Set("Passwords", models.Passwords)
                    .Set("FirstName", models.FirstName)
                    .Set("LastName", models.LastName)
                    .Set("Email", models.Email)
                    .Set("Phone", models.Phone)
                    .Set("Address", models.Address)
                    .Set("CMND", models.CMND)
                    .Set("Position", models.Position)
                    .Set("ModifiedBy", models.ModifiedBy)
                    .Set("ModifiedDate", DateTime.Now)
                    .Set("CodeChangePass", models.CodeChangePass);
                var result = UserCollection.UpdateOne(filter, update);
                return true;
            }
            catch
            {
                return false;
            }

        }
        public bool Delete(string id)
        {
            try
            {
                UserCollection.DeleteOne(Builders<UserModels>.Filter.Eq("_id", ObjectId.Parse(id)));             
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool ChangeStatus(string  id)
        {
            var _Uid = new ObjectId(id);
            var user = UserCollection.AsQueryable<UserModels>().SingleOrDefault(x => x.UserID == _Uid);
            var filter = Builders<UserModels>.Filter.Eq("_id", ObjectId.Parse(id));
            var update = Builders<UserModels>.Update
               .Set("Status", !user.Status);
            var result = UserCollection.UpdateOne(filter, update);
            return !user.Status;
        }
     
    }
}
