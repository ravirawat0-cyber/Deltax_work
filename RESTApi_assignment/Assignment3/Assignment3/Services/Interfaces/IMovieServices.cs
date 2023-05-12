using Assignment3.Models.DbModels;
using Assignment3.Models.Request;
using Assignment3.Models.Response;
using System.Collections.Generic;

namespace Assignment3.Services.Interfaces
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
