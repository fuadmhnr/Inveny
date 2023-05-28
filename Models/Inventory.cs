using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inveny.Models;

public class Inventory
{
  [Key]
  public int Id { get; set; }

  [Required]
  [StringLength(8, ErrorMessage = "SKU cannot be longer than 8 characters")]
  public string Sku { get; set; } = string.Empty;


  [Required]
  [StringLength(75, ErrorMessage = "Name cannot be longer than 75 characters")]
  public string Name { get; set; } = string.Empty;

  [Required]
  public float CostPrice { get; set; }

  [Required]
  public float SalePrice { get; set; }

  [Required]
  public int ItemQuantity { get; set; }

  public string? ExpiredDate { get; set; }

  [Required]
  public float MarginPercentage { get; set; } = 0;

  public bool IsDeleted { get; set; } = false;

  [ForeignKey("Supplier")]
  public int SupplierId { get; set; }
  public Supplier Supplier { get; set; }
}