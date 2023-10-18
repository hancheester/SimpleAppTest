using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Portal.Pages.Tenants;

public class DeleteModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }

    public IActionResult OnGetDelete()
    {
        IndexModel.OrganizationsData.RemoveAll(o => o.Id == Id);

        TempData["Message"] = "Organization deleted successfully.";

        return RedirectToPage("Index");
    }
}
