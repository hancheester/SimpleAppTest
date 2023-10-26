using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Portal.DataAccess;
using Portal.Models;

namespace Portal.Pages.Apps;

public class EditModel : PageModel
{
    private readonly ApplicationDbContext _context;

    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    [BindProperty]
    public EditApplicationViewModel Application { get; set; } = new();

    public EditModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public void OnGet()
    {
        var application = _context.Applications.Find(Id);

        if (application is null)
        {
            TempData["Message"] = "Application not found!";

            RedirectToPage("Index");
        }

        Application = new EditApplicationViewModel
        {
            Id = application!.Id,
            Name = application.Name
        };
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var application = _context.Applications.Find(Id);

        if (application is null)
        {
            TempData["Message"] = "Application not found!";

            RedirectToPage("Index");
        }

        application!.Name = Application.Name!;

        _context.Entry(application).State = EntityState.Modified;
        _context.SaveChanges();

        TempData["Message"] = $"Application {application.Name} updated successfully.";

        return RedirectToPage("Index");
    }
}