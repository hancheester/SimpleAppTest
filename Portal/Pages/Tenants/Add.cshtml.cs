using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portal.DataAccess;
using Portal.Models;

namespace Portal.Pages.Tenants;

public class AddModel : PageModel
{
    private readonly ApplicationDbContext _context;

    [BindProperty]
    public AddOrganizationViewModel Organization { get; set; } = new();

    public AddModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Tenants.Add(new Entities.Tenant
        {
            Identifier = Guid.NewGuid(),
            Name = Organization.Name,
        });
        _context.SaveChanges();

        TempData["Message"] = $"Organization {Organization.Name} added successfully.";

        return RedirectToPage("Index");
    }
}
