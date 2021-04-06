using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pozoriste.Domain.Common;
using Pozoriste.Domain.Interfaces;
using Pozoriste.Domain.Models;
using Pozoriste.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pozoriste.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressesController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AddressDomainModel>> GetAddressById(int id)
        {
            var address = await _addressService.GetByIdAsync(id); 

            if (address == null)
            {
                return NotFound(Messages.ADDRESS_NOT_FOUND);
            }
            return Ok(address);
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<IEnumerable<AddressDomainModel>>> GetAllAddresses()
        {
            IEnumerable<AddressDomainModel> addressDomainModels;
            addressDomainModels = await _addressService.GetAllAsync();

            if (addressDomainModels.Count() == 0)
            {
                addressDomainModels = new List<AddressDomainModel>();

            }

            return Ok(addressDomainModels);
        } 

    }
}
