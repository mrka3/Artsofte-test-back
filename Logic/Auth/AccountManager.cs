using Database.Entities.Users;
using Database.Entities.Users.Repositories;
using Logic.Auth.Registration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Auth
{
    public class AccountManager : IAccountManager
    {
        private readonly IUserRepository userRepository;

        public AccountManager(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void Register(RegistrationForm form)
        {
            var user = new User()
            {
                FIO = form.Fio,
                Phone = form.Phone,
                Email = form.Email,
                Password = form.Password
            };

            userRepository.Add(user);
            userRepository.SaveChanges();
        }
    }
}
