using System;
using System.Collections.Generic;
using System.Linq;
using HocWeb.App_Start;
using MongoDB.Driver;
using HocWeb.Models;
using MongoDB.Bson;


namespace HocWeb.DAO
{
    public class OrderDao
    {
        private MongoDBContext dBContext;
        private IMongoCollection<OrderModels> OrderCollection;
        private IMongoCollection<OrderDetailModels> OrderDetailCollection;
        public OrderDao()
        {
            dBContext = new MongoDBContext();
            OrderCollection = dBContext.database.GetCollection<OrderModels>("Order");
            OrderDetailCollection= dBContext.database.GetCollection<OrderDetailModels>("OrderDetail");
        }
        public List<OrderModels> ListAllPaging()
        {
            List<OrderModels> model = OrderCollection.AsQueryable<OrderModels>().ToList();
            return model;
        }
        public List<OrderModels> GetListOrder()
        {
            List<OrderModels> model = OrderCollection.AsQueryable<OrderModels>().ToList();
            return model;
        }
        public List<OrderDetailModels> GetListDetail()
        {
            List<OrderDetailModels> model = OrderDetailCollection.AsQueryable<OrderDetailModels>().ToList();
            return model;
        }
        public OrderModels ViewDetail(string id)
        {
            var _OrderId = new ObjectId(id);
            return OrderCollection.AsQueryable<OrderModels>().SingleOrDefault(x => x.OrderID == _OrderId);
        }     

        public bool hasOrderDetail(string id)
        {
            var _ProId = new ObjectId(id);
            var result = OrderDetailCollection.AsQueryable<OrderDetailModels>().SingleOrDefault(x => x.ProductID == _ProId);
            if (result == null)
                return false;
            return true;
        }
        public List<OrderDetailModels> GetAll(string id)
        {
            var _OrderId = new ObjectId(id);
            return OrderDetailCollection.AsQueryable<OrderDetailModels>().Where(x => x.OrderID == _OrderId).ToList();
        }
        public bool update(OrderModels models,string id)
        {
            try
            {
                var filter = Builders<OrderModels>.Filter.Eq("_id", ObjectId.Parse(id));
                var update = Builders<OrderModels>.Update
                    .Set("Status", models.Status);
                var result = OrderCollection.UpdateOne(filter, update);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
