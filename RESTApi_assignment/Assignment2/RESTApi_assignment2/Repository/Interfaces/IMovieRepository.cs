using RESTApi_assignment2.Models.DbModels;
using System.Collections.Generic;

namespace RESTApi_assignment2.Repository.Interfaces
{
    public interface IMovieRepository
    {
        List<Movie> GetAll();
        Movie GetById(int id);
        void Create(Movie entity);
        void Update(Movie entity);
        void Delete(int id);
    }
}
