using RESTApi_assignment2.Models.DbModels;
using RESTApi_assignment2.Models.Request;
using RESTApi_assignment2.Models.Response;
using System.Collections.Generic;

namespace RESTApi_assignment2.Services.Interfaces
{
    public interface IMovieServices
    {
        List<MovieResponse> GetAll();
        MovieResponse GetById(int id);
        int Create(MovieRequest request);
        int Update(int id, MovieRequest request);
        void Delete(int id);
    }
}
