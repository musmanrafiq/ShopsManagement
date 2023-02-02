using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SM.WebApp.Models;
using System.Security.Claims;

namespace SM.WebApp.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            // there will be our logic to compare user from database or any other user provider
            try
            {
                // creating list of claims
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, model.Email)
            };
                // claims identitiy 
                var claimIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);
                // creating claims principal object to pass to the singn in method
                var claimsPricipal = new ClaimsPrincipal(claimIdentity);
                // signing in
                await HttpContext.SignInAsync(claimsPricipal);

                return RedirectToAction("Index", "Home");
            }
            catch(Exception exp) {
                return View(model); 
            }
        }
    }
}
