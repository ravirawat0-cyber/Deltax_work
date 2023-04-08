using AutoMapper;
using RESTApi_assignment2.Helper;
using RESTApi_assignment2.Models.DbModels;
using RESTApi_assignment2.Models.Request;
using RESTApi_assignment2.Models.Response;
using RESTApi_assignment2.Repository;
using RESTApi_assignment2.Repository.Interfaces;
using RESTApi_assignment2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RESTApi_assignment2.Services
{
    public class MovieServices: IMovieServices
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IActorServices _actorServices;
        private readonly IProducerServices _producerServices;
        private readonly IGenreServices _genreServices;
        private readonly IDataHelper _dataHelper;
        private readonly IMapper _mapper;

        public MovieServices(IMovieRepository movieRepository,
                             IActorServices actorServices,
                             IProducerServices producerServices,
                             IGenreServices genreServices,
                             IDataHelper dataHelper, 
                             IMapper mapper)
        {
            _movieRepository = movieRepository;
            _actorServices = actorServices;
            _producerServices = producerServices;
            _genreServices = genreServices;
            _dataHelper = dataHelper;
            _mapper = mapper;
        }

        public int Create(MovieRequest request)
        {
            ValidateRequest(request);

            var movie = _mapper.Map<Movie>(request);
            movie.Id = _movieRepository.GetAll().Count + 1;
            movie.ProducerId = request.ProducerId;

            _dataHelper.AddToActorsMoviesDict(movie.Id, request.ActorIds);
            _dataHelper.AddToGenresMoviesDict(movie.Id, request.GenreIds);  
            _dataHelper.AddToMovieIdList(movie.Id);

            _movieRepository.Create(movie);
            return movie.Id;    
        }

        public void Delete(int id)
        {

            if (_movieRepository.GetById(id) == null)
            {
                throw new ArgumentException($"Movie with ID {id} not found");
            }
            _dataHelper.DeleteRelatonHelpers(id);
            _movieRepository.Delete(id);
        }
        public List<MovieResponse> GetAll()
        {
            var movieList = _movieRepository.GetAll();
            if (movieList == null)
            {
                throw new ArgumentException($"There is no movie stored");
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
                
                List<int> actorIds = _dataHelper.GetValueFromActorsMoviesDict(movie.Id);
                List<int> genreIds = _dataHelper.GetValueFromGenresMoviesDict(movie.Id);

                movieResponse.Actors = new List<ActorRespone>();
                movieResponse.Genres = new List<GenreResponse>();
                movieResponse.Actors = actorIds.Select(actorId => _actorServices.GetById(actorId)).ToList();
                movieResponse.Genres = genreIds.Select(genreId => _genreServices.GetById(genreId)).ToList();
                movieResponseList.Add(movieResponse);
            }
            return movieResponseList;
        }

        public MovieResponse GetById(int id)
        {
            var movie = _movieRepository.GetById(id);
            if (movie == null) 
            {
                throw new ArgumentException($"Movie with ID {id} not found");
            }
            var movieResponse = _mapper.Map<MovieResponse>(movie);
            movieResponse.Producer = _producerServices.GetById(movie.ProducerId);

            List<int> actorIds = _dataHelper.GetValueFromActorsMoviesDict(movie.Id);
            List<int> genreIds = _dataHelper.GetValueFromGenresMoviesDict(movie.Id);

            movieResponse.Actors = new List<ActorRespone>();
            movieResponse.Genres = new List<GenreResponse>();
            movieResponse.Actors = actorIds.Select(actorId => _actorServices.GetById(actorId)).ToList();
            movieResponse.Genres = genreIds.Select(genreId => _genreServices.GetById(genreId)).ToList();
            return movieResponse;
        }

        public int Update(int id, MovieRequest request)
        {
            var movie = _movieRepository.GetById(id);
            if (movie == null)
            {
                throw new ArgumentException($"Movie with ID {id} not found");
            }
            ValidateRequest(request);

            var updatemovie = new Movie
            {
                Id = movie.Id,
                Name = request.Name,
                YearOfRelease = request.YearOfRelease,
                Plot = request.Plot,
                ProducerId = request.ProducerId,
                CoverImageUrl = request.CoverImageUrl    
            };
            _dataHelper.AddToActorsMoviesDict(movie.Id, request.ActorIds);
            _dataHelper.AddToGenresMoviesDict(movie.Id, request.GenreIds);
            _movieRepository.Update(updatemovie);
            return movie.Id;
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

            if (movie.ActorIds == null || movie.ActorIds.Count == 0)
            {
                throw new ArgumentException("At least one actor must be associated with the movie");
            }

            if (movie.GenreIds == null || movie.GenreIds.Count == 0)
            {
                throw new ArgumentException("At least one genre must be associated with the movie");
            }

            foreach (var actorId in movie.ActorIds)
            {
                if (_actorServices.GetById(actorId) == null)
                {
                    throw new ArgumentException($"Invalid actor ID: {actorId}");
                }
            }

            foreach (var genreId in movie.GenreIds)
            {
                if (_genreServices.GetById(genreId) == null)
                {
                    throw new ArgumentException($"Invalid genre ID: {genreId}");
                }
            }

            if (_producerServices.GetById(movie.ProducerId) == null)
            {
                throw new ArgumentException($"Invalid producer ID: {movie.ProducerId}");
            }
        }
    }   
}

