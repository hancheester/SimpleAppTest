using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portal.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Portal.Pages.Account;

[AllowAnonymous]
public class LoginModel : PageModel
{
    private readonly IConfiguration _configuration;

    [TempData]
    public string? ErrorMessage { get; set; }
    public string? ReturnUrl { get; set; }
    [BindProperty, Required]
    public string? Username { get; set; }
    [BindProperty, DataType(DataType.Password)]
    public string? Password { get; set; }

    public LoginModel(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void OnGet(string? returnUrl = null)
    {
        if (!string.IsNullOrEmpty(ErrorMessage))
        {
            ModelState.AddModelError(string.Empty, ErrorMessage);
        }

        returnUrl = returnUrl ?? Url.Content("~/");

        ReturnUrl = returnUrl;
    }

    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
        returnUrl = returnUrl ?? Url.Content("~/");

        if (ModelState.IsValid)
        {
            var user = _configuration.GetSection("User").Get<UserViewModel>();
            var verificationResult = Username == user.Username && VerifyPassword(Password!, user.Password!, user.Salt!);

            if (verificationResult)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, Username!)
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                return Redirect(returnUrl);
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        }

        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        
        // If we got this far, something failed, redisplay form
        return Page();
    }

    private bool VerifyPassword(string password, string hash, string salt)
    {
        const int keySize = 64;
        const int iterations = 350000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        var hashToVerify = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            Convert.FromHexString(salt),
            iterations,
            hashAlgorithm,
            keySize);

        return CryptographicOperations.FixedTimeEquals(hashToVerify, Convert.FromHexString(hash));
    }
}
