using System;
using API.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Menu> Menus { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Menu>()
                .HasMany(c => c.Restaurants)
                .WithOne(e => e.Menu);
        }
    }
}
