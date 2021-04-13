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
    public class ActorsController : ControllerBase
    {
        private readonly IActorService _actorService;

        public ActorsController(IActorService actorService)
        {
            _actorService = actorService;
        }

        [HttpPost]
        public async Task<ActionResult<ActorDomainModel>> AddActor([FromBody] ActorModel actorModel)
        {
            ActorDomainModel domainModel = new ActorDomainModel
            {
                FirstName = actorModel.FirstName,
                LastName = actorModel.LastName
            };

            ActorDomainModel createActor;

            try
            {
                createActor = await _actorService.AddActor(domainModel);
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

            if (createActor == null)
            {
                return BadRequest(Messages.ACTOR_CREATION_ERROR);
            }

            return CreatedAtAction(nameof(GetActorById), new { Id = createActor.Id }, createActor);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActorDomainModel>>> GetAllActors()
        {
            var actors = await _actorService.GetAllAsync();

            if (actors == null)
            {
                actors = new List<ActorDomainModel>(); 
            }

            return Ok(actors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ActorDomainModel>> GetActorById(int id)
        {
            var actor = await _actorService.GetByIdAsync(id);

            if (actor == null)
            {
                return NotFound(Messages.ACTOR_DOES_NOT_EXIST);
            }

            return Ok(actor);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ActorDomainModel>> DeleteActor(int id)
        {
            ActorDomainModel deletedActor;
            try
            {
                deletedActor = await _actorService.DeleteActor(id);
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

            if (deletedActor == null)
            {
                ErrorResponseModel errorResponse = new ErrorResponseModel
                {
                    ErrorMessage = Messages.ACTOR_DOES_NOT_EXIST,
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };

                return BadRequest(errorResponse);
            }

            return Accepted("actors//" + deletedActor.Id, deletedActor);
        }





    }
}
