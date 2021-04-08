using Microsoft.EntityFrameworkCore;
using Pozoriste.Data.Context;
using Pozoriste.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pozoriste.Repository
{
    public interface IUsersRepository : IRepository<User>
    {
        Task<User> GetByUserName(string username);
    }
    public class UsersRepository : IUsersRepository
    {
        private TheatreContext _theatreContext;

        public UsersRepository(TheatreContext theatreContext)
        {
            _theatreContext = theatreContext;
        }

        public async Task<User> Delete(int id)
        {
            User existing = await _theatreContext.Users.FindAsync(id);
            var result = _theatreContext.Users.Remove(existing);

            return result.Entity;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var data = await _theatreContext.Users.ToListAsync();

            return data;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var data = await _theatreContext.Users.FindAsync(id);

            return data;
        }

        public async Task<User> GetByUserName(string username)
        {
            var user = await _theatreContext.Users
                .FirstOrDefaultAsync(user => user.UserName == username);

            return user;
        }

        public User Insert(User obj)
        {
            return _theatreContext.Users.Add(obj).Entity;
        }

        public void Save()
        {
            _theatreContext.SaveChanges();
        }

        public User Update(User obj)
        {
            _theatreContext.Entry(obj).State = EntityState.Modified;

            return obj;
        }
    }
}
