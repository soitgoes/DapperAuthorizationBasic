using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperAuthorizationBasic.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Content("From HomeController");
        }

        [Authorize]
        public IActionResult Auth()
        {
            return Content("I'm auth'd");
        }

        [Authorize(Roles ="Admin")]
        public IActionResult AdminsOnly()
        {
            var result = User.IsInRole("Admin");
            return Content("I'm Admin");
        }
    }
}
