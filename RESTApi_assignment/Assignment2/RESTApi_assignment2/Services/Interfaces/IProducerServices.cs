using RESTApi_assignment2.Models.Request;
using RESTApi_assignment2.Models.Response;
using System.Collections.Generic;

namespace RESTApi_assignment2.Services.Interfaces
{
    public interface IProducerServices
    {
        List<ProducerRespone> GetAll();
        ProducerRespone GetById(int id);
        int Create(ProducerRequest request);
        int Update(int id, ProducerRequest request);
        void Delete(int id);
    }
}
