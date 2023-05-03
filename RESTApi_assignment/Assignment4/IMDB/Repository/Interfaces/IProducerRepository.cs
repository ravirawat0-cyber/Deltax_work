using IMDB.Models.DbModels;
using System.Collections.Generic;

namespace IMDB.Repository.Interfaces
{
    public interface IProducerRepository
    {
        int Create(Producer entity);
        void Delete(int id);
        IEnumerable<Producer> GetAll();

        Producer GetById(int id);
        void Update(int id, Producer entity);
    }
}
