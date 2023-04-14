using RESTApi_assignment2.Models.DbModels;
using RESTApi_assignment2.Models.Request;
using RESTApi_assignment2.Models.Response;
using System.Collections.Generic;

namespace RESTApi_assignment2.Services.Interfaces
{
    public interface IReviewServices
    {
        List<ReviewResponse> GetAll(int movieId);
        ReviewResponse GetById(int movieId, int id);
        int Create(int movieId, ReviewRequest request);
        int  Update(int movieId, int id, ReviewRequest request);
        void Delete(int movieId, int id);
    }
}
