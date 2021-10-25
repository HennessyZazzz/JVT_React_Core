using JVT_React_Core.Data.Models;
using JVT_React_Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JVT_React_Core.Data
{
    public interface IUserRepository
    {
        User Create(User user);
        User Login(LoginDtos dtos);
        User Delete(DeleteDtos dtos);
        User Update(UpdateDtos dtos);
        User GetUserById(int id);
    }
}
