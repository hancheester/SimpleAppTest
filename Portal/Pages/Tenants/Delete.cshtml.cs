using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portal.DataAccess;

namespace Portal.Pages.Tenants;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult OnGetDelete()
    {
        var tenant = _context.Tenants.Find(Id);

        if (tenant is null)
            TempData["Message"] = "Organization not found!";

        _context.Tenants.Remove(tenant!);
        _context.SaveChanges();

        TempData["Message"] = "Organization deleted successfully.";

        return RedirectToPage("Index");
    }
}
