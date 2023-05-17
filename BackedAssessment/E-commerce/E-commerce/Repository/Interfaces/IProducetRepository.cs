using E_commerce.Models;
using System.Collections;
using System.Collections.Generic;

namespace E_commerce.Repository.Interfaces
{
    public interface IProducetRepository
    {
        IEnumerable<Products> GetAll();
        IEnumerable<int> GetOrderId(int id);

        IEnumerable<int> GetProductId(List<int> orderId);
        IEnumerable<int> FilterProductIdBasedOnCategory(List<int> productId);
        IEnumerable<int> GetAllProductIdsFromOrderItemsTable();

        IEnumerable<Products> GetAllProductByIDs(List<int> productIds);
    }

   
}
