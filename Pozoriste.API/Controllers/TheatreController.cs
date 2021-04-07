using Microsoft.AspNetCore.Mvc;
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
    public class TheatreController : ControllerBase
    {
        private readonly ITheatreService _theatreService;
        private readonly IAddressService _addressService;

        public TheatreController(ITheatreService theatreService, IAddressService addressService)
        {
            _theatreService = theatreService;
            _addressService = addressService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TheatreDomainModel>>> GetAsync()
        {
            IEnumerable<TheatreDomainModel> theatreDomainModels;

            theatreDomainModels = await _theatreService.GetAllAsync();

            if(theatreDomainModels == null)
            {
                theatreDomainModels = new List<TheatreDomainModel>();
            }

            return Ok(theatreDomainModels);
        }
    }
}
