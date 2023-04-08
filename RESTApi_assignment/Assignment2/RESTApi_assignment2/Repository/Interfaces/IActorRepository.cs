using RESTApi_assignment2.Models.DbModels;
using System.Collections.Generic;

namespace RESTApi_assignment2.Repository.Interfaces
{
    public interface IActorRepository
    {
        List<Actor> GetAll();
        Actor GetById(int id);
        void Create(Actor entity);
        void Update(Actor entity);
        void Delete(int id);
    }
}
