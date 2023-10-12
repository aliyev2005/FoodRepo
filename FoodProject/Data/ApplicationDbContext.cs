using FoodProject.Model;
using Microsoft.EntityFrameworkCore;

namespace FoodProject.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<UserAdress> Adresses { get; set; }
        public DbSet<Blacklist> Blacklist { get; set; }
    }
}
