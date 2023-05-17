using Dapper;
using E_commerce.Models;
using Newtonsoft.Json.Linq;

namespace E_commerce.SqlHelper
{
    public class DataHelper:IDataHelper
    {
        private readonly DbContext _dbContext;

        public DataHelper(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int CheckIfCustomerExist(int id)
        {
            var query = @"SELECT ID
                        FROM Customers (NOLOCK)
                        WHERE Id = @id";
            var value = new { Id = id };
            using var connection = _dbContext.CreateConnection();
            return connection.ExecuteScalar<int>(query, value);
        }

        public Category GetCategoryFromGivenId(int CategoryID)
        {
            var query = @"SELECT *
                        FROM Categories (NOLOCK)
                        WHERE Id = @id";
            var value = new { Id = CategoryID };
            using var connection = _dbContext.CreateConnection();
            return connection.QueryFirstOrDefault<Category>(query, value);
        }

        public void PopulateOrderItemsTable(OrderItems orderItems)
        {
            var query = @"INSERT INTO Order_Items (
	                                OrderID
	                                ,ProductID
	                                ,Quantity
                                    ,ProductPrice        
	                                )
                                VALUES (
	                                @OrderID
	                                ,@ProductID
	                                ,@Quantity
                                    ,@ProductPrice 
	                                );
                                SELECT SCOPE_IDENTITY()";

            var values = new { ProductID = orderItems.ProductID,
                                OrderID = orderItems.OrderID,
                                Quantity = orderItems.Quantity,
                                ProductPrice = orderItems.ProductPrice  };
            using var connection = _dbContext.CreateConnection();
            connection.Execute(query, values);
        }
    }
}
