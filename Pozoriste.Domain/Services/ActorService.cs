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
    public class ActorService : IActorService
    {
        private readonly IActorsRepository _actorsRepository;

        public ActorService(IActorsRepository actorsRepository)
        {
            _actorsRepository = actorsRepository;
        }

        public async Task<IEnumerable<ActorDomainModel>> GetAllAsync()
        {
            var actors = await _actorsRepository.GetAllAsync();

            if (actors.Count() == 0)
            {
                return null;
            }

            IEnumerable<ActorDomainModel> actorDomainModels = actors.Select(actor => new ActorDomainModel
            {
                Id = actor.Id,
                FirstName = actor.FirstName,
                LastName = actor.LastName
            });

            return actorDomainModels;
        }

        public async Task<ActorDomainModel> GetByIdAsync(int id)
        {
            var actor = await _actorsRepository.GetByIdAsync(id);

            if (actor == null)
            {
                return null;
            }

            ActorDomainModel actorDomainModel = new ActorDomainModel
            {
                Id = actor.Id,
                FirstName = actor.FirstName,
                LastName = actor.LastName
            };

            return actorDomainModel;
        }

        public async Task<ActorDomainModel> AddActor(ActorDomainModel actorModel)
        {
            // get all actors
            var actors = await _actorsRepository.GetAllAsync();

            // check if there is actor with same first name and last name 
            foreach(var actor in actors)
            {
                if (actor.FirstName == actorModel.FirstName && actor.LastName == actorModel.LastName)
                {
                    return null;
                }
            }

            Actor actorToAdd = new Actor
            {
                FirstName = actorModel.FirstName,
                LastName = actorModel.LastName
            };

            var addedActor = _actorsRepository.Insert(actorToAdd);

            _actorsRepository.Save();

            ActorDomainModel actorDomainModel = new ActorDomainModel
            {
                Id = addedActor.Id,
                FirstName = addedActor.FirstName,
                LastName = addedActor.LastName
            };

            return actorDomainModel;
        }
    }
}
