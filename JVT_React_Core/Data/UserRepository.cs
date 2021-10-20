using JVT_React_Core.Data.Models;
using JVT_React_Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JVT_React_Core.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _context;

        public UserRepository(UserDbContext context)
        {
            _context = context;    
        }

        public User Create(User user)
        {
            _context.Add(user);
            user.Id = _context.SaveChanges();
            return user;
        }

        public User Login(LoginDtos dtos)
        {
            var userr = _context.Users.FirstOrDefault(x => x.Email == dtos.Email);
            if (userr is not null)
            {
                if (BCrypt.Net.BCrypt.Verify(dtos.Password, userr.Password))
                {
                    return userr;
                }
            }
            return null;
        }
    }
}
