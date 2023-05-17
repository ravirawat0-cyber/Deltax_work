using E_commerce.Models;
using E_commerce.Repository.Interfaces;
using E_commerce.Services.Interfaces;
using E_commerce.SqlHelper;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Collections.Generic;

namespace E_commerce.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IProducetRepository _producetRepository;
        private readonly IDataHelper _dataHelper;

        public ProductServices(IProducetRepository producetRepository, IDataHelper dataHelper)
        {
            _producetRepository = producetRepository;
            _dataHelper = dataHelper;
        }
        public IEnumerable<ProductResponse> GetAll()
        {
            var producerList = _producetRepository.GetAll();
            List<ProductResponse> productResponseList = new List<ProductResponse>();
            foreach(var producet in producerList)
            {
                var productResponse = new ProductResponse
                {
                    Id = producet.Id,
                    Name = producet.Name,
                    Description = producet.Description,
                    ImageUrl = producet.ImageUrl,
                    Price = producet.Price,
                    Category = _dataHelper.GetCategoryFromGivenId(producet.CategoryID)
                };
                 productResponseList.Add(productResponse);
            }
            return productResponseList;
        }
    }
}
