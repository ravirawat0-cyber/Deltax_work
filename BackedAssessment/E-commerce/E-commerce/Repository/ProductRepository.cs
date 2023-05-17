using Dapper;
using E_commerce.Models;
using E_commerce.Repository.Interfaces;
using System.Collections.Generic;

namespace E_commerce.Repository
{
    public class ProductRepository : IProducetRepository
    {
        private readonly DbContext _dbContext;

        public ProductRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Products> GetAll()
        {
            var query = @"SELECT *
                        FROM Products (NOLOCK)";
            using var connection = _dbContext.CreateConnection();
            return connection.Query<Products>(query);
        }

        public IEnumerable<int> GetOrderId(int id)
        {
            var query = @"SELECT ID
                        FROM Orders (NOLOCK)
                        Where CustomerID = @Id";
            var value = new { Id = id };
            using var connection = _dbContext.CreateConnection();
            return connection.Query<int>(query, value);
        }

        public IEnumerable<int> GetProductId(List<int> orderId)
        {
            var query = @"SELECT ProductID
                        FROM Order_Items (NOLOCK)
                        Where OrderID IN @OrderId";
            var value = new { OrderId = orderId };
            using var connection = _dbContext.CreateConnection();
            return connection.Query<int>(query, value);
        }

        public IEnumerable<int> FilterProductIdBasedOnCategory(List<int> productId)
        {
            var query = @"SELECT DISTINCT p.Id
                            FROM Products p (NOLOCK)
                            WHERE p.CategoryID IN (
                                SELECT CategoryID
                                FROM Products (NOLOCK)
                                WHERE Id IN @ProductId
                                GROUP BY CategoryID
                                HAVING COUNT(DISTINCT Id) > 1)";

            var value = new { ProductId = productId };
            using var connection = _dbContext.CreateConnection();
            return connection.Query<int>(query, value);
        }
        public IEnumerable<int> GetAllProductIdsFromOrderItemsTable()
        {
            var query = @"SELECT ProductID
                        FROM Order_Items (NOLOCK)";
            using var connection = _dbContext.CreateConnection();
            return connection.Query<int>(query);
        }

        public IEnumerable<Products> GetAllProductByIDs(List<int> productIds)
        {
            var query = @"SELECT *
                        FROM Products (NOLOCK)
                        Where Id IN @Ids";
            var value = new {Ids = productIds};
            using var connection = _dbContext.CreateConnection();
            return connection.Query<Products>(query, value);
        }
    }
}
