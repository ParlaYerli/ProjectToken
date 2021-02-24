using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.API.Domain.Entities;
using Web.API.Domain.Model;
using Web.API.Security.Token;

namespace Web.API.Domain.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly TokenOptions _tokenOptions;
        public UserRepository(TokenContext _context,IOptions<TokenOptions> tokenOptions) :base(_context)
        {
            _tokenOptions = tokenOptions.Value;
        }
        public void AddUser(User user)
        {
            _context.Users.Add(user);
        }

        public User FindByEmailandPassword(string email, string password)
        {
            return _context.Users.Where(u => u.Email == email && u.Password == password).FirstOrDefault();
        }

        public User FindById(int userId)
        {
            return _context.Users.Find(userId);
        }

        public User GetUserWithRefreshToken(string refreshToken)
        {
            return _context.Users.FirstOrDefault(u=>u.RefreshToken == refreshToken);
        }

        public void RemoveRefreshToken(User user)
        {
            User newUser = this.FindById(user.Id);
            newUser.RefreshToken = null;
        }

        public void SaveRefreshToken(int userId, string refreshToken)
        {
            User newUser = this.FindById(userId);
            newUser.RefreshToken = refreshToken;
            newUser.RefreshTokenEndDate = DateTime.Now.AddDays(_tokenOptions.RefreshTokenExpiration);
        }
    }
}
