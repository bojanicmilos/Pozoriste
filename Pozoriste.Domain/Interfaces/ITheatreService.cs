using Pozoriste.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pozoriste.Domain.Interfaces
{
    public interface ITheatreService
    {
        Task<IEnumerable<TheatreDomainModel>> GetAllAsync();
        Task<TheatreDomainModel> Create(TheatreDomainModel domainModel, int numOfSeats, int numOfRows, string auditoriumName);
        Task<TheatreDomainModel> Delete(int id);
    }
}
