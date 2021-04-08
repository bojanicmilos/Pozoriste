using Pozoriste.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pozoriste.Domain.Interfaces
{
    public interface IUserService 
    {
        Task<UserDomainModel> GetUserByUserName(string username);
    }
}
