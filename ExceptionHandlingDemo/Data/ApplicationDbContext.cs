using ExceptionHandlingDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace ExceptionHandlingDemo.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; } // DbSet for Employee
    }
}
