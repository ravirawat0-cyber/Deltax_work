using Assignment3.Models.Response;
using Assignment3.Models.Request;
using System.Collections.Generic;

namespace Assignment3.Services.Interfaces
{
    public interface IActorServices
    {
        List<ActorRespone>GetAll();
        ActorRespone GetById(int id);
        int Create(ActorRequest request);
        void Update(int id, ActorRequest request);
        void Delete(int id);
        IEnumerable<int> CheckIdsExistInDatabase(string ids);

        List<ActorRespone> GetByGivenIds(string[] ids);
    }

}
