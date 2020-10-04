using System;
using System.Collections.Generic;
using System.Linq;
using HocWeb.App_Start;
using MongoDB.Driver;
using HocWeb.Models;
using MongoDB.Bson;


namespace HocWeb.DAO
{

    public class ProductDao
    {
        private MongoDBContext dBContext;
        private IMongoCollection<ProductModels> ProductCollection;
        private IMongoCollection<BrandModels> BrandCollection;
        private IMongoCollection<CateProductModels> CateProductCollection;

        public ProductDao()
        {
            dBContext = new MongoDBContext();
            ProductCollection = dBContext.database.GetCollection<ProductModels>("Product");
            BrandCollection = dBContext.database.GetCollection<BrandModels>("Brand");
            CateProductCollection= dBContext.database.GetCollection<CateProductModels>("ProductCategory");
        }

        public int CountProduct()
        {
            return ProductCollection.AsQueryable<ProductModels>().Count(x => x.Status==true);
        }
        public ProductModels GetByID(string ID)
        {
            var _productid = new ObjectId(ID);
            return ProductCollection.AsQueryable<ProductModels>().SingleOrDefault(x => x.ProductID == _productid);
        }
        //public List<ProductModels> CategoryProduct(string cateID)
        //{
        //    var model = ProductCollection.AsQueryable<ProductModels>().SingleOrDefault(x => x.CategoryID == cateID); 
        //    return model.OrderBy(x => x).ToList();
        //}
        public List<ProductModels> ListAllProduct()
        {
            List<ProductModels> model = ProductCollection.AsQueryable<ProductModels>().ToList();
            return model;
        }
        //public List<string> ListName(string keyword)
        //{
        //    return db.Products.Where(x => x.Name.Contains(keyword)).Select(x => x.Name).ToList();
        //}
       
        public List<ProductModels> ListAllPaging()
        {
            List<ProductModels> model = ProductCollection.AsQueryable<ProductModels>().ToList();
            return model;
        }
        //public List<ProductModels> ListRelatedProducts(string productId)
        //{
        //    var product = db.Products.Find(productId);
        //    return db.Products.Where(x => x.ID != productId && x.CategoryID == product.CategoryID).ToList();
        //}
        public ProductModels ViewDetail(string id)
        {
            var _productid = new ObjectId(id);
            return ProductCollection.AsQueryable<ProductModels>().SingleOrDefault(x => x.ProductID == _productid);
        }
        public bool update(ProductModels models, string id)
        {
            try
            {
                var filter = Builders<ProductModels>.Filter.Eq("_id", ObjectId.Parse(id));
                var update = Builders<ProductModels>.Update
                    .Set("Name", models.Name)
                    .Set("Code", models.Code)
                    .Set("MetaTitle", models.MetaTitle)
                    .Set("Description", models.Description)
                    .Set("Image", models.Image)
                    .Set("Price", models.Price)
                    .Set("PromotionPrice", models.PromotionPrice)
                    .Set("BrandID", models.BrandID)
                    .Set("CategoryID", models.CategoryID)
                    .Set("Warranty", models.Warranty)
                    .Set("MetaDescription", models.MetaDescription)
                    .Set("ModifiedBy", models.ModifiedBy)
                    .Set("ModifiedDate", DateTime.Now)
                    .Set("Status", models.Status)
                    .Set("ViewCounts", models.ViewCounts);
                var result = ProductCollection.UpdateOne(filter, update);
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
                ProductCollection.DeleteOne(Builders<ProductModels>.Filter.Eq("_id", ObjectId.Parse(id)));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool ChangeStatus(string id)
        {
            var _proId = new ObjectId(id);
            var product = ProductCollection.AsQueryable<ProductModels>().SingleOrDefault(x => x.ProductID == _proId);
            var filter = Builders<ProductModels>.Filter.Eq("_id", ObjectId.Parse(id));
            var update = Builders<ProductModels>.Update
               .Set("Status", !product.Status);
            var result = ProductCollection.UpdateOne(filter, update);
            return !product.Status;
        }
        public bool CheckProduct(string name)
        {
            return ProductCollection.AsQueryable<ProductModels>().Count(x => x.Name == name) > 0;
        }
        public bool Insert(ProductModels models)
        {
            try
            {
                ProductCollection.InsertOne(models);
            }
            catch { }
            if (CheckProduct(models.Name) == true)
            {
                return true;
            }
            return false;
        }
        public List<BrandModels> ListAll()
        {
            List<BrandModels> model = BrandCollection.AsQueryable<BrandModels>().ToList();
            return model;
        }
        public List<ProductModels> GetListProduct()
        {
            List<ProductModels> model = ProductCollection.AsQueryable<ProductModels>().ToList();
            return model;
        }

        public string GetBrand_Name(ProductModels models)
        {
            var brand_models = BrandCollection.AsQueryable<BrandModels>().SingleOrDefault(x => x.ID == models.BrandID);
            return brand_models.Name;
        }
        public string GetCate_Name(ProductModels models)
        {
            var cate_models = CateProductCollection.AsQueryable<CateProductModels>().SingleOrDefault(x => x.ID == models.CategoryID);
            return cate_models.Name;
        }
    }
}
