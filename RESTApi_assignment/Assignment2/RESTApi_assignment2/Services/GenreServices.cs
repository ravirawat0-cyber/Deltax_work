using AutoMapper;
using RESTApi_assignment2.Models.DbModels;
using RESTApi_assignment2.Models.Request;
using RESTApi_assignment2.Models.Response;
using RESTApi_assignment2.Repository;
using RESTApi_assignment2.Repository.Interfaces;
using RESTApi_assignment2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace RESTApi_assignment2.Services
{
    public class GenreServices : IGenreServices
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GenreServices(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public int Create(GenreRequest request)
        {
            ValidateRequest(request);
            var genre = _mapper.Map<Genre>(request);
            genre.Id = _genreRepository.GetAll().Count + 1;
            _genreRepository.Create(genre);
            return genre.Id;
        }

        public void Delete(int id)
        {
            if (_genreRepository.GetById(id) == null)
            {
                throw new KeyNotFoundException($"Genre with ID {id} not found");
            }
            _genreRepository.Delete(id);
        }

        public List<GenreResponse> GetAll()
        {
            var genrelist = _genreRepository.GetAll();
            if (genrelist == null)
            {
               return new List<GenreResponse>();
            }
            return _mapper.Map<List<GenreResponse>>(genrelist);
        }

        public GenreResponse GetById(int id)
        {
            var genre = _genreRepository.GetById(id);
            if (genre == null)
            {
                throw new KeyNotFoundException($"Genre with ID {id} not found");
            }
            return _mapper.Map<GenreResponse>(genre);
        }

        public int Update(int id, GenreRequest request)
        {
            var genre = _genreRepository.GetById(id);
            if (genre == null)
            {
                throw new KeyNotFoundException($"Genre with {id} not found");
            }
            ValidateRequest(request);
            genre.Name = request.Name;
            _genreRepository.Update(genre);
            return genre.Id;
        }

        private void ValidateRequest(GenreRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ArgumentException("Name cannot be empty or null");
            }
        }
    }
}
