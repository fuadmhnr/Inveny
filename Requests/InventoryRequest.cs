using System.ComponentModel.DataAnnotations;

namespace Inveny.Requests;

public class InventoryRequest
{
  [Required]
  [StringLength(8, ErrorMessage = "SKU cannot be longer than 8 characters")]
  public string Sku { get; set; } = string.Empty;


  [Required]
  [StringLength(75, ErrorMessage = "Name cannot be longer than 75 characters")]
  public string Name { get; set; } = string.Empty;

  [Required]
  public float CostPrice { get; set; }

  [Required]
  public int ItemQuantity { get; set; }

  public string? ExpiredDate { get; set; }

  [Required]
  public float MarginPercentage { get; set; } = 0;
}