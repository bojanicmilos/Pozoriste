using Pozoriste.Data.Entities;
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
    public class TheatreService : ITheatreService
    {
        private readonly ITheatreRepository _theatreRepository;
        private readonly IAuditoriumsRepository _auditoriumsRepository;
        private readonly ISeatsRepository _seatsRepository;
        private readonly IAddressesRepository _addressesRepository;

        public TheatreService(ITheatreRepository theatreRepository, IAuditoriumsRepository auditoriumsRepository, ISeatsRepository seatsRepository, IAddressesRepository addressesRepository)
        {
            _theatreRepository = theatreRepository;
            _auditoriumsRepository = auditoriumsRepository;
            _seatsRepository = seatsRepository;
            _addressesRepository = addressesRepository;
        }

        public async Task<TheatreDomainModel> Create(TheatreDomainModel theatreDomainModel, int numOfSeats, int numOfRows, string auditoriumName)
        {
            var address = await _addressesRepository.GetByIdAsync(theatreDomainModel.AddressId);

            if(address == null)
            {
                return null;
            }

            var theatre = await _theatreRepository.GetAllAsync();
            var theatreOnSameAddress = theatre.Where(x => x.AddressId == theatreDomainModel.AddressId);

            foreach(var thea in theatreOnSameAddress)
            {
                if(thea.Name == theatreDomainModel.Name)
                {
                    return null;
                }
            }

            Theatre newTheatre = new Theatre
            {
                Name = theatreDomainModel.Name,
                AddressId = theatreDomainModel.AddressId
            };

            newTheatre.Auditoriums = new List<Auditorium>();
            Auditorium auditorium = new Auditorium
            {
                Name = auditoriumName
            };

            auditorium.Seats = new List<Seat>();

            for(int j = 1; j <= numOfRows; j++)
            {
                for(int k = 1; k <= numOfSeats; k++)
                {
                    Seat seat = new Seat
                    {
                        Row = j,
                        Number = k
                    };

                    auditorium.Seats.Add(seat);
                }
            }

            newTheatre.Auditoriums.Add(auditorium);

            Theatre insertedTheatre = _theatreRepository.Insert(newTheatre);

            if(insertedTheatre == null)
            {
                return null;
            }

            _theatreRepository.Save();

            if(insertedTheatre == null)
            {
                return null;
            }

            TheatreDomainModel theatreModel = new TheatreDomainModel
            {
                Id = insertedTheatre.Id,
                Name = insertedTheatre.Name,
                AddressId = insertedTheatre.AddressId,
                AuditoriumsList = new List<AuditoriumDomainModel>()
            };

            foreach(var auditoriumInserted in insertedTheatre.Auditoriums)
            {
                AuditoriumDomainModel modelAuditoroum = new AuditoriumDomainModel
                {
                    Id = auditoriumInserted.Id,
                    TheatreId = insertedTheatre.Id,
                    Name = auditoriumInserted.Name,
                    SeatsList = new List<SeatDomainModel>()
                };

                foreach (var seat in auditoriumInserted.Seats)
                {
                    modelAuditoroum.SeatsList.Add(new SeatDomainModel
                    {
                        Id = seat.Id,
                        AuditoriumId = auditoriumInserted.Id,
                        Number = seat.Number,
                        Row = seat.Row
                    });
                }

                theatreModel.AuditoriumsList.Add(modelAuditoroum);

            }

            return theatreModel;
        }

        public async Task<TheatreDomainModel> Delete(int id)
        {
            var theatre = await _theatreRepository.GetByIdAsync(id);

            if(theatre == null)
            {
                return null;
            }

            var allAuditoriums = await _auditoriumsRepository.GetAllAsync();

            if(allAuditoriums == null)
            {
                return null;
            }

            var auditoriumsInTheatre = allAuditoriums.Where(x => x.TheatreId == id);

            var seats = await _seatsRepository.GetAllAsync();

            foreach(var auditorium in auditoriumsInTheatre)
            {
                seats = seats.Where(x => x.AuditoriumId == auditorium.Id);

                foreach(var seat in seats)
                {
                    if(seat.ReservationSeats.Any())
                    {
                        return null;
                    }
                    //dodat await
                    await _seatsRepository.Delete(seat.Id);
                }
                //dodat await
                await _auditoriumsRepository.Delete(auditorium.Id);
            }

            TheatreDomainModel theatreModel = new TheatreDomainModel
            {
                Id = theatre.Id,
                Name = theatre.Name,
                AddressId = theatre.AddressId,
                AuditoriumsList = theatre.Auditoriums.Select(x => new AuditoriumDomainModel
                {
                    Id = x.Id,
                    TheatreId = x.TheatreId,
                    Name = x.Name,
                    SeatsList = x.Seats.Select(x => new SeatDomainModel
                    {
                        Id = x.Id,
                        AuditoriumId = x.AuditoriumId,
                        Number = x.Number,
                        Row = x.Row
                    })
                    .ToList()
                })
                .ToList()
            };
            //dodat await
            await _theatreRepository.Delete(theatre.Id);

            _theatreRepository.Save();

            return theatreModel;
        }

        public Task<IEnumerable<TheatreDomainModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
