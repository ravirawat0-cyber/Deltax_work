using IMDB.Models.DbModels;
using IMDB.Models.Request;
using IMDB.Models.Response;
using System.Collections.Generic;

namespace IMDB.Services.Interfaces
{
    public interface IMovieServices
    {
        List<MovieResponse> GetAll();
        MovieResponse GetById(int id);
        int Create(MovieRequest request);
        void Update(int id, MovieRequest request);
        void Delete(int id);
    }
}
