using RESTApi_assignment2.Models.DbModels;
using RESTApi_assignment2.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace RESTApi_assignment2.Repository
{
    public class ProducerRepository : IProducerRepository
    {
        private readonly List<Producer> _producerList = new List<Producer>();
        public void Create(Producer entity)
        {
            _producerList.Add(entity);
        }

        public void Delete(int id)
        {
            var producer = GetById(id);
            _producerList.Remove(producer);
        }

        public List<Producer> GetAll()
        {
            return _producerList;
        }

        public Producer GetById(int id)
        {
            return _producerList.FirstOrDefault(p => p.Id == id);
        }

        public void Update(Producer entity)
        {
            var existingProducer = GetById(entity.Id);
            _producerList.Remove(existingProducer);
            _producerList.Add(entity);
        }
    }
}
