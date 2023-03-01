using System;
using System.ComponentModel.DataAnnotations;
using MoonlightShadow.ViewModels;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace MoonlightShadow.Models
{
    public class Contact
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        public string Name { get; set; }

        public string Email { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }

        public Contact(ContactViewModel contactForm)
        {
            Name = contactForm.Name;

            Email = contactForm.Email;

            Subject = contactForm.Subject;
            
            Message = contactForm.Message;
        }
    }
}