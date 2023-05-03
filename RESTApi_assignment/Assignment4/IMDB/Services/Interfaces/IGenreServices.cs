using IMDB.Models.DbModels;
using IMDB.Models.Request;
using IMDB.Models.Response;
using System.Collections.Generic;

namespace IMDB.Services.Interfaces
{
    public interface IGenreServices
    {
        List<GenreResponse> GetAll();
        GenreResponse GetById(int id);
        int Create(GenreRequest request);
        void Update(int id,GenreRequest request);
        void Delete(int id);
        IEnumerable<int> CheckIdsExistInDatabase(List<string> ids);

        List<GenreResponse> GetByGivenIds(List<string> ids);
    }
}
