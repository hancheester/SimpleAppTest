using System.ComponentModel.DataAnnotations;

namespace Portal.Models;

public class AddOrganizationViewModel
{
    [Required]
    public string? Name { get; set; }
}
