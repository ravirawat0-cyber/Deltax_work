using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMDB.Domain;
using IMDB.Repository;
using IMDB.Repository.Interfaces;
using IMDB.Services.Interfaces;

namespace IMDB.Services
{
    public class ActorServices: IActorServices
    {
        private readonly IActorRepository _actorRepository;

        public ActorServices()
        {
            _actorRepository = new ActorRepository();
        }
        public List<Actor> GetAllActors()
        {
            return _actorRepository.GetAll();
        }

        public Actor GetActorById(int id)
        {
            return _actorRepository.GetActorById(id);
        }

        public void AddActor(string name, string dateOfBirth)
        {
            var actor = new Actor();
            actor.Id = _actorRepository.GetAll().Max(a => a.Id) + 1;
            actor.Name = name;
            actor.DateOfBirth = DateTime.Parse(dateOfBirth);
            _actorRepository.AddActor(actor);
        }
    }
}
