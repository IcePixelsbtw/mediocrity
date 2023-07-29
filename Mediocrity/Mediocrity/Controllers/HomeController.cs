using Mediocrity.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Mediocrity.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        static private User tempUser;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Auth()
        {
            return View();
        }
        
        public IActionResult Reg()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Registrate(User a)
        {
            tempUser = a;
            string[] b = { "Test Tech1", "Test Tech2" };
            DatabaseManager.createNewUser("" + tempUser.FirstName, "" + tempUser.LastName, "" + tempUser.Password, "" + tempUser.Email, b);

            return View("Auth");
        }

        [HttpPost]
        public IActionResult SelectStack()
        {
            
            return View("Auth");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}