using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace NewsBlogAPI.Data.Services
{
    public class DataContext : DbContext
    {
        public DbSet<News> News { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
