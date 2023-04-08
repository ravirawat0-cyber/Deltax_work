using RESTApi_assignment2.Models.DbModels;
using System.Collections.Generic;

namespace RESTApi_assignment2.Repository.Interfaces
{
    public interface IReviewRepository
    {
        List<Review> GetAll();
        Review GetById(int id);
        void Create(Review entity);
        void Update(Review entity);
        void Delete(int id);
    }
}
