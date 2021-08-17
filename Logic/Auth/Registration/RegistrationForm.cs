using System.ComponentModel.DataAnnotations;

namespace Logic.Auth.Registration
{
    public class RegistrationForm
    {
        public string Fio { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}
