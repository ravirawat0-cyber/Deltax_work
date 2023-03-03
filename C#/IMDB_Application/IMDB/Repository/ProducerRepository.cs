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
        private List<Producer> _producerList = new List<Producer>();
        // {
        //     new Producer{ Id = 1,Name = "Kevin Feige",DateOfBirth = DateTime.Parse("06/02/1973")},
        //     new Producer{ Id = 2, Name = "Taika waititi", DateOfBirth = DateTime.Parse("08/16/1975") },
        //     new Producer{ Id = 3, Name = "Jhon salley", DateOfBirth = DateTime.Parse("05/16/1964") },
        //     new Producer{ Id = 4, Name = "Richard Lester", DateOfBirth = DateTime.Parse("01/19/1932") }
        // };
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
