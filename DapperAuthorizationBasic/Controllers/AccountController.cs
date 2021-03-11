using BusinessLogic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DapperAuthorizationBasic.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository userRepository;

        public AccountController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            var user = userRepository.GetByEmail(email);

            if (user.Password == password)
            {
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                //identity.AddClaim(new Claim(ClaimTypes.Name, user.Ssn));
                //identity.AddClaim(new Claim(ClaimTypes.GivenName, user.FirstName));
                //identity.AddClaim(new Claim(ClaimTypes.Surname, user.LastName));

                //foreach (var role in user.Roles)
                //{
                //    identity.AddClaim(new Claim(ClaimTypes.Role, role.Role));
                //}

                return RedirectToAction("Auth", "Home");
            }

            ViewBag["Error"] = "Invalid credentials please try again";
            return View();
        }

    }

    
}
