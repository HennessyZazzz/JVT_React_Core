using JVT_React_Core.Data.Models;
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
    }
}
