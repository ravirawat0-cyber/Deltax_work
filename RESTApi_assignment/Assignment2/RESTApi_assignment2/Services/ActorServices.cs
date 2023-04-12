using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using RESTApi_assignment2.Models.DbModels;
using RESTApi_assignment2.Models.Request;
using RESTApi_assignment2.Models.Response;
using RESTApi_assignment2.Repository;
using RESTApi_assignment2.Repository.Interfaces;
using RESTApi_assignment2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;

namespace RESTApi_assignment2.Services
{
    public class ActorServices : IActorServices
    {
        private readonly IActorRepository _actorRepository;
        private readonly IMapper _mapper;

        public ActorServices(IActorRepository actorRepository, IMapper mapper)
        {
            _actorRepository = actorRepository;
            _mapper = mapper;
        }
        public int Create(ActorRequest request)
        {
            ValidateRequest(request);
            var actor = _mapper.Map<Actor>(request);
            actor.Id = _actorRepository.GetAll().Count + 1;
            _actorRepository.Create(actor);
            return actor.Id;
        }

        public void Delete(int id)
        {
            if (_actorRepository.GetById(id) == null)
            {
                throw new KeyNotFoundException($"Actor with ID {id} not found");
            }
            _actorRepository.Delete(id);
        }

        public List<ActorRespone> GetAll()
        {
            var actorlist = _actorRepository.GetAll();
            if (actorlist == null)
            {
               return new List<ActorRespone>();
            }
            return _mapper.Map<List<ActorRespone>>(actorlist);
        }

        public ActorRespone GetById(int id)
        {
            var actor = _actorRepository.GetById(id);
            if (actor == null)
            {
                throw new KeyNotFoundException($"Actor with ID {id} not found");
            }
            var mapper =  _mapper.Map<ActorRespone>(actor);  
            return mapper;
        }

        public int Update(int id, ActorRequest request)
        {      
            var actor = _actorRepository.GetById(id);

            if (actor == null)
            {
                throw new KeyNotFoundException($"Actor with {id} not found");
            }
            ValidateRequest(request);

            actor.Name = request.Name;
            actor.Gender = request.Gender;
            actor.Bio = request.Bio;
            actor.DOB = DateTime.Parse(request.DOB);
            _actorRepository.Update(actor);
            return actor.Id;
        }

        private void ValidateRequest(ActorRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ArgumentException("Name cannot be empty or null");
            }
            var parameterDate = DateTime.ParseExact(request.DOB, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (request.DOB == null || parameterDate > DateTime.Today)
            {
                throw new ArgumentException("DOB cannot be empty or greater than current year");
            }

            if (string.IsNullOrWhiteSpace(request.Gender))
            {
                throw new ArgumentException("Gender cannot be empty or null");
            }

            if (string.IsNullOrWhiteSpace(request.Bio))
            {
                throw new ArgumentException("Bio cannot be empty or null");
            }
        }
    }
}

