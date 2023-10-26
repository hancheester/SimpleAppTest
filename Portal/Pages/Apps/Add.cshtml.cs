using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portal.DataAccess;
using Portal.Models;

namespace Portal.Pages.Apps;

public class AddModel : PageModel
{
    private readonly ApplicationDbContext _context;

    [BindProperty]
    public AddApplicationViewModel Application { get; set; } = new();

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

        _context.Applications.Add(new Entities.Application
        {
            Name = Application.Name,
        });
        _context.SaveChanges();

        TempData["Message"] = $"Application {Application.Name} added successfully.";

        return RedirectToPage("Index");
    }
}
