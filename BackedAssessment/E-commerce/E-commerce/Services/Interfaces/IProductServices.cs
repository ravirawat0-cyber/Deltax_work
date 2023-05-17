using E_commerce.Models;
using System.Collections;
using System.Collections.Generic;

namespace E_commerce.Services.Interfaces
{
    public interface IProductServices
    {
        IEnumerable<ProductResponse> GetAll();
        IEnumerable<ProductResponse> GetTopRecommendedProducts(int customerId);


    }
}
