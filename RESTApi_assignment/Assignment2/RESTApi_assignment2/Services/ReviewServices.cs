using AutoMapper;
using RESTApi_assignment2.Helper;
using RESTApi_assignment2.Models.DbModels;
using RESTApi_assignment2.Models.Request;
using RESTApi_assignment2.Models.Response;
using RESTApi_assignment2.Repository.Interfaces;
using RESTApi_assignment2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RESTApi_assignment2.Services
{
    public class ReviewServices : IReviewServices
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IDataHelper _applicationData;
        private readonly IMapper _mapper;

        public ReviewServices(IReviewRepository reviewRepository,
                              IDataHelper applicationData,
                              IMapper mapper)
        {
           _reviewRepository = reviewRepository;
           _applicationData = applicationData;
            _mapper = mapper;
        }

        public int Create(int movieId, ReviewRequest request)
        {
            if (!(_applicationData.IsMoviePresent(movieId)))
            {
                throw new ArgumentException($"No movie present to review");
            }
            if (string.IsNullOrWhiteSpace(request.Message))
            {
                throw new ArgumentException("Message cannot be empty or null");
            }

            var review = new Review
            {
                Id = _reviewRepository.GetAll().Count + 1,
                MovieId = movieId,
                Message = request.Message
            };
            
            _reviewRepository.Create(review);
            return review.Id;
        }

        public void Delete(int movieId, int id)
        {
            if (!_applicationData.IsMoviePresent(movieId))
            {
                throw new ArgumentException($"No movie present to Delete");
            }

            var reviewToDelete = _reviewRepository.GetAll()
                .FirstOrDefault(r => r.MovieId == movieId && r.Id == id);

            if (reviewToDelete == null)
            {
                throw new ArgumentException($"No review found with ID {id} and Movie ID {movieId}");
            }

            _reviewRepository.Delete(reviewToDelete.Id);
        }

        public List<ReviewResponse> GetAll(int movieId)
        {
            if (!_applicationData.IsMoviePresent(movieId))
            {
                throw new ArgumentException($"Movie with id {movieId} does not exist");
            }

            var reviews = _reviewRepository.GetAll()
                            .Where(r => r.MovieId == movieId)
                            .ToList();

            if (reviews.Count == 0)
            {
                throw new ArgumentException($"There are no reviews for movie with id {movieId}");
            }
            return _mapper.Map<List<ReviewResponse>>(reviews);
        }

        public ReviewResponse GetById(int movieId, int id)
        {
            if (!_applicationData.IsMoviePresent(movieId))
            {
                throw new ArgumentException($"Movie with id {movieId} does not exist");
            }

            var review = _reviewRepository.GetAll()
                            .FirstOrDefault(r => r.MovieId == movieId && r.Id == id);

            if (review == null)
            {
                throw new ArgumentException($"No review with id {id} found for movie with id {movieId}");
            }

            return _mapper.Map<ReviewResponse>(review);
        }

        public int Update(int movieId, int id,  ReviewRequest request)
        {
            if (!_applicationData.IsMoviePresent(movieId))
            {
                throw new ArgumentException($"Movie with id {movieId} does not exist");
            }

            if (string.IsNullOrWhiteSpace(request.Message))
            {
                throw new ArgumentException("Message cannot be empty or null");
            }

            var review = _reviewRepository.GetAll()
                            .FirstOrDefault(r => r.MovieId == movieId && r.Id == id);
            if (review == null)
            {
                throw new ArgumentException($"No review with id {id} found for movie with id {movieId}");
            }

            review.Message = request.Message;
            _reviewRepository.Update(review);
            return review.Id;
        }
    }
}
