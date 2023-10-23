using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Portal.DataAccess;
using Portal.Models;

namespace Portal.Pages.Tenants;

public class EditModel : PageModel
{
    private readonly ApplicationDbContext _context;

    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    [BindProperty]
    public EditOrganizationViewModel Organization { get; set; } = new();

    public EditModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public void OnGet()
    {
        var organization = _context.Tenants.Find(Id);

        if (organization is null)
        {
            TempData["Message"] = "Organization not found!";

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

        var organization = _context.Tenants.Find(Id);

        if (organization is null)
        {
            TempData["Message"] = "Organization not found!";

            RedirectToPage("Index");
        }

        organization!.Name = Organization.Name!;

        _context.Entry(organization).State = EntityState.Modified;
        _context.SaveChanges();

        TempData["Message"] = $"Organization {organization.Name} updated successfully.";

        return RedirectToPage("Index");
    }
}
