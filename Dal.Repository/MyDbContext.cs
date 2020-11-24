using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Dal.Domain.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace Dal.Repository
{
    public class MyDbContext : DbContext
    {
        public static readonly ILoggerFactory MyLoggerFactory =
     LoggerFactory.Create(
          builder =>
            {
                builder.AddConsole();
            }
     );
        public MyDbContext()
        {

        }
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseLoggerFactory(MyLoggerFactory)  //tie-up DbContext with LoggerFactory object
                        .EnableSensitiveDataLogging()
                .UseSqlite($"DataSource=/Users/erdinckara/Projects/DAL/DalDB.db");
        }
    }
}