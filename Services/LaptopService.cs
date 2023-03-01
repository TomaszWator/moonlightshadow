using MoonlightShadow.Models;
using MoonlightShadow.Models.Products;
using MongoDB.Driver;
using System.Linq;
using System.Collections.Generic;

namespace MoonlightShadow.Services
{
    public class LaptopService
    {
        private readonly IMongoCollection<Laptop> _Laptop;

        public LaptopService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _Laptop = database.GetCollection<Laptop>(settings.LaptopCollectionName);
        }

        public List<Laptop> Get()
        {
            return _Laptop.Find(Laptop => true).ToList();
        }

        public Laptop GetBy(string id)
        {
            return _Laptop.Find(Laptop => Laptop.Id == id).FirstOrDefault();
        }

        public Laptop Create(Laptop Laptop)
        {
            _Laptop.InsertOne(Laptop);
            return Laptop;
        }

        public void Update(string id, Laptop LaptopIn) =>
            _Laptop.ReplaceOne(Laptop => Laptop.Id == id, LaptopIn);

        public void Remove(Laptop LaptopIn) =>
            _Laptop.DeleteOne(Laptop => Laptop.Id == LaptopIn.Id);

        public void Remove(string id) => 
            _Laptop.DeleteOne(Laptop => Laptop.Id == id);
    }

}