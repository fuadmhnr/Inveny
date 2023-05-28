using Inveny.Models;

namespace Inveny.Collections;

public class InventoryCollection
{
  public int Id { get; set; }
  public string Sku { get; set; } = string.Empty;
  public string Name { get; set; } = string.Empty;
  public float CostPrice { get; set; }
  public float SalePrice { get; set; }
  public InventorySupplierCustom Supplier { get; set; }
}

public class InventorySupplierCustom {
  public string Name { get; set; }
  public string Phone { get; set; }
}