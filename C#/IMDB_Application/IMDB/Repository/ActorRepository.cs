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
        private List<Actor> _actorList = new List<Actor>();
        // {
        //     new Actor{Id = 1, Name = "Robert Downey Jr.", DateOfBirth = DateTime.Parse("03/15/1990")},
        //     new Actor{Id = 2, Name = "Chris Hemsworth",DateOfBirth = DateTime.Parse("08/11/1983")},
        //     new Actor{Id = 3,Name = "Will Smith",DateOfBirth = DateTime.Parse("09/25/1968")},
        //     new Actor{Id = 4, Name = "Henry Cavill", DateOfBirth = DateTime.Parse("05/05/1983")}
        // };
    

        public List<Actor> GetAll()
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
