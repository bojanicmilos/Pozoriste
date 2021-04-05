using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class ShowsController : ControllerBase
    {
        private readonly IShowService _showService;

        public ShowsController(IShowService showService)
        {
            _showService = showService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShowPieceActorAuditoriumTheatreDomainModel>>> GetAllShows()
        {
            var shows = await _showService.GetAllShowsAsync();

            if (shows == null)
            {
                shows = new List<ShowPieceActorAuditoriumTheatreDomainModel>();
            }

            return Ok(shows);
        }
             
    }
}
