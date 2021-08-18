using Database.Entities.Users;
using Database.Entities.Users.Repositories;
using Logic.Auth.Login;
using Logic.Auth.Profile;
using Logic.Auth.Registration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;

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

        public void Login(LoginForm form, HttpContext httpContext)
        {
            var user = userRepository.Find(form.Phone);

            user.LastLogin = DateTime.Now;

            userRepository.SaveChanges();

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Phone)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        } 

        public ProfileModel GetAccount(string phone)
        {
            var user = userRepository.Find(phone);

            if(user == null)
            {
                return new ProfileModel();
            }

            return new ProfileModel() 
            {
                FIO = user.FIO,
                Phone = user.Phone,
                Email = user.Email,
                LastLogin = user.LastLogin?.ToString("dd/MM/yyyy HH:mm")
            };
        }
    }
}
