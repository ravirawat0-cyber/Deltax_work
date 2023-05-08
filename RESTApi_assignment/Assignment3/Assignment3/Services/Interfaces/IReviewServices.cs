using Assignment3.Models.DbModels;
using Assignment3.Models.Request;
using Assignment3.Models.Response;
using System.Collections.Generic;

namespace Assignment3.Services.Interfaces
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
