using IMDB.Models.DbModels;
using System.Collections.Generic;

namespace IMDB.Repository.Interfaces
{
    public interface IGenreRepository
    {
        int Create(Genre entity);
        void Delete(int id);
        IEnumerable<Genre> GetAll();
        Genre GetById(int id);
        void Update(int id, Genre entity);
        IEnumerable<int> CheckIdsExistInDatabase(List<string> ids);
        IEnumerable<Genre> GetByGivenIds(List<string> ids);
    }
}
