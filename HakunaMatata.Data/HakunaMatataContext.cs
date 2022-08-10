using HakunaMatata.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace HakunaMatata.Data
{
    public class HakunaMatataContext : DbContext
    {
        public HakunaMatataContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
        }
    }
}