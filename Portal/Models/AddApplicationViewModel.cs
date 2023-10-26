using System.ComponentModel.DataAnnotations;

namespace Portal.Models;

public class AddApplicationViewModel
{
    [Required]
    public string? Name { get; set; }
}
