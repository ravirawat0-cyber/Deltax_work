using IMDB.Domain;
using IMDB.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Repository
{
    public class ActorRepository : IActorRepository
    {
        private List<Actor> _actorList = new List<Actor>()
        {
            new Actor(1, "Robert Downey Jr.", DateTime.Parse("03/15/1990")),
            new Actor(2, "Chris Hemsworth", DateTime.Parse("08/11/1983")),
            new Actor(3, "Will Smith", DateTime.Parse("09/25/1968")),
            new Actor(4, "Henry Cavill", DateTime.Parse("05/05/1983"))
        };
    

        public List<Actor> GetAllActors()
        {
            return _actorList;
        }

        public Actor GetActorById(int id)
        {
            return _actorList.FirstOrDefault(a => a.Id == id);
        }

        public void AddActor(Actor actor)
        {
            _actorList.Add(actor);
        }
    }
}
