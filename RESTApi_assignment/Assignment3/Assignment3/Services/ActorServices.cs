using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Assignment3.Models.DbModels;
using Assignment3.Models.Request;
using Assignment3.Models.Response;
using Assignment3.Repository;
using Assignment3.Repository.Interfaces;
using Assignment3.Services.Interfaces;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;
using System.Linq;
using System.Collections;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using Assignment3.CustomExceptions;
using Microsoft.Extensions.Logging;
using Assignment3.Enums;
using System.Reflection.PortableExecutable;
using SystemSqlException = System.Data.SqlClient.SqlException;

namespace Assignment3.Services
{
    public class ActorServices : IActorServices
    {
        private readonly ActorRepository _actorRepository;
        public ActorServices(ActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
        }
        public int Create(ActorRequest request)
        {
            ValidateRequest(request);
            var actor = new Actor 
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
                throw new KeyNotFoundException($"Actor with ID {id} not found");
            }
            try
            {
                _actorRepository.Delete(id);
            }
            catch (SystemSqlException ex)
            {
                if (ex.Number == (int)SqlErrorNumber.ForeignKeyViolation)
                {
                    throw new CustomExceptions.SqlException("Cannot delete this Actor record because it is referenced by another table.");
                }
                else
                {
                    throw;
                }
            }
        }

        public List<ActorRespone> GetAll()
        {
            var actorlist = _actorRepository.GetAll();
            if (actorlist == null)
            {
               return new List<ActorRespone>();
            }   
            var actorResponseList = actorlist.Select(actor => new ActorRespone
            {
                Id = actor.Id,
                Name = actor.Name,
                Bio = actor.Bio,
                Sex = actor.Sex,
                DOB = actor.DOB
            }).ToList();
            return actorResponseList;
        }

        public ActorRespone GetById(int id)
        {
            var actor = _actorRepository.GetById(id);
            if (actor == null)
            {
                return null;
            }
            var actorResponse = new ActorRespone
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
            ValidateRequest(request);
            var actor = new Actor
            {
                Id = id,
                Name = request.Name,
                Sex = request.Sex,
                Bio = request.Bio,
                DOB = request.DOB
            };
            _actorRepository.Update(id, actor);
        }
        public IEnumerable<int> CheckIdsExistInDatabase(string ids)
        {
            return _actorRepository.CheckIdsExistInDatabase(ids);
        }
        public List<ActorRespone> GetByGivenIds(string[] ids)
        {
            var actorlist = _actorRepository.GetByGivenIds(ids);
            if (actorlist == null)
            {
                return new List<ActorRespone>();
            }
            var actorResponseList = actorlist.Select(actor => new ActorRespone
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
                throw new ArgumentException("DOB cannot be greater than current date.");
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

