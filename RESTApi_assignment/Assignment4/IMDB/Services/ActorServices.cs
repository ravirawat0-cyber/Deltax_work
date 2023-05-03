using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using IMDB.Models.DbModels;
using IMDB.Models.Request;
using IMDB.Models.Response;
using IMDB.Repository;
using IMDB.Repository.Interfaces;
using IMDB.Services.Interfaces;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;
using System.Linq;
using System.Collections;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using IMDB.CustomExceptions;
using Microsoft.Extensions.Logging;
using IMDB.Enums;
using System.Reflection.PortableExecutable;
using SystemSqlException = System.Data.SqlClient.SqlException;

namespace IMDB.Services
{
    public class ActorServices : IActorServices
    {
        private readonly IActorRepository _actorRepository;
        public ActorServices(IActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
        }
        public int Create(ActorRequest request)
        {
            ValidateRequest(request);
            var actor = new Models.DbModels.Actor 
            {
                Name = request.Name,
                Bio = request.Bio,
                Sex = request.Sex,
                DOB = request.DOB
            };
            var id = _actorRepository.Create(actor);
            return id;      
        }

        public void Delete(int id)
        {
            if (_actorRepository.GetById(id) == null)
            {
                throw new KeyNotFoundException($"No Actor to Delete with given ID {id}");
            }
            try
            {
                _actorRepository.Delete(id);
            }
            catch (SystemSqlException ex)
            {
                if (ex.Number == (int)SqlErrorNumber.ForeignKeyViolation)
                {
                    throw new CustomExceptions.SqlException("Cannot delete this Actor record because it is referenced by another table");
                }
                else
                {
                    throw;
                }
            }
        }

        public List<Models.Response.ActorResponse> GetAll()
        {
            var actorlist = _actorRepository.GetAll();
            if (actorlist == null)
            {
               return new List<Models.Response.ActorResponse>();
            }   
            var actorResponseList = actorlist.Select(actor => new ActorResponse
            {
                Id = actor.Id,
                Name = actor.Name,
                Bio = actor.Bio,
                Sex = actor.Sex,
                DOB = actor.DOB
            }).ToList();
            return actorResponseList;
        }

        public Models.Response.ActorResponse GetById(int id)
        {
            var actor = _actorRepository.GetById(id);
            if (actor == null)
            {
                throw new KeyNotFoundException($"Actor with ID {id} not found");
            }
            var actorResponse = new Models.Response.ActorResponse
            {
                Id = actor.Id,
                Name = actor.Name,
                Bio = actor.Bio,
                Sex = actor.Sex,
                DOB = actor.DOB
            };
            return actorResponse;
        }

        public void Update(int id, ActorRequest request)
        {
            var actortoUpdate = _actorRepository.GetById(id);
            if (actortoUpdate == null)
            {
                throw new KeyNotFoundException($"There is no Actor to Update with Id {id}");
            }
            ValidateRequest(request);
            var actor = new Models.DbModels.Actor
            {
                Id = id,
                Name = request.Name,
                Sex = request.Sex,
                Bio = request.Bio,
                DOB = request.DOB
            };
            _actorRepository.Update(id, actor);
        }
        public IEnumerable<int> CheckIdsExistInDatabase(List<string> ids)
        {
            return _actorRepository.CheckIdsExistInDatabase(ids);
        }
        public List<ActorResponse> GetByGivenIds(List<string> ids)
        {
            var actorlist = _actorRepository.GetByGivenIds(ids);
            if (actorlist == null)
            {
                return new List<ActorResponse>();
            }
            var actorResponseList = actorlist.Select(actor => new ActorResponse
            {
                Id = actor.Id,
                Name = actor.Name,
                Bio = actor.Bio,
                Sex = actor.Sex,
                DOB = actor.DOB
            }).ToList();
            return actorResponseList;
        }
        private void ValidateRequest(ActorRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ArgumentException("Name cannot be empty or null");
            }

            if (request.DOB > DateTime.Now)
            {
                throw new ArgumentException("DOB cannot be greater than current date");
            }
      
            if (string.IsNullOrWhiteSpace(request.Sex))
            {
                throw new ArgumentException("Sex cannot be empty or null");
            }

            if (string.IsNullOrWhiteSpace(request.Bio))
            {
                throw new ArgumentException("Bio cannot be empty or null");
            }
        }
    }
}

