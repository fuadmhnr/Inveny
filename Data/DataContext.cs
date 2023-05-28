using Inveny.Models;
using Microsoft.EntityFrameworkCore;

namespace Inveny.Data;

public class DataContext : DbContext
{
  public DataContext(DbContextOptions<DataContext> options) : base(options)
  {}

  public DbSet<Supplier> Suppliers { get; set; }
  public DbSet<Inventory> Inventories { get; set; }
}