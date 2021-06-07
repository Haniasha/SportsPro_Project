// Authors: Cecilia Santiago, David McDonald, Ehsan Jalali, Hanieh Shahrokhi
// Workshop 5
// Date May 28, 2021

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


// <summary>
// Home Controller for displaying the links to the Management Pages
// </summary>
namespace SportsPro.Controllers
{
    [AllowAnonymous]
   
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("About")]
        public IActionResult About()
        {
            return View();
        }


    }
}