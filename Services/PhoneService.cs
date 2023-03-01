using MoonlightShadow.Models;
using MongoDB.Driver;
using System.Linq;
using System.Collections.Generic;
using MoonlightShadow.Models.Products;

namespace MoonlightShadow.Services
{
    public class PhoneService
    {
        private readonly IMongoCollection<Phone> _Phone;

        public PhoneService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _Phone = database.GetCollection<Phone>(settings.PhoneCollectionName);
        }

        public List<Phone> Get()
        {
            return _Phone.Find(Phone => true).ToList();
        }

        public Phone GetBy(string id)
        {
            return _Phone.Find(Phone => Phone.Id == id).FirstOrDefault();
        }

        public Phone Create(Phone Phone)
        {
            _Phone.InsertOne(Phone);
            return Phone;
        }

        public void Update(string id, Phone PhoneIn) =>
            _Phone.ReplaceOne(Phone => Phone.Id == id, PhoneIn);

        public void Remove(Phone PhoneIn) =>
            _Phone.DeleteOne(Phone => Phone.Id == PhoneIn.Id);

        public void Remove(string id) => 
            _Phone.DeleteOne(Phone => Phone.Id == id);
    }

}