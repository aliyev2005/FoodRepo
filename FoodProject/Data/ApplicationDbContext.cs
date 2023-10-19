﻿using FoodProject.Model;
using Microsoft.EntityFrameworkCore;

namespace FoodProject.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<UserAdress> Adresses { get; set; }
        public DbSet<Blacklist> Blacklist { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
