using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Portal.DataAccess;
using Portal.Models;

namespace Portal.Pages.Apps;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public List<ApplicationViewModel> Applications { get; set; } = new();

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task OnGetAsync()
    {
        Applications = await _context.Applications.Select(x => new ApplicationViewModel
        {
            Id = x.Id,
            Name = x.Name
        }).ToListAsync();
    }
}
