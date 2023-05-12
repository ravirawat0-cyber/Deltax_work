using IMDB.Models.Request;
using IMDB.Models.Response;
using System.Collections.Generic;

namespace IMDB.Services.Interfaces
{
    public interface IProducerServices
    {
        List<ProducerRespone> GetAll();
        ProducerRespone GetById(int id);
        int Create(ProducerRequest request);
        void Update(int id, ProducerRequest request);
        void Delete(int id);
    }
}
