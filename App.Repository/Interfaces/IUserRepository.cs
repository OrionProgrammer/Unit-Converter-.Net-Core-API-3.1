using App.Domain;
using App.Repository.Helpers;

namespace App.Repository.Interfaces
{

    public interface IUserRepository : IGenericRepository<User>
    {
        User GetByEmail(string email);
    }
}
