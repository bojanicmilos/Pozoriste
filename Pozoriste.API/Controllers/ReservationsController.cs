using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pozoriste.API.Models;
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
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost("{action}")]
        public async Task<ActionResult<ReservationResultModel>> MakeReservation([FromBody] ReservationModel model)
        {
            ReservationDomainModel reservationModel = new ReservationDomainModel
            {
                ShowId = model.ShowId,
                UserId = model.UserId,
                SeatsList = model.SeatIds
            };

            ReservationResultModel reservationResultModel;

            try
            {
                reservationResultModel = await _reservationService.CreateReservation(reservationModel);
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

            if (!reservationResultModel.isSuccessful)
            {
                ErrorResponseModel errorResponseModel = new ErrorResponseModel
                {
                    ErrorMessage = reservationResultModel.ErrorMessage
                };
                return BadRequest(errorResponseModel);
            }
            return Created("reservations//" + reservationResultModel.ReservationDomainModel.Id, reservationResultModel.ReservationDomainModel);
        }

        [HttpGet("getbyshowid/{id}")]
        public async Task<ActionResult<IEnumerable<SeatDomainModel>>> GetTakenSeats(int id)
        {
            var takenSeats = await _reservationService.GetTakenSeats(id);

            if (takenSeats == null)
            {
                takenSeats = new List<SeatDomainModel>();
            }

            return Ok(takenSeats);
        }

        [HttpGet("byuserid/{id}")]
        public async Task<ActionResult<IEnumerable<UserReservationDomainModel>>> GetUserReservationsByUserId(int id)
        {
            var reservations = await _reservationService.GetReservationsByUserId(id);

            if (reservations == null)
            {
                reservations = new List<UserReservationDomainModel>();
            }

            return Ok(reservations);
        }


    }
}
