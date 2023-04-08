using RESTApi_assignment2.Models.Response;
using RESTApi_assignment2.Models.Request;
using System.Collections.Generic;

namespace RESTApi_assignment2.Services.Interfaces
{
    public interface IActorServices
    {
        List<ActorRespone>GetAll();
        ActorRespone GetById(int id);
        int Create(ActorRequest request);
        int Update(int id, ActorRequest request);
        void Delete(int id);
    }
}
