using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System;
using MoonlightShadow.Models.ClassHelper;
using System.ComponentModel.DataAnnotations;

namespace MoonlightShadow.Models
{
    public class Token
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Display(Name = "Nazwa: ")]
        public string name { get; set; }

        [Display(Name = "Wartość: ")]
        public string value { get; set; }  

        [Display(Name = "Email ")]
        public string email { get; set; }  
    }
}