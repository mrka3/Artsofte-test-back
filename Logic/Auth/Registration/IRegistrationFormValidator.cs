using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Logic.Auth.Registration
{
    public interface IRegistrationFormValidator
    {
        void Validate(RegistrationForm form, ModelStateDictionary modelState);
    }
}