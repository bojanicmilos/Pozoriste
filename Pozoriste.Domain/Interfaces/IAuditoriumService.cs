using Pozoriste.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pozoriste.Domain.Interfaces
{
    public interface IAuditoriumService
    {
        Task<IEnumerable<AuditoriumDomainModel>> GetAllAuditoriums();
        Task<CreateAuditoriumResultModel> AddAuditorium(AuditoriumDomainModel domainModel, int numberOfRows, int numberOfSeats);
        Task<DeleteAuditoriumResultModel> DeleteAuditorium(int Id);
        IEnumerable<AuditoriumDomainModel> GetAuditoriumsByCinemaId(int cinemaId);
    }
}
