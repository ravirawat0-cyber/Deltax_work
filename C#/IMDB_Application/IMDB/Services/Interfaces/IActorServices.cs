using IMDB.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Services.Interfaces
{
    public interface IActorServices
    {
        List<Actor> GetAllActors();

        Actor GetActorById(int id);
        void AddActor(string name, string dateOfBirth);
    }
}
