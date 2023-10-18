using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portal.Models;

namespace Portal.Pages.Tenants;

public class AddModel : PageModel
{
    [BindProperty]
    public AddOrganizationViewModel Organization { get; set; } = new();

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var organization = new OrganizationViewModel
        {
            Id = IndexModel.OrganizationsData.Count + 1,
            Identifier = Guid.NewGuid(),
            Name = Organization.Name!
        };

        IndexModel.OrganizationsData.Add(organization);

        TempData["Message"] = $"Organization {organization.Name} added successfully.";

        return RedirectToPage("Index");
    }
}
