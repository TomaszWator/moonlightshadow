using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System;
using MoonlightShadow;
using System.ComponentModel.DataAnnotations;

namespace MoonlightShadow.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Display(Name = "Nazwa produktu")]
        [Required(ErrorMessage = "Wprowadź nazwę produktu")]
        public string Name { get; set; }

        [Display(Name = "Cena")]
        [Required(ErrorMessage = "Wprowadź cene produktu")]
        public decimal Price { get; set; }

        [Display(Name = "Kategoria")]
        public string Category { get; set; }

        [Display(Name = "Marka")]
        [Required(ErrorMessage = "Wprowadź markę produktu")]
        public string Brand { get; set; }

        [Display(Name = "Model")]
        [Required(ErrorMessage = "Wprowadź model produktu")]
        public string Model { get; set; }

        public int RecomendedQuantity { get; set; }

        public int FollowedQuantity { get; set; }

        public int BoughtQuantity { get; set; }

        public string IdUserCreated { get; set; }

        public List<string> ImagesPath { get; set; }
        
        [Display(Name = "Opis")]
        [Required(ErrorMessage = "Wprowadź opis produktu")]
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
            Description = product.Description;
        }
    }
}