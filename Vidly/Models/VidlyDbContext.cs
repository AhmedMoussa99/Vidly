using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class VidlyDbContext : DbContext
    {
        public VidlyDbContext(): base("DefaultConnection")
        {

        }
        public DbSet<Customer> customers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MembershipType> membershipTypes { get; set; }
    }
}