using IMDB.Models.Response;
using IMDB.Models.Request;
using System.Collections.Generic;

namespace IMDB.Services.Interfaces
{
    public interface IActorServices
    {
        List<ActorResponse>GetAll();
        ActorResponse GetById(int id);
        int Create(ActorRequest request);
        void Update(int id, ActorRequest request);
        void Delete(int id);
        IEnumerable<int> CheckIdsExistInDatabase(List<string> ids);
        List<ActorResponse> GetByGivenIds(List<string> ids);
    }

}
