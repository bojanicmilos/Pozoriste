using Pozoriste.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pozoriste.Domain.Interfaces
{
    public interface IShowService
    {
        Task<IEnumerable<ShowPieceActorAuditoriumTheatreDomainModel>> GetAllShowsAsync();
        Task<ShowResultModel> AddShow(ShowDomainModel requestedShow);
        Task<ShowDomainModel> DeleteShow(int id);
        Task<IEnumerable<ShowDomainModel>> GetFutureShows();
        Task<IEnumerable<ShowDomainModel>> GetFutureShowsByPieceId(int id);
        Task<IEnumerable<ShowDomainModel>> GetFutureShowsByPieceId(PieceDomainModel domainModel);
    }
}
