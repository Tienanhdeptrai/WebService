using MongoDB.Driver;
namespace HocWeb.App_Start
{
    public class MongoDBContext
    {
        public IMongoDatabase database;
        public MongoDBContext()   
        {
            var client = new MongoClient("mongodb+srv://myMongoDBUser:Abc123456@cluster0.fkfwh.mongodb.net/DoAn");
            database = client.GetDatabase("DoAn");
        }
    }
}