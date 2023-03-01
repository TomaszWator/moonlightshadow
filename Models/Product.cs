using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System;
using MoonlightShadow;

namespace MoonlightShadow.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public int RecomendedQuantity { get; set; }

        public int FollowedQuantity { get; set; }

        public int BoughtQuantity { get; set; }

        public string IdUserCreated { get; set; }

        public List<string> ImagePath { get; set; }

        public string Description { get; set; }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public void Update(Product product)
        {
            Name = product.Name;
            Price = product.Price;
            Category = product.Category;
            Brand = product.Brand;
            Model = product.Model;
            RecomendedQuantity = product.RecomendedQuantity;
            FollowedQuantity = product.FollowedQuantity;
            BoughtQuantity = product.BoughtQuantity;
            IdUserCreated = product.IdUserCreated;
            ImagePath = product.ImagePath;
            Description = product.Description;
        }
    }
}