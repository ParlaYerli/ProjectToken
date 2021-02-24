using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.API.Domain.Model;

namespace Web.API.Domain.Repositories
{
    public interface IUserRepository
    {
        void AddUser(User user);
        User FindById(int userId);
        User FindByEmailandPassword(string email, string password);
        void SaveRefreshToken(int userId,string refreshToken);
        User GetUserWithRefreshToken(string refreshToken);
        void RemoveRefreshToken(User user);
    }
}
