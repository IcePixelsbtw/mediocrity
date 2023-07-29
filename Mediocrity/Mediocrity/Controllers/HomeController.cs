using Mediocrity.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using FirebaseAdmin.Auth;
using FireSharp.Interfaces;
using FireSharp.Response;

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
        public IActionResult SelectStack(User user)
        {
            // LOG IN
            
            string safeEmail = DatabaseManager.safeEmail(user.Email);

            IFirebaseClient client = DatabaseManager.establishDataBaseConnection();
            
            FirebaseResponse result = client.Get(@"Users/" + safeEmail);
            User ResultUser = result.ResultAs<User>();

            User CurrentUser = new User()
            {
                Email = user.Email,
                Password = user.Password
            };

            if (DatabaseManager.isEqual(ResultUser, CurrentUser))
            {
                //If login succeeded
                return View("Index");
            }
            else
            {
                return View("Auth");
            }

            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}