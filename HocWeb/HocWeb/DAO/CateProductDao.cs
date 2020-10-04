using System;
using System.Collections.Generic;
using System.Linq;
using HocWeb.App_Start;
using MongoDB.Driver;
using HocWeb.Models;
using MongoDB.Bson;

namespace HocWeb.DAO
{
    public class CateProductDao
    {
        private MongoDBContext dBContext;
        private IMongoCollection<CateProductModels> CateProductCollection;
        public CateProductDao()
        {
            dBContext = new MongoDBContext();
            CateProductCollection = dBContext.database.GetCollection<CateProductModels>("ProductCategory");
        }

        public List<CateProductModels> ListAll()
        {
            List<CateProductModels> model = CateProductCollection.AsQueryable<CateProductModels>().ToList();
            return model;
        }
        public List<CateProductModels> ListAllPaging()
        {
            List<CateProductModels> model = CateProductCollection.AsQueryable<CateProductModels>().ToList();
            return model;
        }
        public bool ChangeStatus(string id)
        {
            var _Uid = new ObjectId(id);
            var user = CateProductCollection.AsQueryable<CateProductModels>().SingleOrDefault(x => x.CateProductID == _Uid);
            var filter = Builders<CateProductModels>.Filter.Eq("_id", ObjectId.Parse(id));
            var update = Builders<CateProductModels>.Update
               .Set("Status", !user.Status);
            var result = CateProductCollection.UpdateOne(filter, update);
            return !user.Status;
        }
        public bool Insert(CateProductModels models)
        {
            try
            {
                CateProductCollection.InsertOne(models);
            }
            catch { }
            if (CheckProduct(models.Name) == true)
            {
                return true;
            }
            return false;
        }
        public CateProductModels ViewDetail(string id)
        {
            var _productid = new ObjectId(id);
            return CateProductCollection.AsQueryable<CateProductModels>().SingleOrDefault(x => x.CateProductID == _productid);
        }
        public bool update(CateProductModels models,string id)
        {
            try
            {
                var filter = Builders<CateProductModels>.Filter.Eq("_id", ObjectId.Parse(id));
                var update = Builders<CateProductModels>.Update
                    .Set("Name", models.Name)
                    .Set("ID", models.ID)
                    .Set("MetaTitle", models.MetaTitle)
                    .Set("MetaDescription", models.MetaDescription)
                    .Set("ModifiedBy", models.ModifiedBy)
                    .Set("ModifiedDate", DateTime.Now)
                    .Set("Status", models.Status);
                var result = CateProductCollection.UpdateOne(filter, update);
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
                CateProductCollection.DeleteOne(Builders<CateProductModels>.Filter.Eq("_id", ObjectId.Parse(id)));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool CheckProduct(string name)
        {
            return CateProductCollection.AsQueryable<CateProductModels>().Count(x => x.Name == name) > 0;
        }
    }
}
