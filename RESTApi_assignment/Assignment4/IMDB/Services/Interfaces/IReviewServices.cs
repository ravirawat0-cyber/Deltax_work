using IMDB.Models.DbModels;
using IMDB.Models.Request;
using IMDB.Models.Response;
using System.Collections.Generic;

namespace IMDB.Services.Interfaces
{
    public interface IReviewServices
    {
        List<ReviewResponse> GetAll(int movieId);
        ReviewResponse GetById(int movieId, int id);
        int Create(int movieId, ReviewRequest request);
        void  Update(int movieId, int id, ReviewRequest request);
        void Delete(int movieId, int id);
    }
}
