using Pozoriste.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pozoriste.Domain.Interfaces
{
    public interface IActorService
    {
        Task<IEnumerable<ActorDomainModel>> GetAllAsync();
        Task<ActorDomainModel> GetByIdAsync(int id);
        Task<ActorDomainModel> AddActor(ActorDomainModel actorModel);
        Task<ActorDomainModel> DeleteActor(int id);

    }
}
