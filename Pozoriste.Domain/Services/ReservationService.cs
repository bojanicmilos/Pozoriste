using Pozoriste.Data.Entities;
using Pozoriste.Domain.Common;
using Pozoriste.Domain.Interfaces;
using Pozoriste.Domain.Models;
using Pozoriste.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pozoriste.Domain.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationsRepository _reservationsRepository;
        private readonly ISeatsRepository _seatRepository;
        private readonly IShowsRepository _showRepository;
        private readonly IAuditoriumsRepository _auditoriumRepository;
        private readonly IUsersRepository _userRepository;

        public ReservationService(IReservationsRepository reservationsRepository, ISeatsRepository seatRepository, IShowsRepository showRepository, IAuditoriumsRepository auditoriumRepository, IUsersRepository userRepository)
        {
            _reservationsRepository = reservationsRepository;
            _seatRepository = seatRepository;
            _showRepository = showRepository;
            _auditoriumRepository = auditoriumRepository;
            _userRepository = userRepository;
        }

        public async Task<ReservationResultModel> CreateReservation(ReservationDomainModel requestedReservation)
        {
            // get taken seats for show
            var takenSeats = await GetTakenSeats(requestedReservation.ShowId);

            var seats = await _seatRepository.GetAllAsync();

            // check if show id is valid
            var show = await _showRepository.GetByIdAsync(requestedReservation.ShowId);

            if (show == null)
            {
                return new ReservationResultModel
                {
                    isSuccessful = false,
                    ErrorMessage = Messages.SHOW_DOES_NOT_EXIST_FOR_RESERVATION
                };
            }

            // check if user id is valid
            var user = await _userRepository.GetByIdAsync(requestedReservation.UserId);

            if (user == null)
            {
                return new ReservationResultModel
                {
                    isSuccessful = false,
                    ErrorMessage = Messages.USER_FOR_RESERVATION_DOES_NOT_EXIST
                };
            }

            // get all seats for auditorium
            seats = seats.Where(seat => seat.AuditoriumId == show.AuditoriumId);

            //check if requested seats exist in the auditorium 
            var duplicateSeats = seats
                .Select(x => x.Id)
                .Intersect(requestedReservation.SeatsList
                .Select(x => x.Id));

            if (duplicateSeats.Count() != requestedReservation.SeatsList.Count())
            {
                return new ReservationResultModel
                {
                    isSuccessful = false,
                    ErrorMessage = Messages.SEAT_DOES_NOT_EXIST_FOR_AUDITORIUM
                };
            }

            // check if requested seats are more than 1 and in the same row
            List<SeatDomainModel> seatModels = new List<SeatDomainModel>();

            foreach(var seat in requestedReservation.SeatsList)
            {
                var reqSeat = await _seatRepository.GetByIdAsync(seat.Id);
                SeatDomainModel seatDomain = new SeatDomainModel
                {
                    Id = reqSeat.Id,
                    Number = reqSeat.Number,
                    Row = reqSeat.Row,
                    AuditoriumId = reqSeat.AuditoriumId
                };

                seatModels.Add(seatDomain);
            }

            // check if seats are duplicates
            var row = seatModels[0].Number;
            var differentRows = seatModels.Select(x => x.Number).Distinct();

            if (differentRows.Count() < seatModels.Count)
            {
                return new ReservationResultModel
                {
                    isSuccessful = false,
                    ErrorMessage = Messages.SEATS_CANNOT_BE_DUPLICATES
                };
            }

            // check if seats are in the same row
            if (requestedReservation.SeatsList.Count() > 1)
            {
                var singleSeat = seatModels[0];

                foreach(var x in seatModels)
                {
                    if (singleSeat.Row != x.Row)
                    {
                        return new ReservationResultModel
                        {
                            isSuccessful = false,
                            ErrorMessage = Messages.SEATS_NOT_IN_THE_SAME_ROW
                        };
                    }
                }
            }

            // check if seats are next to each other
            if (requestedReservation.SeatsList.Count() > 1)
            {
                seatModels = seatModels.OrderByDescending(x => x.Number).ToList();

                var singleSeat2 = seatModels[0];

                var counter = 1;

                foreach(var x in seatModels.Skip(1))
                {
                    if(x.Number + counter != singleSeat2.Number)
                    {
                        return new ReservationResultModel
                        {
                            isSuccessful = false,
                            ErrorMessage = Messages.SEATS_MUST_BE_NEXT_TO_EACH_OTHER
                        };
                    }
                    else
                    {
                        counter++;
                    }
                }

            }

            // check if requested seats are already taken
            if (takenSeats != null)
            {
                foreach(var takenSeat in takenSeats)
                {
                    foreach(var requestedSeat in requestedReservation.SeatsList)
                    {
                        if (takenSeat.Id == requestedSeat.Id)
                        {
                            return new ReservationResultModel
                            {
                                isSuccessful = false,
                                ErrorMessage = Messages.SEATS_ALREADY_TAKEN_ERROR
                            };
                        }
                    }
                }
            }

            // insert in database
            Reservation reservationToInsert = new Reservation
            {
                ShowId = requestedReservation.ShowId,
                UserId = requestedReservation.UserId,
                ReservationSeats = new List<ReservationSeat>()
            };

            var insertedReservation = _reservationsRepository.Insert(reservationToInsert);

            foreach(var rs in requestedReservation.SeatsList)
            {
                reservationToInsert.ReservationSeats.Add(new ReservationSeat
                {
                    SeatId = rs.Id,
                    ReservationId = insertedReservation.Id
                });
            }

            _reservationsRepository.Save();

            return new ReservationResultModel
            {
                isSuccessful = true,
                ReservationDomainModel = new ReservationDomainModel
                {
                    Id = insertedReservation.Id,
                    ShowId = insertedReservation.ShowId,
                    UserId = insertedReservation.UserId
                }
            };

        }

        public async Task<IEnumerable<SeatDomainModel>> GetTakenSeats(int showId)
        {
            var reservationsForShow = await _reservationsRepository.GetReservationByShowId(showId);

            if (reservationsForShow.Count() == 0)
            {
                return null;
            }

            List<SeatDomainModel> seatList = new List<SeatDomainModel>();

            foreach(var reservation in reservationsForShow)
            {
                var seats = reservation.ReservationSeats.Select(resSeat => new SeatDomainModel
                {
                    Id = resSeat.SeatId,
                    AuditoriumId = resSeat.Seat.AuditoriumId,
                    Number = resSeat.Seat.Number,
                    Row = resSeat.Seat.Row
                });

                foreach(var seat in seats)
                {
                    seatList.Add(seat);
                }
            }

            return seatList;
        }

        public async Task<IEnumerable<UserReservationDomainModel>> GetReservationsByUserId(int userId)
        {
            var reservations = await _reservationsRepository.GetReservationsByUserId(userId);

            if (reservations.Count() == 0)
            {
                return null;
            }

            IEnumerable<UserReservationDomainModel> userReservations = reservations
                .Select(reservation => new UserReservationDomainModel
            {
                Id = reservation.Id,
                PieceTitle = reservation.Show.Piece.Title,
                ShowTime = reservation.Show.ShowTime.ToString("MM/dd/yyyy HH:mm"),
                AuditoriumName = reservation.Show.Auditorium.Name,
                TheatreName = reservation.Show.Auditorium.Theatre.Name,
                ReservedSeats = reservation.ReservationSeats.Select(resSeat => new SeatDomainModel 
                {
                    Id = resSeat.Seat.Id,
                    Number = resSeat.Seat.Number,
                    Row = resSeat.Seat.Row
                }).ToList()
            });

            return userReservations;
        }
    }
}
