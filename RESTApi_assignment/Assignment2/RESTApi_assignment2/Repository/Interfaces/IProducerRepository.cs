using RESTApi_assignment2.Models.DbModels;
using System.Collections.Generic;

namespace RESTApi_assignment2.Repository.Interfaces
{
    public interface IProducerRepository
    {
        List<Producer> GetAll();
        Producer GetById(int id);
        void Create(Producer entity);
        void Update(Producer entity);
        void Delete(int id);
    }
}
