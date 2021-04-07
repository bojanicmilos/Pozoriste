using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pozoriste.Domain.Common;
using Pozoriste.Domain.Interfaces;
using Pozoriste.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pozoriste.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatsController : ControllerBase
    {
        private readonly ISeatService _seatService;

        public SeatsController(ISeatService seatService)
        {
            _seatService = seatService;
        }

        [HttpGet("numberofseats/{id}")]
        public async Task<ActionResult<SeatAuditoriumDomainModel>> GetSeatsByAuditoriumId(int id)
        {
            var seatsInAuditorium = await _seatService.GetSeatsByAuditoriumId(id);

            if (seatsInAuditorium == null)
            {
                return NotFound(Messages.SEAT_AUDITORIUM_NOT_FOUND);
            }

            return Ok(seatsInAuditorium);
        }
    }
}
