using DemoPOCApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoPOCApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Order> Orders { get; set; }
    }
}
