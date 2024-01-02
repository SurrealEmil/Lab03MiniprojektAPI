using Lab03MiniprojektAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Lab03MiniprojektAPI.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<WebsiteLink> WebsiteLinks { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
    }
}
