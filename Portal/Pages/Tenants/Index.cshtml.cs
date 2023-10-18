using Microsoft.AspNetCore.Mvc.RazorPages;
using Portal.Models;

namespace Portal.Pages.Tenants;

public class IndexModel : PageModel
{
    public List<OrganizationViewModel> Organizations { get; set; } = OrganizationsData;

    public static List<OrganizationViewModel> OrganizationsData = new()
    {
        new OrganizationViewModel
        {
            Id = 1,
            Identifier = Guid.NewGuid(),
            Name = "Acme"
        },
        new OrganizationViewModel
        {
            Id = 2,
            Identifier = Guid.NewGuid(),
            Name = "Contoso"
        }
    };
}

