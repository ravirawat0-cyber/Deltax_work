using IMDB.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Repository.Interfaces
{
    public interface IProducerRepository
    {
        List<Producer> GetAllProducers();
        Producer GetProducerById(int id);

        void AddProducer(Producer producer);

    }
}
