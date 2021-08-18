using Logic.Auth.Login;
using Logic.Auth.Profile;
using Logic.Auth.Registration;
using Microsoft.AspNetCore.Http;

namespace Logic.Auth
{
    public interface IAccountManager
    {
        void Register(RegistrationForm form);
        void Login(LoginForm form, HttpContext httpContext);
        ProfileModel GetAccount(string phone);
    }
}