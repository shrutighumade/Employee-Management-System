using Microsoft.EntityFrameworkCore;
using EmployeeAPI.Models;
using Microsoft.Extensions.Options;
namespace EmployeeAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Employee> Employees { get; set; }
}
