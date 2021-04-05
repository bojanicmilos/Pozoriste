using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pozoriste.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<T> Delete(int id);

        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);

        T Insert(T obj);

        T Update(T obj);
        void Save();
    }
}
