using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pozoriste.API.Models;
using Pozoriste.Domain.Common;
using Pozoriste.Domain.Interfaces;
using Pozoriste.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pozoriste.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TheatresController : ControllerBase
    {
        private readonly ITheatreService _theatreService;
        private readonly IAddressService _addressService;

        public TheatresController(ITheatreService theatreService, IAddressService addressService)
        {
            _theatreService = theatreService;
            _addressService = addressService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TheatreDomainModel>>> GetAsync()
        {
            IEnumerable<TheatreDomainModel> theatreDomainModels;

            theatreDomainModels = await _theatreService.GetAllAsync();

            if (theatreDomainModels == null)
            {
                theatreDomainModels = new List<TheatreDomainModel>();
            }

            return Ok(theatreDomainModels);
        }

        [HttpPost]
        public async Task<ActionResult<TheatreDomainModel>> PostAsync([FromBody] CreateTheatreModel createTheatreModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AddressDomainModel addressModel = await _addressService.GetByCityNameAsync(createTheatreModel.CityName);

            if (addressModel == null)
            {
                return BadRequest(Messages.ADDRESS_NOT_FOUND);
            }

            TheatreDomainModel theatreDomainModel = new TheatreDomainModel
            {
                Name = createTheatreModel.Name,
                AddressId = addressModel.Id,
            };

            TheatreDomainModel insertedModel;

            try
            {
                insertedModel = await _theatreService.Create(theatreDomainModel, createTheatreModel.NumberOfSeats, createTheatreModel.SeatRows, createTheatreModel.AuditName);
            }
            catch (DbUpdateException e)
            {
                ErrorResponseModel errorResponse = new ErrorResponseModel
                {
                    ErrorMessage = e.InnerException.Message ?? e.Message,
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };

                return BadRequest(errorResponse);
            }

            if (insertedModel == null)
            {
                ErrorResponseModel errorResponse = new ErrorResponseModel
                {
                    ErrorMessage = Messages.THEATRE_CREATION_ERROR,
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };

                return BadRequest(errorResponse);
            }

            return Created("theatres//" + insertedModel.Id, insertedModel);
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<TheatreDomainModel>> DeleteAsync(int id)
        {
            TheatreDomainModel deletedTheatre;

            try
            {
                deletedTheatre = await _theatreService.Delete(id);
            }
            catch (DbUpdateException e)
            {
                ErrorResponseModel errorResponse = new ErrorResponseModel
                {
                    ErrorMessage = e.InnerException.Message ?? e.Message,
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };

                return BadRequest(errorResponse);
            }

            if(deletedTheatre == null)
            {
                ErrorResponseModel errorResponse = new ErrorResponseModel
                {
                    ErrorMessage = Messages.THEATRE_DOES_NOT_EXIST,
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };

                return BadRequest(errorResponse);
            }

            return Accepted("theatres//" + deletedTheatre.Id, deletedTheatre);
        }
    }
}
