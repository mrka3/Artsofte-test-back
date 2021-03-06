using Logic.Auth;
using Logic.Auth.Login;
using Logic.Auth.Registration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly IRegistrationFormValidator registrationFormValidator;
        private readonly ILoginFormValidator loginFormValidator;
        private readonly IAccountManager accountManager;

        public AccountController(IRegistrationFormValidator registrationFormValidator,
                                 IAccountManager accountManager,
                                 ILoginFormValidator loginFormValidator)
        {
            this.registrationFormValidator = registrationFormValidator;
            this.accountManager = accountManager;
            this.loginFormValidator = loginFormValidator;
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("register")]
        public IActionResult Register(RegistrationForm form)
        {
            registrationFormValidator.Validate(form, ModelState);

            if (ModelState.IsValid)
            {
                accountManager.Register(form);
                return RedirectToAction("RegisterSuccess", new { name = form.Fio });
            }
                
            return View(form);
        }
        
        [HttpGet("register/success")]
        public IActionResult RegisterSuccess(string name)
        {
            ViewData["Name"] = name;
            return View();
        }
        
        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public IActionResult Login(LoginForm form)
        {
            loginFormValidator.Validate(form, ModelState);
            if (ModelState.IsValid)
            {
                accountManager.Login(form, HttpContext);
                return RedirectToAction("Cabinet");
            }

            return View(form);
        }

        [Authorize]
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        [Authorize]
        [HttpGet("cabinet")]
        public IActionResult Cabinet()
        {
            var phoneIdentity = HttpContext.User.Identity.Name;
            var profile = accountManager.GetAccount(phoneIdentity);
            return View(profile);
        }
    }
}
