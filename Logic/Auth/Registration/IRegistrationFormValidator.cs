using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Logic.Auth.Registration
{
    public interface IRegistrationFormValidator
    {
        ModelStateDictionary Validate(RegistrationForm form, ModelStateDictionary modelState);
    }
}