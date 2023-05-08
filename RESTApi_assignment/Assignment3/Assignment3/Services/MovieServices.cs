using AutoMapper;
using Assignment3.Helper;
using Assignment3.Models.DbModels;
using Assignment3.Models.Request;
using Assignment3.Models.Response;
using Assignment3.Repository;
using Assignment3.Repository.Interfaces;
using Assignment3.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Assignment3.Services
{
    public class MovieServices: IMovieServices
    {
        private readonly MovieRepository _movieRepository;
        private readonly IActorServices _actorServices;
        private readonly IProducerServices _producerServices;
        private readonly IGenreServices _genreServices;
        private readonly IDataHelper _dataHelper;
        public MovieServices(MovieRepository movieRepository,
                             IActorServices actorServices,
                             IProducerServices producerServices,
                             IGenreServices genreServices,
                             IDataHelper dataHelper
                             )
        {
            _movieRepository = movieRepository;
            _actorServices = actorServices;
            _producerServices = producerServices;
            _genreServices = genreServices;
            _dataHelper = dataHelper;
        }

        public int Create(MovieRequest request)
        {
            ValidateRequest(request);
            var id = _movieRepository.Create(request);
            return id;
        }

        public void Delete(int id)
        {
            if (_movieRepository.GetById(id) == null )
            {
                throw new KeyNotFoundException($"Movie with ID {id} not found");
            }
            _dataHelper.DeleteFromActorMovieTable(id);
            _dataHelper.DeleteFromGenreMovieTable(id);
            _dataHelper.DeleteFromReviewsTable(id);
            _movieRepository.Delete(id);
        }
        public List<MovieResponse> GetAll()
        {

            var movieList = _movieRepository.GetAll();
            if (movieList == null)
            {
                return new List<MovieResponse>(); 
            }

            List<MovieResponse> movieResponseList = new List<MovieResponse>();
            foreach (var movie in movieList)
            {
                var movieResponse = new MovieResponse
                {
                    Id = movie.Id,
                    Name = movie.Name,
                    YearOfRelease = movie.YearOfRelease,
                    Plot = movie.Plot,
                    Producer = _producerServices.GetById(movie.ProducerId),
                    CoverImageUrl = movie.CoverImageUrl
                };
                PopulateActorsAndGenres(movie, movieResponse);
                movieResponseList.Add(movieResponse);
            }
            return movieResponseList;
        }

        private void PopulateActorsAndGenres(Movie movie, MovieResponse movieResponse)
        {
            string[] actorIds = _dataHelper.GetActorIdsFromActorMovieTable(movie.Id).ToArray();
            string[] genreIds = _dataHelper.GetGenreIdsFromGenreMovieTable(movie.Id).ToArray();
            movieResponse.Actors = new List<ActorRespone>();
            movieResponse.Genres = new List<GenreResponse>();
            movieResponse.Actors = _actorServices.GetByGivenIds(actorIds);
            movieResponse.Genres = _genreServices.GetByGivenIds(genreIds);
        }

        public MovieResponse GetById(int id)
        {
            var movie = _movieRepository.GetById(id);
            if (movie == null) 
            {
                return null;
            }
            var movieResponse = new MovieResponse
            {
                Id = movie.Id,
                Name = movie.Name,
                YearOfRelease = movie.YearOfRelease,
                Plot = movie.Plot,
                Producer = _producerServices.GetById(movie.ProducerId),
                CoverImageUrl = movie.CoverImageUrl
            };
            PopulateActorsAndGenres(movie, movieResponse);
            return movieResponse;
        }

        public void Update(int id, MovieRequest request)
        {
            ValidateRequest(request);
            _movieRepository.Update(id, request);
        }

        private void ValidateRequest(MovieRequest movie)
        {
            if (string.IsNullOrWhiteSpace(movie.Name))
            {
                throw new ArgumentException("Movie name cannot be null or empty");
            }

            if (movie.YearOfRelease <= 0)
            {
                throw new ArgumentException("Invalid year of release");
            }

            if (string.IsNullOrWhiteSpace(movie.Plot))
            {
                throw new ArgumentException("Movie plot cannot be null or empty");
            }

            if (string.IsNullOrWhiteSpace(movie.CoverImageUrl))
            {
                throw new ArgumentException("Cover image URL cannot be null or empty");
            }

            if (String.IsNullOrWhiteSpace(movie.ActorIds))
            {
                throw new ArgumentException("please enter actor Ids");
            }

            if (String.IsNullOrWhiteSpace(movie.GenreIds))
            {
                throw new ArgumentException("Please enter genre Ids");
            }
            var missingActorIds = _actorServices.CheckIdsExistInDatabase(movie.ActorIds);
            var missingGenreIds = _genreServices.CheckIdsExistInDatabase(movie.GenreIds);
            if (missingActorIds.Any())
            {
                var missingIdsStr = string.Join(",", missingActorIds);
                throw new KeyNotFoundException($"No Actor present with given Ids {missingIdsStr}.");
            }

            if (missingGenreIds.Any())
            {
                var missingIdsStr = string.Join(",", missingGenreIds);
                throw new KeyNotFoundException($"No Genre present with given Ids {missingIdsStr}.");
            }
            if (_producerServices.GetById(movie.ProducerId) == null)
            {
                throw new KeyNotFoundException($"No Producer present with given Id {movie.ProducerId}");
            }

        }
    }   
}

