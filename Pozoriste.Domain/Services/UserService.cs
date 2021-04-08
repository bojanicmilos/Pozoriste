using Pozoriste.Domain.Interfaces;
using Pozoriste.Domain.Models;
using Pozoriste.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pozoriste.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUsersRepository _usersRepository;

        public UserService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<UserDomainModel> GetUserByUserName(string username)
        {
            var user = await _usersRepository.GetByUserName(username);

            if (user == null)
            {
                return null;
            }

            return new UserDomainModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                UserRole = user.UserRole
            };
            
        }
    }
}
