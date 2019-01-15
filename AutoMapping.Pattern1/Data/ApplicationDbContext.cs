using AutoMapping.Pattern1.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoMapping.Pattern1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
