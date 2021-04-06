using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pozoriste.API.Models;
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
        [HttpPost]
        [Route("create")]

        public async Task<ActionResult> Post([FromBody] AddressModel addressModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AddressDomainModel domainModel = new AddressDomainModel
            {
                CityName = addressModel.CityName,
                StreetName = addressModel.StreetName

            };

            AddressDomainModel createAddress;
            try
            {
                createAddress = await _addressService.AddAddress(domainModel);
            }
            catch(DbUpdateException e)
            {
                ErrorResponseModel errorResponse = new ErrorResponseModel
                {
                    ErrorMessage = e.InnerException.Message ?? e.Message,
                    StatusCode = System.Net.HttpStatusCode.BadRequest

                };
                return BadRequest(errorResponse);
            }
            if (createAddress == null)
            {
                ErrorResponseModel errorResponse = new ErrorResponseModel
                {
                    ErrorMessage = Messages.ADDRESS_CREATION_ERROR,
                    StatusCode = System.Net.HttpStatusCode.InternalServerError

                };
                return BadRequest(errorResponse);
            }
            return Created("Addresses //" + createAddress.Id, createAddress); 
        }
    }
}
