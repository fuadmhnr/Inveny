using System.ComponentModel.DataAnnotations;

namespace Inveny.Requests;

public class SupplierRequest
{
  [Required]
  [StringLength(75, ErrorMessage = "Name cannot be longer than 50 characters")]

  public string Name { get; set; } = string.Empty;
  [Required]
  [StringLength(14, ErrorMessage = "Phone number cannot be longer than 50 characters")]
  public string Phone { get; set; } = string.Empty;
}