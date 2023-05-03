using IMDB.Models.DbModels;
using IMDB.Models.Request;
using System.Collections.Generic;

namespace IMDB.Repository.Interfaces
{
    public interface IMovieRepository
    {
        int Create(MovieRequest entity);
        void Delete(int id);
        IEnumerable<Movie> GetAll();
        Movie GetById(int id);
        void Update(int id, MovieRequest entity);
    }
}
