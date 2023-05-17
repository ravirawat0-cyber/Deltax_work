using E_commerce.Models;
using E_commerce.Repository.Interfaces;
using E_commerce.Services.Interfaces;
using E_commerce.SqlHelper;
using Microsoft.Extensions.Configuration.UserSecrets;
using System;
using System.Collections.Generic;
using System.Linq;

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
            var productList = _producetRepository.GetAll();
            return CreateProductResponseList(productList);

        }

        private List<ProductResponse> CreateProductResponseList(IEnumerable<Products> productList)
        {
            List<ProductResponse> productResponseList = new List<ProductResponse>();
            foreach (var product in productList)
            {
                var productResponse = new ProductResponse
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    ImageUrl = product.ImageUrl,
                    Price = product.Price,
                    Category = _dataHelper.GetCategoryFromGivenId(product.CategoryID)
                };
                productResponseList.Add(productResponse);
            }
            return productResponseList;
        }


        public IEnumerable<ProductResponse> GetTopRecommendedProducts(int customerId)
        {
            List<int> perticularUserOrderId = _producetRepository.GetOrderId(customerId).ToList();
            List<int> perticularUserProductIds = _producetRepository.GetProductId(perticularUserOrderId).ToList();
            List<int> allUsersProductIds = _producetRepository.GetAllProductIdsFromOrderItemsTable().ToList();

            List<int> frequentlyPurchasedProducts = GetFrequentlyPurchasedProducts(perticularUserProductIds);
            List<int> popularProducts = GetPopularProducts();
            List<int> popularSimilarProducts = GetPopularSimilarProducts(allUsersProductIds);
            List<int> similarProducts = GetSimilarProducts(perticularUserProductIds);


            List<int> commonElements = frequentlyPurchasedProducts.Intersect(popularProducts)
                                                                  .Intersect(similarProducts)
                                                                  .Intersect(popularSimilarProducts)
                                                                  .ToList();
            var producerList = _producetRepository.GetAllProductByIDs(commonElements);
            return CreateProductResponseList(producerList);


        }

        private List<int> GetPopularSimilarProducts(List<int> allUsersProductIds)
        {
            List<int> FilterProductIdsBasedOnCategory = _producetRepository.FilterProductIdBasedOnCategory(allUsersProductIds).ToList();
            return FilterProductIdsBasedOnCategory;
        }

        private List<int> GetPopularProducts()
        {
            List<int> productIds = _producetRepository.GetAllProductIdsFromOrderItemsTable().ToList();
            var topProducts = productIds
                                 .GroupBy(productId => productId)
                                 .OrderByDescending(group => group.Count())
                                 .Select(group => group.Key)
                                 .Take(10)
                                 .ToList();
            return topProducts;
        }

        private List<int> GetFrequentlyPurchasedProducts(List<int> ProductIds)
        { 
            List<int> frequentlyPurchasedProductIds = ProductIds
                                                        .GroupBy(productId => productId)
                                                        .Where(group => group.Count() > 1)
                                                        .Select(group => group.Key)
                                                        .ToList();
            return frequentlyPurchasedProductIds;
        }
        private List<int> GetSimilarProducts(List<int> ProductIds)
        {
           List<int> FilterProductIdsBasedOnCategory = _producetRepository.FilterProductIdBasedOnCategory(ProductIds).ToList();
           return FilterProductIdsBasedOnCategory;
        }

    }
}
