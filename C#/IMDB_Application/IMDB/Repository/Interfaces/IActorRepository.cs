using IMDB.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Repository.Interfaces
{
    public interface IActorRepository
    {
        List<Actor> GetAll();
        Actor GetActorById(int id);
        void AddActor(Actor actor);
    }
}
