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
    public class AuditoriumsController : ControllerBase
    {
        private readonly IAuditoriumService _auditoriumService;

        public AuditoriumsController(IAuditoriumService auditoriumService)
        {
            _auditoriumService = auditoriumService;
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<ActionResult<IEnumerable<AuditoriumDomainModel>>> GetAsync()
        {
            IEnumerable<AuditoriumDomainModel> auditoriumDomainModels;

            auditoriumDomainModels = await _auditoriumService.GetAllAuditoriums();

            if(auditoriumDomainModels == null)
            {
                auditoriumDomainModels = new List<AuditoriumDomainModel>();
            }

            return Ok(auditoriumDomainModels);
        }

        [HttpGet]
        [Route("getAllByTheatreId/{id}")]
        public async Task<ActionResult<IEnumerable<AuditoriumDomainModel>>> GetAllAuditoriumsByTheatreId(int id)
        {
            IEnumerable<AuditoriumDomainModel> auditoriumDomainModels;

            auditoriumDomainModels = await _auditoriumService.GetAuditoriumsByTheatreId(id);

            if(auditoriumDomainModels == null)
            {
                auditoriumDomainModels = new List<AuditoriumDomainModel>();
            }

            return Ok(auditoriumDomainModels);
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<AuditoriumDomainModel>> PostAsync(CreateAuditoriumModel createAuditoriumModel)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AuditoriumDomainModel auditoriumDomainModel = new AuditoriumDomainModel
            {
                TheatreId = createAuditoriumModel.theatreId,
                Name = createAuditoriumModel.auditName
            };

            CreateAuditoriumResultModel createAuditoriumResultModel;

            try
            {
                createAuditoriumResultModel = await _auditoriumService.AddAuditorium(auditoriumDomainModel, createAuditoriumModel.numberOfSeats, createAuditoriumModel.seatRows);
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

            if(!createAuditoriumResultModel.IsSuccessful)
            {
                ErrorResponseModel errorResponse = new ErrorResponseModel()
                {
                    ErrorMessage = createAuditoriumResultModel.ErrorMessage,
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };

                return BadRequest(errorResponse);
            }

            return Created("auditoriums//" + createAuditoriumResultModel.Auditorium.Id, createAuditoriumResultModel.Auditorium);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            DeleteAuditoriumResultModel deleteAudit;
            try
            {
                deleteAudit = await _auditoriumService.DeleteAuditorium(id);
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

            if (deleteAudit.IsSuccessful.Equals(false))
            {
                ErrorResponseModel errorResponse = new ErrorResponseModel()
                {
                    ErrorMessage = deleteAudit.ErrorMessage,
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };

                return BadRequest(errorResponse);
            }

            return Accepted("auditorium//" + deleteAudit.Auditorium.Id, deleteAudit.Auditorium);
        }
    }
}
