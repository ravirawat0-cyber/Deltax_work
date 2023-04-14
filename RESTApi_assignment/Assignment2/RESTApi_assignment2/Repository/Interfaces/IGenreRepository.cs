using RESTApi_assignment2.Models.DbModels;
using System.Collections.Generic;

namespace RESTApi_assignment2.Repository.Interfaces
{
    public interface IGenreRepository
    {
        List<Genre> GetAll();
        Genre GetById(int id);
        void Create(Genre entity);
        void Update(Genre entity);
        void Delete(int id);
    }
}
