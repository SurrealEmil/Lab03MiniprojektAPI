﻿using Lab03MiniprojektAPI.Data;
using Lab03MiniprojektAPI.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Lab03MiniprojektAPI.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public virtual ICollection<Interest> Interests { get; set; }
        public virtual ICollection<WebsiteLink> WebsiteLinks { get; set; }
 
    }
}
