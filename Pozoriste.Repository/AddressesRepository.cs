using Microsoft.EntityFrameworkCore;
using Pozoriste.Data.Context;
using Pozoriste.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pozoriste.Repository
{
    public interface IAddressesRepository : IRepository<Address>
    {
        Task<Address> GetByCityNameAsync(string cityName);
    }
    public class AddressesRepository : IAddressesRepository
    {
        private TheatreContext _theatreContext;

        public AddressesRepository(TheatreContext theatreContext)
        {
            _theatreContext = theatreContext;
        }

        public async Task<Address> Delete(int id)
        {
            Address existing = await _theatreContext.Addresses.FindAsync(id);
            var result = _theatreContext.Addresses.Remove(existing);

            return result.Entity;
        }

        public async Task<IEnumerable<Address>> GetAllAsync()
        {
            var data = await _theatreContext.Addresses.ToListAsync();

            return data;
        }

        public async Task<Address> GetByIdAsync(int id)
        {
            var data = await _theatreContext.Addresses.FindAsync(id);

            return data;
        }

        public Address Insert(Address obj)
        {
            return _theatreContext.Addresses.Add(obj).Entity;
        }

        public void Save()
        {
             _theatreContext.SaveChanges();
        }

        public Address Update(Address obj)
        {
            _theatreContext.Entry(obj).State = EntityState.Modified;

            return obj;
        }

        public async Task<Address> GetByCityNameAsync(string cityName)
        {
            var city = await _theatreContext.Addresses.SingleOrDefaultAsync(city => city.CityName == cityName);

            return city;
        }
    }
}
