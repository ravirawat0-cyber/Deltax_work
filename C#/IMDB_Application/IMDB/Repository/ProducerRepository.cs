using IMDB.Domain;
using IMDB.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Repository
{
    public class ProducerRepository : IProducerRepository
    {
        private List<Producer> _producerList = new List<Producer>()
        {
            new Producer(1,"Kevin Feige", DateTime.Parse("06/02/1973")),
            new Producer(2, "Taika waititi" ,DateTime.Parse("08/16/1975")),
            new Producer(3, "Jhon salley", DateTime.Parse("05/16/1964")),
            new Producer(4, "Richard Lester", DateTime.Parse("01/19/1932"))
        };
        public List<Producer> GetAll()
        {
            return _producerList;
        }

        public Producer GetProducerById(int id)
        {
            return _producerList.FirstOrDefault(p => p.Id == id);
        }

        public void AddProducer(Producer producer)
        {
            _producerList.Add(producer);
        }
    }
}
