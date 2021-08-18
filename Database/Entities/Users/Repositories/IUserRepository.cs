using System.Collections.Generic;

namespace Database.Entities.Users.Repositories
{
    public interface IUserRepository
    {
        User Find(int id);
        User Find(string phone);
        User Find(string phone, string password);
        List<User> GetAll();
        void Add(User user);
        void SaveChanges();
    }
}