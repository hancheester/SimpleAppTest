using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portal.DataAccess;

namespace Portal.Pages.Apps;

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
        var application = _context.Applications.Find(Id);

        if (application is null)
            TempData["Message"] = "Application not found!";

        _context.Applications.Remove(application!);
        _context.SaveChanges();

        TempData["Message"] = "Application deleted successfully.";

        return RedirectToPage("Index");
    }
}
