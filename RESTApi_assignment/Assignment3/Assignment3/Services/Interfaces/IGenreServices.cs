using Assignment3.Models.DbModels;
using Assignment3.Models.Request;
using Assignment3.Models.Response;
using System.Collections.Generic;

namespace Assignment3.Services.Interfaces
{
    public interface IGenreServices
    {
        List<GenreResponse> GetAll();
        GenreResponse GetById(int id);
        int Create(GenreRequest request);
        void Update(int id,GenreRequest request);
        void Delete(int id);
        IEnumerable<int> CheckIdsExistInDatabase(string ids);

        List<GenreResponse> GetByGivenIds(string[] ids);
    }
}
