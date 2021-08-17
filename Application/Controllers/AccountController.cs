using Logic.Auth;
using Logic.Auth.Registration;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly IRegistrationFormValidator registrationFormValidator;
        private readonly IAccountManager accountManager;

        public AccountController(IRegistrationFormValidator registrationFormValidator, IAccountManager accountManager)
        {
            this.registrationFormValidator = registrationFormValidator;
            this.accountManager = accountManager;
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("register")]
        public IActionResult Register(RegistrationForm form)
        {
            var valResult = registrationFormValidator.Validate(form, ModelState);

            if (valResult.IsValid)
            {
                accountManager.Register(form);
                return View(new RegistrationForm());
            }
                
            return View(form);
        }
    }
}
