using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMDB.Domain;
using IMDB.Repository;
using IMDB.Repository.Interfaces;
using IMDB.Services.Interfaces;

namespace IMDB.Services
{
    public class ProducerServices: IProducerService
    {
        private readonly IProducerRepository _producerRepository;

        public ProducerServices()
        {
            _producerRepository = new ProducerRepository();
        }
        public List<Producer> GetAllProducers()
        {
            return _producerRepository.GetAll();
        }

        public Producer GetProducerById(int id)
        {
            return _producerRepository.GetProducerById(id);
        }

        public void AddProducer(string name, string dateOfBirth)
        {
            if (string.IsNullOrEmpty(name)) throw new Exception("Actor name cannot be empty ");
            if (string.IsNullOrEmpty(dateOfBirth)) throw new Exception("Date of cannot be empty");
            var parameterDate = DateTime.ParseExact(dateOfBirth, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            if (int.TryParse(name, out int valid)) throw new AggregateException("name should not contain integer value");
            if (parameterDate > DateTime.Today) throw new ArgumentNullException("Producer DOB cannot be  greater than current date");

            var producer = new Producer();
            var producerList = _producerRepository.GetAll();
            producer.Id = producerList.Any() ? producerList.Max(a => a.Id) + 1 : 1;
   
            producer.Name = name;
            producer.DateOfBirth = DateTime.Parse(dateOfBirth);
            _producerRepository.AddProducer(producer);
        }
    }
}
