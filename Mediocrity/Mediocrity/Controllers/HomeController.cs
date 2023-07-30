using Mediocrity.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using FirebaseAdmin.Auth;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.Net.Mail;

namespace Mediocrity.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        static private User tempUser;
        private List<string> techsList = new List<string>();
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
            bool creationResult = DatabaseManager.createNewUser("" + tempUser.FirstName, "" + tempUser.LastName, "" + tempUser.Password, "" + tempUser.Email, b);


            if (creationResult)
            {
                string fromMail = "mediocrity.notify@gmail.com";
                string fromPassword = "fssruwuhlvapjpuj";
                MailMessage message = new MailMessage();
                message.From = new MailAddress(fromMail);
                message.Subject = "Успішна реєстрація";
                string recipient = tempUser.Email;
                message.To.Add(new MailAddress(recipient));

                message.Body = "Вас успішно зареєстровано на платформі Mediocrity :)";

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromMail, fromPassword),
                    EnableSsl = true,
                };

                smtpClient.Send(message);
                ViewBag.IsCreated = "1";

                return View("Auth");
            }
            else
            {
                ViewBag.IsCreated = "0";
                return View("Reg");
            }

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
                ViewBag.StaticValue = "Logged";
                StackRepository.setEmail(CurrentUser.Email);
                return View("Main");
            }
            else
            {
                return View("Auth");
            }
        }

        public IActionResult AboutMe()
        {
            ViewBag.TECHS = techsList;
            return View();
        }

        [HttpGet]
        public IActionResult CreateProject()
        {
            return View();
        }
        
        
        [HttpPost]
        public IActionResult CreateProject(Project project)
        {
            Console.WriteLine(project.Title);
            Console.WriteLine(project.Description);
            Console.WriteLine("Кількість учасників:"+project.ParticipantsNumber);
            Console.WriteLine("Цена:" + project.Price);
            Console.WriteLine(project.ContactInfo);
            
            
            string[] stack = { "Test Tech1", "Test Tech2" };
            bool creationResult = DatabaseManager.createNewProject("" + project.Title, "" 
                + project.Description, stack, project.ParticipantsNumber, project.Price,
                ""+project.ContactInfo);


            if (creationResult)
            {
                 string fromMail = "mediocrity.notify@gmail.com";
                 string fromPassword = "fssruwuhlvapjpuj";
                 MailMessage message = new MailMessage();
                 message.From = new MailAddress(fromMail);
                 message.Subject = "Успішно створено новий проєкт";
                 string recipient = StackRepository.UserInfo;
                 message.To.Add(new MailAddress(recipient));
                
                 message.Body = "Вас успішно створили новий проєкт "+project.Title+" на платформі Mediocrity :)";
                
                 var smtpClient = new SmtpClient("smtp.gmail.com")
                 {
                     Port = 587,
                     Credentials = new NetworkCredential(fromMail, fromPassword),
                     EnableSsl = true,
                 };
                
                smtpClient.Send(message);
                ViewBag.IsCreated = "1";

                return View("Index");////////////////////////////////////////////
            }
            else
            {
                ViewBag.IsCreated = "0";
                return View("Reg");/////////////////////////////////////////////
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpPost]
        public IActionResult AddButton(string buttonName)
        {
            if (!string.IsNullOrEmpty(buttonName))
            {
                StackRepository.addStack(buttonName);
                return View("AboutMe", StackRepository.Stack);
            }

            return View("AboutMe");
        }

        [HttpPost]
        public IActionResult AddUserInfo(string info)
        {
            if (!string.IsNullOrEmpty(info))
            {
                StackRepository.addStack(info);
                return View("AboutMe", StackRepository.UserInfo);
            }

            return View("AboutMe");
        }
    }
}