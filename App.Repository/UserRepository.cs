using System.Linq;
using App.Domain;
using App.Repository.Helpers;
using App.Domain.Helpers;
using App.Repository.Interfaces;

namespace App.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {

        }

        public User GetByEmail(string email)
        {
            return _context.User
                            .Where(e => e.Email == email)
                            .FirstOrDefault();
        }
    }
}
