using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult<IEnumerable<ShowPieceActorAuditoriumTheatreDomainModel>>> GetAllShows([FromQuery] ShowQuery showQuery)
        {
            IEnumerable<ShowPieceActorAuditoriumTheatreDomainModel> shows;

            if (showQuery.Search == null)
            {
                shows = await _showService.GetAllShowsAsync();
            }
            else
            {
                shows = await _showService.GetAllShowsAsync(showQuery.Search);
            }
            



            if (shows == null)
            {
                shows = new List<ShowPieceActorAuditoriumTheatreDomainModel>();
            }

            return Ok(shows);
        }

        [HttpPost]
        public async Task<ActionResult<ShowDomainModel>> CreateShowAsync([FromBody] ShowModel showModel)
        {
            ShowDomainModel domainModel = new ShowDomainModel
            {
                ShowTime = showModel.ShowTime,
                AuditoriumId = showModel.AuditoriumId,
                PieceId = showModel.PieceId,
                TicketPrice = showModel.TicketPrice,
                ActorsList = showModel.ActorIds
            };

            ShowResultModel createShow;

            try
            {
                createShow = await _showService.AddShow(domainModel);
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

            if (!createShow.isSuccessful)
            {
                ErrorResponseModel errorResponseModel = new ErrorResponseModel
                {
                    ErrorMessage = createShow.ErrorMessage
                };
                return BadRequest(errorResponseModel);
            }

            return CreatedAtAction(nameof(GetShowById) , new { id = createShow.ShowDomainModel.Id }, createShow.ShowDomainModel);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ShowDomainModel>> DeleteShow(int id)
        {
            ShowDomainModel deletedPiece;
            try
            {
                deletedPiece = await _showService.DeleteShow(id);
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

            if (deletedPiece == null)
            {
                ErrorResponseModel errorResponse = new ErrorResponseModel
                {
                    ErrorMessage = Messages.CANNOT_DELETE_SHOW,
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };

                return BadRequest(errorResponse);
            }

            return Accepted("pieces//" + deletedPiece.Id, deletedPiece);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ShowPieceActorAuditoriumTheatreDomainModel>> GetShowById(int id)
        {
            var show = await _showService.GetShowByIdAsync(id);

            if (show == null)
            {
                return NotFound(Messages.SHOW_NOT_FOUND);
            }

            return show;
        }
             
    }
}
