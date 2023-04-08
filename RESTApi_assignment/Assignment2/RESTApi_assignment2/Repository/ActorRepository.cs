using RESTApi_assignment2.Models.DbModels;
using RESTApi_assignment2.Models.Response;
using RESTApi_assignment2.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace RESTApi_assignment2.Repository
{
    public class ActorRepository : IActorRepository
    {
        private readonly List<Actor> _actorsList = new List<Actor>();
        public void Create(Actor entity)
        {
            _actorsList.Add(entity);
        }

        public void Delete(int id)
        {
           var actor = GetById(id);
           _actorsList.Remove(actor);   
        }

        public List<Actor> GetAll()
        {
            return _actorsList;
        }

        public Actor GetById(int id)
        {
            return _actorsList.FirstOrDefault(a => a.Id == id);
        }

        public void Update(Actor entity)
        {
            var existingActor = GetById(entity.Id);
            _actorsList.Remove(existingActor);
            _actorsList.Add(entity);
            
        }
    }
}
