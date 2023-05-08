using Assignment3.Models.Request;
using Assignment3.Models.Response;
using System.Collections.Generic;

namespace Assignment3.Services.Interfaces
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
