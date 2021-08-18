using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Logic.Auth.Login
{
    public interface ILoginFormValidator
    {
        void Validate(LoginForm form, ModelStateDictionary modelState);
    }
}