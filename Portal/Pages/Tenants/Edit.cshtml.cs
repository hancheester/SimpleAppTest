using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portal.Models;

namespace Portal.Pages.Tenants;

public class EditModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    [BindProperty]
    public EditOrganizationViewModel Organization { get; set; } = new();

    public void OnGet()
    {
        var organization = IndexModel.OrganizationsData.FirstOrDefault(o => o.Id == Id);

        if (organization is null)
        {
            RedirectToPage("Index");
        }

        Organization = new EditOrganizationViewModel
        {
            Id = organization!.Id,
            Name = organization.Name
        };
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var organization = IndexModel.OrganizationsData.FirstOrDefault(o => o.Id == Id);

        if (organization is null)
        {
            RedirectToPage("Index");
        }

        organization!.Name = Organization.Name!;

        TempData["Message"] = $"Organization {organization.Name} updated successfully.";

        return RedirectToPage("Index");
    }
}
