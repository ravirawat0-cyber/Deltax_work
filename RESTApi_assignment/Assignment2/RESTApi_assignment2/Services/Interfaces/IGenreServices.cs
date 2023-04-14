using RESTApi_assignment2.Models.DbModels;
using RESTApi_assignment2.Models.Request;
using RESTApi_assignment2.Models.Response;
using System.Collections.Generic;

namespace RESTApi_assignment2.Services.Interfaces
{
    public interface IGenreServices
    {
        List<GenreResponse> GetAll();
        GenreResponse GetById(int id);
        int Create(GenreRequest request);
        int Update(int id,GenreRequest request);
        void Delete(int id);
    }
}
