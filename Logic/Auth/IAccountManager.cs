using Logic.Auth.Registration;

namespace Logic.Auth
{
    public interface IAccountManager
    {
        void Register(RegistrationForm form);
    }
}