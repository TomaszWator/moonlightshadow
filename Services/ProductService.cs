using MoonlightShadow.Models;
using MongoDB.Driver;
using System.Linq;
using System.Collections.Generic;
using MongoDB.Bson;
using MoonlightShadow.Models.Products;
using MoonlightShadow.Models.Transaction;

namespace MoonlightShadow.Services
{
    public class ProductService
    {
        private readonly IMongoCollection<Camera> _Camera;
        private readonly IMongoCollection<Phone> _Phone;
        private readonly IMongoCollection<Laptop> _Laptop;
        private readonly IMongoCollection<Game> _Game;

        public ProductService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _Camera = database.GetCollection<Camera>(settings.CameraCollectionName);
            _Phone = database.GetCollection<Phone>(settings.PhoneCollectionName);
            _Laptop = database.GetCollection<Laptop>(settings.LaptopCollectionName);
            _Game = database.GetCollection<Game>(settings.GameCollectionName);
        }

        public Product GetBy(string category, string id)
        {
            if (category == "Camera")
                return _Camera.Find(product => product.Id == id).FirstOrDefault() as Product;

            if (category == "Phone")
                return _Phone.Find(product => product.Id == id).FirstOrDefault() as Product;

            if (category == "Laptop")
                return _Laptop.Find(product => product.Id == id).FirstOrDefault() as Product;

            if (category == "Game")
                return _Game.Find(product => product.Id == id).FirstOrDefault() as Product;

            return null;
        }

        public Product GetBy(OrderItem orderItem)
        {
            if (orderItem.Category == "Camera")
                return _Camera.Find(product => product.Id == orderItem.Id).FirstOrDefault() as Product;

            if (orderItem.Category == "Phone")
                return _Phone.Find(product => product.Id == orderItem.Id).FirstOrDefault() as Product;

            if (orderItem.Category == "Laptop")
                return _Laptop.Find(product => product.Id == orderItem.Id).FirstOrDefault() as Product;

            if (orderItem.Category == "Game")
                return _Game.Find(product => product.Id == orderItem.Id).FirstOrDefault() as Product;

            return null;
        }

        public List<Product> GetBy(BoughtOrder boughtOrder)
        {
            var products = new List<Product>();

            if(boughtOrder.ProductItems == null) return products;
            
            foreach (var productItem in boughtOrder.ProductItems)
            {
                var product = this.GetBy(productItem.Category, productItem.Id);
                
                if(product != null) products.Add(product);
            }

            return products;
        }

        public List<Product> GetBy(Order order)
        {
            var products = new List<Product>();

            if(order.Items == null) return products;
            
            foreach (var orderItem in order.Items)
            {
                var product = this.GetBy(orderItem.Category, orderItem.Id);
                
                if(product != null) products.Add(product);
            }

            return products;
        }

        public void Update(Product productIn)
        {
            if(productIn.Category == "Camera")
            {
                var cameraIn = _Camera.Find(product => product.Id == productIn.Id).FirstOrDefault();

                (cameraIn as Product).Update(productIn);

                _Camera.ReplaceOne(camera => camera.Id == cameraIn.Id, cameraIn);
            }

            else if(productIn.Category == "Phone")
            {
                var phoneIn = _Phone.Find(product => product.Id == productIn.Id).FirstOrDefault();

                (phoneIn as Product).Update(productIn);

                _Phone.ReplaceOne(phone => phone.Id == phoneIn.Id, phoneIn);
            }

            else if(productIn.Category == "Laptop")
            {
                var laptopIn = _Laptop.Find(product => product.Id == productIn.Id).FirstOrDefault();

                (laptopIn as Product).Update(productIn);

                _Laptop.ReplaceOne(laptop => laptop.Id == laptopIn.Id, laptopIn);
            }

            else if(productIn.Category == "Game")
            {
                var gameIn = _Game.Find(product => product.Id == productIn.Id).FirstOrDefault();

                (gameIn as Product).Update(productIn);

                _Game.ReplaceOne(game => game.Id == gameIn.Id, gameIn);
            }
        }

        public void Update(List<Product> products)
        {
            foreach (var product in products)
            {
                Update(product);
            }
        }

        // public void Update(string propertyName, product product)
        // {
        //     var filter = Builders<BsonDocument>.Filter.Eq("Id", product.Id);
        //     var update = Builders<BsonDocument>.Update.Set("Follow")
        //     if (product.Category == "Camera")
        //         _Camera.UpdateOne(camera => camera.Id == product.Id,);

        //     if (product.Category == "Camera")

        //     if (product.Category == "Camera")

        //     if (product.Category == "Camera")
        // }

        // public void Update<T>(string propertyName, T propertyIn, product productIn)
        // {
        //     var filter = Builders<BsonDocument>.Filter.Eq("Id", product.Id);
        //     var update = Builders<BsonDocument>.Update.Set("Follow")
        //     if (product.Category == "Camera")
        //         _Camera.UpdateOne(camera => camera.Id == product.Id,);

        //     if (product.Category == "Camera")

        //     if (product.Category == "Camera")

        //     if (product.Category == "Camera")
        // }
    }

}