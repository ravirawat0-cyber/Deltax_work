using IMDB.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Services.Interfaces
{
    public interface IProducerService
    {
        List<Producer> GetAllProducers();
        Producer GetProducerById(int id);
        void AddProducer(string name, string dateOfBirth);

    }
}
