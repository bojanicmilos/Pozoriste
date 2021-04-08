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
    public class AuditoriumService : IAuditoriumService
    {
        private readonly IAuditoriumsRepository _auditoriumsRepository;
        private readonly ITheatreRepository _theatreRepository;
        private readonly ISeatsRepository _seatsRepository;
        private IShowsRepository _showsRepository;

        public AuditoriumService(IAuditoriumsRepository auditoriumsRepository, ITheatreRepository theatreRepository)
        {
            _auditoriumsRepository = auditoriumsRepository;
            _theatreRepository = theatreRepository;
        }

        public async Task<CreateAuditoriumResultModel> AddAuditorium(AuditoriumDomainModel domainModel, int numberOfRows, int numberOfSeats)
        {
            var theatre = await _theatreRepository.GetByIdAsync(domainModel.TheatreId);
            if (theatre == null)
            {
                return new CreateAuditoriumResultModel
                {
                    IsSuccessful = false,
                    ErrorMessage = Messages.AUDITORIUM_INVALID_CINEMAID
                };
            }

            Auditorium newAuditorium = new Auditorium
            {
                Name = domainModel.Name,
                TheatreId = domainModel.TheatreId
            };

            newAuditorium.Seats = new List<Seat>();

            for (int i = 1; i <= numberOfRows; i++)
            {
                for (int j = 1; j <= numberOfSeats; j++)
                {
                    Seat newSeat = new Seat()
                    {
                        Row = i,
                        Number = j
                    };

                    newAuditorium.Seats.Add(newSeat);
                }
            }

            Auditorium insertedAuditorium = _auditoriumsRepository.Insert(newAuditorium);

            if (insertedAuditorium == null)
            {
                return new CreateAuditoriumResultModel
                {
                    IsSuccessful = false,
                    ErrorMessage = Messages.AUDITORIUM_CREATION_ERROR
                };
            }

            _auditoriumsRepository.Save();

            CreateAuditoriumResultModel resultModel = new CreateAuditoriumResultModel
            {
                IsSuccessful = true,
                ErrorMessage = null,
                Auditorium = new AuditoriumDomainModel
                {
                    Id = insertedAuditorium.Id,
                    Name = insertedAuditorium.Name,
                    TheatreId = insertedAuditorium.TheatreId,
                    SeatsList = new List<SeatDomainModel>()
                }
            };

            foreach (var item in insertedAuditorium.Seats)
            {
                resultModel.Auditorium.SeatsList.Add(new SeatDomainModel
                {
                    AuditoriumId = insertedAuditorium.Id,
                    Id = item.Id,
                    Number = item.Number,
                    Row = item.Row
                });
            }

            return resultModel;
        }

/*        public async Task<AuditoriumDomainModel> DeleteAuditorium(int Id)
        {
            var auditorium = await _auditoriumsRepository.GetByIdAsync(Id);
            if(auditorium == null)
            {
                return null;
            }

            var shows = await _showsRepository

            foreach (var show in auditorium.Shows)
            {
                await _showsRepository.Delete(show.Id);
            }

            var seats = await _seatsRepository.GetSeatsByAuditoriumId(auditorium.Id);

            foreach (var seat in seats)
            {
                await _seatsRepository.Delete(seat.Id);
            }
        }*/

        public async Task<IEnumerable<AuditoriumDomainModel>> GetAllAuditoriums()
        {
            var data = await _auditoriumsRepository.GetAllAsync();

            List<AuditoriumDomainModel> result = new List<AuditoriumDomainModel>();
            AuditoriumDomainModel model;
            foreach (var item in data)
            {
                model = new AuditoriumDomainModel
                {
                    Id = item.Id,
                    TheatreId = item.TheatreId,
                    Name = item.Name
                };
                result.Add(model);
            }

            return result;
        }

        public IEnumerable<AuditoriumDomainModel> GetAuditoriumsByCinemaId(int cinemaId)
        {
            var audits = _auditoriumsRepository.GetAuditoriumsByCinemaId(cinemaId);

            List<AuditoriumDomainModel> auditList = new List<AuditoriumDomainModel>();

            auditList = audits.Select(a => new AuditoriumDomainModel
            {
                TheatreId = a.TheatreId,
                Id = a.Id,
                Name = a.Name
            }).ToList();

            return auditList;
        }
    }
}
