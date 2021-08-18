using Database.Entities.Users.Repositories;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Auth.Login
{
    public class LoginFormValidator : ILoginFormValidator
    {
        private readonly IUserRepository userRepository;

        public LoginFormValidator(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void Validate(LoginForm form, ModelStateDictionary modelState)
        {
            if (string.IsNullOrWhiteSpace(form.Phone))
            {
                modelState.AddModelError("Phone", "Поле Номер телефона должно быть заполнено");
            }

            if (string.IsNullOrWhiteSpace(form.Password))
            {
                modelState.AddModelError("Password", "Поле Пароль должно быть заполнено");
            }

            var checkUser = userRepository.Find(form.Phone);

            if(checkUser == null)
            {
                modelState.AddModelError("Phone", "Пользователя с таким номер телефона не существует");
                return;
            }

            if(checkUser.Password != form.Password)
            {
                modelState.AddModelError("Password", "Неверный пароль");
            }
        }
    }
}
