using AutoMapper;
using IMDB.Models.DbModels;
using IMDB.Models.Request;
using IMDB.Models.Response;
using IMDB.Repository;
using IMDB.Repository.Interfaces;
using IMDB.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;
using IMDB.Enums;
using SystemSqlException = System.Data.SqlClient.SqlException;

namespace IMDB.Services
{
    public class GenreServices : IGenreServices
    {
        private readonly IGenreRepository _genreRepository;

        public GenreServices(IGenreRepository genreRepository)
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
                throw new KeyNotFoundException($"Genre with ID {id} not found");
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
                throw new KeyNotFoundException($"There is no Genre to Update with Id {id}");
            }
            ValidateRequest(request);
            genre.Name = request.Name;
            _genreRepository.Update(id, genre);
            
        }

        public IEnumerable<int> CheckIdsExistInDatabase(List<string> ids)
        {
            return _genreRepository.CheckIdsExistInDatabase(ids);
        }

        public List<GenreResponse> GetByGivenIds(List<string> ids)
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
