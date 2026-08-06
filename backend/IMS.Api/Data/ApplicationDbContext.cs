using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using IMS.Api.Models;

namespace IMS.Api.Data
{
    //This is the bridge between your models and SQL Server.
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
    }

}
