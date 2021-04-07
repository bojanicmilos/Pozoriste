using Pozoriste.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pozoriste.Domain.Interfaces
{
    public interface IAddressService
    {
        Task<IEnumerable<AddressDomainModel>> GetAllAsync();
        Task<AddressDomainModel> GetByIdAsync(int id);
        Task<AddressDomainModel> AddAddress(AddressDomainModel addressModel);
        Task<AddressDomainModel> GetByCityNameAsync(string cityName);
    }
}
