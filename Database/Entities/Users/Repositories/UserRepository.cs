using System.Collections.Generic;
using System.Linq;

namespace Database.Entities.Users.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public User Find(int id)
        {
            return applicationDbContext.Users.FirstOrDefault(user => user.Id == id);
        }

        public User Find(string phone)
        {
            return applicationDbContext.Users.FirstOrDefault(user => user.Phone == phone);
        }

        public User Find(string phone, string password)
        {
            return applicationDbContext.Users.FirstOrDefault(user => user.Phone == phone && user.Password == password);
        }

        public List<User> GetAll()
        {
            return applicationDbContext.Users.ToList();
        }

        public void Add(User user)
        {
            applicationDbContext.Users.Add(user);
        }

        public void SaveChanges()
        {
            applicationDbContext.SaveChanges();
        }
    }
}
