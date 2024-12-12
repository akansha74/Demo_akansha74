using AllFunctionalityNetCore.Data;
using AllFunctionalityNetCore.Models;
using AllFunctionalityNetCore.Models.ViewModel;
using AllFunctionalityNetCore.Repository.Interface;
using AllFunctionalityNetCore.Repository.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AllFunctionalityNetCore.Controllers.Account
{
    public class AccountController : Controller
    {
        private readonly ApplicationContext context;
        private readonly IEmailSender emailSender;

        public AccountController(ApplicationContext context,IEmailSender emailSender)
        {
            this.context = context;
            this.emailSender = emailSender;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpUserViewModel md)
        {
            if(ModelState.IsValid)
            {
                var data = new User()
                {
                    UserName=md.UserName,
                    Email=md.Email,
                    Mobile=md.Mobile,
                    Password=md.Password,
                    IsActive=md.IsActive

                };
               
                context.Users.Add(data);
                context.SaveChanges();
              bool status= await emailSender.EmailSendAsync(md.Email, "Account Created", "Hi");
                    
                TempData["successmessage"] = "Eligible to login fill ur credentials and login";
                return RedirectToAction("Login");
            }
            else
            {
                TempData["errormessage"] = "Empty Form cannot be submitted";
                return View();
            }
            
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login( LoginSignUpViewModel md)
        {
            if (ModelState.IsValid)
            {
                var data = context.Users.Where(u => u.UserName == md.UserName).SingleOrDefault();
                if(data!=null)
                {
                    bool isValid = (data.UserName == md.UserName && data.Password == md.Password);
                    if(isValid)
                    {
                        var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, md.UserName) }, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal=new ClaimsPrincipal(identity);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                        HttpContext.Session.SetString("UserName", md.UserName);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["errormessage"] = "password is invalid";
                        return View(md);
                    }
                }
                return View();
            }
            else
            {
                TempData["errormessage"] = "User name not found";
                return View(md);
            }
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login","Account");

            
        }
    }
}
