using IMDB.Models.DbModels;
using System.Collections.Generic;

namespace IMDB.Repository.Interfaces
{
    public interface IActorRepository
    {
        IEnumerable<Actor> GetAll();
        Actor GetById(int id);
        int Create(Actor entity);
        void Delete(int id);
      
        void Update(int id, Actor entity);
        IEnumerable<int> CheckIdsExistInDatabase(List<string> ids);
        IEnumerable<Actor> GetByGivenIds(List<string> ids);


    }
}
