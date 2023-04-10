using AutoMapper;
using RESTApi_assignment2.Models.DbModels;
using RESTApi_assignment2.Models.Request;
using RESTApi_assignment2.Models.Response;
using RESTApi_assignment2.Repository;
using RESTApi_assignment2.Repository.Interfaces;
using RESTApi_assignment2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http.Headers;
using System.Numerics;

namespace RESTApi_assignment2.Services
{
    public class ProducerServices : IProducerServices
    {
        private readonly IProducerRepository _producerRepository;
        private readonly IMapper _mapper;

        public ProducerServices(IProducerRepository producerRepository, IMapper mapper)
        {
            _producerRepository = producerRepository;
            _mapper = mapper;
        }

        public int Create(ProducerRequest request)
        {
            ValidateReqeust(request);
            var producer = _mapper.Map<Producer>(request);
            producer.Id = _producerRepository.GetAll().Count + 1;
            _producerRepository.Create(producer);
            return producer.Id;
        }

        public void Delete(int id)
        {
            if (_producerRepository.GetById(id) == null)
            {
                throw new ArgumentException($"producer with ID {id} not found");
            }
            _producerRepository.Delete(id);
        }

        public List<ProducerRespone> GetAll()
        {
            var produerlist = _producerRepository.GetAll();
            if (produerlist == null)
            {
                return new List<ProducerRespone>();
            }
            return _mapper.Map<List<ProducerRespone>>(produerlist);
        }

        public ProducerRespone GetById(int id)
        {
            var producer = _producerRepository.GetById(id);
            if (producer == null)
            {
                return null;
            }
            return _mapper.Map<ProducerRespone>(producer);
        }

        public int Update(int id, ProducerRequest request)
        {
            var producer = _producerRepository.GetById(id);
            if (producer == null)
            {
                throw new ArgumentException($"Producer with ID {id} not found");
            }
            ValidateReqeust(request);
            producer.Name = request.Name;
            producer.Gender = request.Gender;
            producer.Bio = request.Bio;
            producer.DOB = DateTime.Parse(request.DOB);

            _producerRepository.Update(producer);
            return producer.Id;
        }
        private void ValidateReqeust(ProducerRequest request)
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
