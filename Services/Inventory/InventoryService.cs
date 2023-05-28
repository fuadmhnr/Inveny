using Inveny.Data;
using Inveny.Models;

namespace Inveny.Services;

public class InventoryService : IInventoryService
{
  private readonly DataContext _context;

  public InventoryService(DataContext context)
  {
    _context = context;
  }

  public bool CreateInventory(Inventory inventory)
  {
    _context.Add(inventory);
    return Save();
  }

  public bool DeleteInventory(Inventory inventory)
  {
    _context.Remove(inventory);
    return Save();
  }

  public IQueryable<Inventory> GetInventories()
  {
    return _context.Inventories.AsQueryable();
  }

  public Inventory GetInventory(int id)
  {
    return _context.Inventories.Where(x => x.Id == id).FirstOrDefault();
  }

  public bool IsInventoryExist(int id)
  {
    return _context.Inventories.Any(x => x.Id == id);
  }

  public bool Save()
  {
    var saved = _context.SaveChanges();
    return saved > 0;
  }

  public bool UpdateInventory(Inventory inventory)
  {
    _context.Update(inventory);
    return Save();

  }
}