using System.Collections.Generic;

namespace Database.Entities.Users.Repositories
{
    public interface IUserRepository
    {
        User Find(int id);
        List<User> GetAll();
        void Add(User user);
        void SaveChanges();
    }
}