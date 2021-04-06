using Pozoriste.Data.Entities;
using Pozoriste.Domain.Interfaces;
using Pozoriste.Domain.Models;
using Pozoriste.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pozoriste.Domain.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressesRepository _addressesRepository;

        public AddressService(IAddressesRepository addressesRepository)
        {
            _addressesRepository = addressesRepository;
        }
       
        public async Task<AddressDomainModel> AddAddress(AddressDomainModel addressModel)
        {
            Address createAddress = new Address()
            {
                CityName = addressModel.CityName,
                StreetName = addressModel.StreetName,
                
            };

            Address data = _addressesRepository.Insert(createAddress);

            if (data == null)
            {
                return null;
            }

            _addressesRepository.Save();

            return new AddressDomainModel
            {
                CityName = data.CityName,
                StreetName = data.StreetName
            };

        }

        public async Task<IEnumerable<AddressDomainModel>> GetAllAsync()
        {
            var addresses = await _addressesRepository.GetAllAsync();

            if (addresses.Count() == 0)
            {
                return null;
            }

            List<AddressDomainModel> addressDomainModels = new List<AddressDomainModel>();

            foreach (var address in addresses)
            {
                AddressDomainModel addressDomainModel = new AddressDomainModel
                {
                    Id = address.Id,
                    CityName = address.CityName,
                    StreetName = address.StreetName

                };
                addressDomainModels.Add(addressDomainModel);
            }

            return addressDomainModels;
        }

        public async Task<AddressDomainModel> GetByIdAsync(int id)
        {
            var address = await _addressesRepository.GetByIdAsync(id);
            
            if (address == null)
            {
                return null;
            }

            AddressDomainModel addressDomainModel = new AddressDomainModel
            {
                Id = address.Id,
                CityName = address.CityName,
                StreetName = address.StreetName
            };
            return addressDomainModel;
        }
    }
}
