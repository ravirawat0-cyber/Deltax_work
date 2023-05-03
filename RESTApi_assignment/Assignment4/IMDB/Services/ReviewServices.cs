using IMDB.Models.DbModels;
using IMDB.Models.Request;
using IMDB.Models.Response;
using IMDB.Repository;
using IMDB.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IMDB.Services
{
    public class ReviewServices : IReviewServices
    {
        private readonly ReviewRepository _reviewRepository;
        private readonly IMovieServices _movieServices;
        public ReviewServices(ReviewRepository reviewRepository,
                              IMovieServices movieServices
                              )
        {
           _reviewRepository = reviewRepository;
           _movieServices = movieServices;
        }

        public int Create(int movieId, ReviewRequest request)
        {
            CheckIfMovieExistsForReview(movieId);
            ValidateRequest(request);
            var review = new Review
            {
                MovieId = movieId,
                Message = request.Message
            };
            var id = _reviewRepository.Create(review);
            return id;
        }
        public void Delete(int movieId, int id)
        {
            CheckIfMovieExistsForReview(movieId);
            if (_reviewRepository.GetById(movieId, id) == null)
            {
                throw new KeyNotFoundException($"No review found with ID {id} and Movie ID {movieId}");
            }
            _reviewRepository.Delete(movieId, id);
        }

        public List<ReviewResponse> GetAll(int movieId)
        {
            CheckIfMovieExistsForReview(movieId);
            var reviewList = _reviewRepository.GetAll(movieId);
            if (reviewList.Count() == 0)
            {
                throw new KeyNotFoundException($"No review found for movie with id {movieId}");
            }
            var reviewResponseList = reviewList.Select(review => new ReviewResponse
            {
                Id = review.Id,
                Message = review.Message,
                MovieId = review.MovieId
            }).ToList();
            return reviewResponseList; 
        }

        public ReviewResponse GetById(int movieId, int id)
        {
            CheckIfMovieExistsForReview(movieId);
            var review = _reviewRepository.GetById(movieId, id);
            if (review == null)
            {
                throw new KeyNotFoundException($"Review with Id {id} and  MovieId {movieId} does not exist");
            }
            var reviewResponse = new ReviewResponse
            {
                Id = review.Id,
                Message = review.Message,
                MovieId = review.MovieId
            };
            return reviewResponse;
        }

        public void Update(int movieId, int id,  ReviewRequest request)
        {
            CheckIfMovieExistsForReview(movieId);
            ValidateRequest(request);
            var review = new Review
            {
                MovieId = movieId,
                Id = id,
                Message = request.Message,
            };
            _reviewRepository.Update(review);
        }

        private void CheckIfMovieExistsForReview(int movieId)
        {
            if (_movieServices.GetById(movieId) == null)
            {
                throw new KeyNotFoundException($"No movie present");
            }
        }
        private void ValidateRequest(ReviewRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Message))
            {
                throw new ArgumentException("Message cannot be empty or null");
            }
        }

    }
}
