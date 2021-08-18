using Logic.Auth;
using Logic.Auth.Login;
using Logic.Auth.Profile;
using Logic.Auth.Registration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Areas
{
    [Route("account/api")]
    public class AccountApiController : ControllerBase
    {
        private readonly IRegistrationFormValidator registrationFormValidator;
        private readonly ILoginFormValidator loginFormValidator;
        private readonly IAccountManager accountManager;

        public AccountApiController(IRegistrationFormValidator registrationFormValidator,
                                 IAccountManager accountManager,
                                 ILoginFormValidator loginFormValidator)
        {
            this.registrationFormValidator = registrationFormValidator;
            this.accountManager = accountManager;
            this.loginFormValidator = loginFormValidator;
        }

        [HttpPost("register")]
        public IActionResult Register(RegistrationForm form)
        {
            registrationFormValidator.Validate(form, ModelState);

            if (ModelState.IsValid)
            {
                accountManager.Register(form);
                return Ok();
            }

            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginForm form)
        {
            loginFormValidator.Validate(form, ModelState);
            if (ModelState.IsValid)
            {
                accountManager.Login(form, HttpContext);
                return Ok();
            }

            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }

        [Authorize]
        [HttpGet("get-my-info")]
        public ProfileModel GetMyInfo()
        {
            var phoneIdentity = HttpContext.User.Identity.Name;
            var profile = accountManager.GetAccount(phoneIdentity);
            return profile;
        }
    }
}
