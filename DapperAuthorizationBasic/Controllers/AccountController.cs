using BusinessLogic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using System;
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
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User user)
        {
            user.Password = Hash(user.Password);
            this.userRepository.Save(user);
            return RedirectToAction("Account", "Login");
        }

        private string Hash(string password)
        {
            byte[] salt = Convert.FromBase64String("234897238479283439823748");  //random  
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));
            return hashed;
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            email = email.Trim();
            var user = userRepository.GetByEmail(email);
            
            if (user != null && user.Password == Hash(password))
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, user.Role));
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                
                
                //identity.AddClaim(new Claim(ClaimTypes.GivenName, user.FirstName));
                //identity.AddClaim(new Claim(ClaimTypes.Surname, user.LastName));

                //foreach (var role in user.Roles)
                //{
                //    identity.AddClaim(new Claim(ClaimTypes.Role, role.Role));
                //}

                return RedirectToAction("Auth", "Home");
            }

            ViewData["Error"] = "Invalid credentials please try again";
            return View();
        }

    }

    
}
