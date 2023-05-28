using Inveny.Models;

namespace Inveny.Services;

public interface IInventoryService
{
  IQueryable<Inventory> GetInventories();
  Inventory GetInventory(int id);
  bool IsInventoryExist(int id);
  bool CreateInventory(Inventory inventory);
  bool UpdateInventory(Inventory inventory);
  bool DeleteInventory(Inventory inventory);
  bool Save();
}