﻿using AutoMapper;
using IMDB.Models.DbModels;
using IMDB.Models.Request;
using IMDB.Models.Response;
using IMDB.Repository;
using IMDB.Repository.Interfaces;
using IMDB.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http.Headers;
using System.Numerics;
using System.Linq;
using SystemSqlException = System.Data.SqlClient.SqlException;
using IMDB.Enums;

namespace IMDB.Services
{
    public class ProducerServices : IProducerServices
    {
        private readonly IProducerRepository _producerRepository;

        public ProducerServices(IProducerRepository producerRepository)
        {
            _producerRepository = producerRepository;
        }

        public int Create(ProducerRequest request)
        {
            ValidateReqeust(request);
            var producer = new Producer
            {
                Name = request.Name,
                Bio = request.Bio,
                Sex = request.Sex,
                DOB = request.DOB
            };
            var id = _producerRepository.Create(producer);
            return id;
        }

        public void Delete(int id)
        {
            if(_producerRepository.GetById(id) == null) 
            {
                throw new KeyNotFoundException($"Producer with ID {id} not found");
            }
            try
            {
                _producerRepository.Delete(id);
            }
            catch (SystemSqlException ex)
            {
                if (ex.Number == (int)SqlErrorNumber.ForeignKeyViolation)
                {
                    throw new CustomExceptions.SqlException("Cannot delete this Producer record because it is referenced by another table");
                }
                else
                {
                    throw;
                }
            }
        }
        public List<ProducerRespone> GetAll()
        {
            var produerlist = _producerRepository.GetAll();
            if (produerlist == null)
            {
                return new List<ProducerRespone>();
            }
            var producerResponseList = produerlist.Select(producer => new ProducerRespone
            {
                Id = producer.Id,
                Name = producer.Name,
                Bio = producer.Bio,
                Sex = producer.Sex,
                DOB = producer.DOB
            }).ToList();
            return producerResponseList;
        }

        public ProducerRespone GetById(int id)
        {
            var producer = _producerRepository.GetById(id);
            if (producer == null)
            {
                throw new KeyNotFoundException($"Producer with ID {id} not found"); ;
            }
            var producerRespone = new ProducerRespone
            {
                Id = producer.Id,
                Name = producer.Name,
                Bio = producer.Bio,
                Sex = producer.Sex,
                DOB = producer.DOB
            };
            return producerRespone;
        }

        public void Update(int id, ProducerRequest request)
        {
            var producertoUpdate = _producerRepository.GetById(id);
            if (producertoUpdate == null)
            {
                throw new KeyNotFoundException($"There is no Producer to Update with Id {id}");
            }
            ValidateReqeust(request);
            var producer = new Producer 
            { 
                Name = request.Name,
                Sex = request.Sex,
                Bio = request.Bio,
                DOB = request.DOB
            };
            _producerRepository.Update(id , producer);
            
        }
        private void ValidateReqeust(ProducerRequest request)
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
