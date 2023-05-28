using Inveny.Models;

namespace Inveny.Services;

public interface ISupplierService
{
  IQueryable<Supplier> GetSuppliers();
  Supplier GetSupplier(int id);
  bool IsSupplierExist(int id);
  bool CreateSupplier(Supplier supplier);
  bool UpdateSupplier(Supplier supplier);
  bool DeleteSupplier(Supplier supplier);
  bool Save();
}