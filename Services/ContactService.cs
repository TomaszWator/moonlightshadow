using MoonlightShadow.Models;
using MoonlightShadow.Models.Products;
using MongoDB.Driver;
using System.Linq;
using System.Collections.Generic;

namespace MoonlightShadow.Services
{
    public class ContactService
    {
        private readonly IMongoCollection<Contact> _Contact;

        public ContactService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _Contact = database.GetCollection<Contact>(settings.ContactCollectionName);
        }

        public List<Contact> Get()
        {
            return _Contact.Find(Contact => true).ToList();
        }

        public Contact GetBy(string id)
        {
            return _Contact.Find(Contact => Contact.Id == id).FirstOrDefault();
        }

        public Contact Create(Contact Contact)
        {
            _Contact.InsertOne(Contact);
            return Contact;
        }

        public void Update(string id, Contact ContactIn) =>
            _Contact.ReplaceOne(Contact => Contact.Id == id, ContactIn);

        public void Remove(Contact ContactIn) =>
            _Contact.DeleteOne(Contact => Contact.Id == ContactIn.Id);

        public void Remove(string id) => 
            _Contact.DeleteOne(Contact => Contact.Id == id);
    }

}