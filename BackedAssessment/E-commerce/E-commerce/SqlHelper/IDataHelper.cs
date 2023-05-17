using E_commerce.Models;

namespace E_commerce.SqlHelper
{
    public interface IDataHelper
    {
        Category GetCategoryFromGivenId(int CategoryID);
        void PopulateOrderItemsTable(OrderItems orderItems);

        int CheckIfCustomerExist(int id);
    }
}
