using Inveny.Data;
using Inveny.Models;

namespace Inveny.Services;

public class SupplierService : ISupplierService
{
  private readonly DataContext _context;

  public SupplierService(DataContext context)
  {
    _context = context;
  }

  public bool CreateSupplier(Supplier supplier)
  {
    _context.Add(supplier);
    return Save();
  }

  public bool DeleteSupplier(Supplier supplier)
  {
    _context.Remove(supplier);
    return Save();
  }

  public Supplier GetSupplier(int id)
  {
    return _context.Suppliers.Where(x => x.Id == id).FirstOrDefault();
  }

  public IQueryable<Supplier> GetSuppliers()
  {
    return _context.Suppliers.AsQueryable();
  }

  public bool IsSupplierExist(int id)
  {
    return _context.Suppliers.Any(x => x.Id == id);
  }

  public bool Save()
  {
    var saved = _context.SaveChanges();
    return saved > 0;
  }

  public bool UpdateSupplier(Supplier supplier)
  {
    _context.Update(supplier);
    return Save();
  }
}