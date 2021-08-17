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
