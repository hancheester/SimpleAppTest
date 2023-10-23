using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Portal.DataAccess;
using Portal.Models;

namespace Portal.Pages.Tenants;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public List<OrganizationViewModel> Organizations { get; set; } = new();

    public IndexModel(ApplicationDbContext context)
    {
        _context = context!;
    }

    public async Task OnGetAsync()
    {
        Organizations = await _context.Tenants.Select(o => new OrganizationViewModel
        {
            Id = o.Id,
            Identifier = o.Identifier,
            Name = o.Name
        })
        .ToListAsync();
    }
}
