using Database.Entities.Users.Repositories;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;
using System.Text.RegularExpressions;

namespace Logic.Auth.Registration
{
    public class RegistrationFormValidator : IRegistrationFormValidator
    {
        private readonly IUserRepository userRepository;
        private const string emailPattern = "[.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}";

        public RegistrationFormValidator(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public ModelStateDictionary Validate(RegistrationForm form, ModelStateDictionary modelState)
        {
            ValidateFio(form.Fio, modelState);
            ValidatePhone(form.Phone, modelState);
            ValidateEmail(form.Email, modelState);
            ValidatePassword(form.Password, form.PasswordConfirm, modelState);

            return modelState;
        }

        private void ValidateFio(string fio, ModelStateDictionary modelState)
        {
            if (string.IsNullOrWhiteSpace(fio))
            {
                modelState.AddModelError("Fio", "Поле ФИО должно быть заполнено");
                return;
            }

            if (fio.Length > 250)
            {
                modelState.AddModelError("Fio", "Длина должна быть меньше 250 символов");
            }
        }

        private void ValidatePhone(string phone, ModelStateDictionary modelState)
        {
            if (string.IsNullOrWhiteSpace(phone))
            {
                modelState.AddModelError("Phone", "Поле Телефон должно быть заполнено");
                return;
            }

            if (phone.Length > 11)
                modelState.AddModelError("Phone", "Длина должна быть меньше 11 символов");

            if (phone[0] != '7')
            {
                modelState.AddModelError("Phone", "Номер должен начинаться с \"7\"");
                return;
            }

            var isExist = userRepository.GetAll().Any(user => user.Phone == phone);

            if(isExist)
                modelState.AddModelError("Phone", "Такой номер уже существует");

        }

        private void ValidateEmail(string email, ModelStateDictionary modelState)
        {
            if(string.IsNullOrWhiteSpace(email))
            {
                modelState.AddModelError("Email", "Поле Email должно быть заполнено");
                return;
            }

            if (email.Length > 11)
                modelState.AddModelError("Email", "Длина должна быть меньше 150 символов");

            var isMatch = Regex.Match(email, emailPattern, RegexOptions.IgnoreCase);

            if(!isMatch.Success)
            {
                modelState.AddModelError("Email", "Неверный формат");
                return;
            }

            var isExist = userRepository.GetAll().Any(user => user.Email == email);

            if (isExist)
                modelState.AddModelError("Email", "Такой email уже существует");
        }

        private void ValidatePassword(string password, string passwordConfirm, ModelStateDictionary modelState)
        {
            if(string.IsNullOrWhiteSpace(password))
            {
                modelState.AddModelError("Password", "Поле Пароль должно быть заполнено");
                return;
            }

            if(string.IsNullOrWhiteSpace(passwordConfirm))
            {
                modelState.AddModelError("PasswordConfirm", "Поле Подтвеждение пароля должно быть заполнено");
                return;
            }

            if (password.Length > 20)
                modelState.AddModelError("Password", "Длина должна быть меньше 20 символов");

            if (passwordConfirm.Length > 20)
                modelState.AddModelError("PasswordConfirm", "Длина должна быть меньше 20 символов");

            if(!password.Equals(passwordConfirm))
                modelState.AddModelError("PasswordConfirm", "Пароли должны совпадать");
        }
    }
}
