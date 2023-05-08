using AutoMapper;
using Assignment3.Models.DbModels;
using Assignment3.Models.Request;
using Assignment3.Models.Response;
using Assignment3.Repository;
using Assignment3.Repository.Interfaces;
using Assignment3.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;
using Assignment3.Enums;
using SystemSqlException = System.Data.SqlClient.SqlException;

namespace Assignment3.Services
{
    public class GenreServices : IGenreServices
    {
        private readonly GenreRepository _genreRepository;

        public GenreServices(GenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public int Create(GenreRequest request)
        {
            ValidateRequest(request);
            var genre = new Genre
            {
                Name = request.Name
            };
            var id = _genreRepository.Create(genre);
            return id;
        }

        public void Delete(int id)
        {
            if (_genreRepository.GetById(id) == null)
            {
                throw new KeyNotFoundException($"Genre with ID {id} not found");
            }
            try
            {
                _genreRepository.Delete(id);
            }
            catch (SystemSqlException ex)
            {
                if (ex.Number == (int)SqlErrorNumber.ForeignKeyViolation)
                {
                    throw new CustomExceptions.SqlException("Cannot delete this Genre record because it is referenced by another table.");
                }
                else
                {
                    throw;
                }
            }
        }

        public List<GenreResponse> GetAll()
        {
            var genrelist = _genreRepository.GetAll();
            if (genrelist == null)
            {
               return new List<GenreResponse>();
            }
            var genreResponseList = genrelist.Select(genre => new GenreResponse
            {
                Id = genre.Id,
                Name = genre.Name
            }).ToList();
            return genreResponseList;
        }

        public GenreResponse GetById(int id)
        {
            var genre = _genreRepository.GetById(id);
            if (genre == null)
            {
                return null;
            }
            var genreResponse = new GenreResponse
            {
                Id = genre.Id,
                Name = genre.Name
            };
            return genreResponse;
        }

        public void Update(int id, GenreRequest request)
        {
            var genre = _genreRepository.GetById(id);
            if (genre == null)
            {
                throw new KeyNotFoundException($"Genre with {id} not found");
            }
            ValidateRequest(request);
            genre.Name = request.Name;
            _genreRepository.Update(id, genre);
            
        }

        public IEnumerable<int> CheckIdsExistInDatabase(string ids)
        {
            return _genreRepository.CheckIdsExistInDatabase(ids);
        }

        public List<GenreResponse> GetByGivenIds(string[] ids)
        {
            var genrelist = _genreRepository.GetByGivenIds(ids);
            if (genrelist == null)
            {
                return new List<GenreResponse>();
            }
            var genreResponseList = genrelist.Select(genre => new GenreResponse
            {
                Id = genre.Id,
                Name = genre.Name
            }).ToList();
            return genreResponseList;
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
