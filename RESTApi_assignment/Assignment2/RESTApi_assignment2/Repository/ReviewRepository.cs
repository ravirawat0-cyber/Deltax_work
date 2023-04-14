using RESTApi_assignment2.Models.DbModels;
using RESTApi_assignment2.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace RESTApi_assignment2.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly List<Review> _reviewList = new List<Review>();
        public void Create(Review entity)
        {
            _reviewList.Add(entity);
        }

        public void Delete(int id)
        {
            var review = GetById(id);
            _reviewList.Remove(review);
        }

        public List<Review> GetAll()
        {
            return _reviewList;
        }

        public Review GetById(int id)
        {
            return _reviewList.FirstOrDefault(r => r.Id == id);
        }

        public void Update(Review entity)
        {
            var existingReview = GetById(entity.Id);
                _reviewList.Remove(existingReview); 
                _reviewList.Add(entity);
        }
    }
}
