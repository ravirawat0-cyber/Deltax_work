using E_commerce.Models;
using E_commerce.Repository.Interfaces;
using static Dapper.SqlMapper;

namespace E_commerce.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbContext _dbContext;

        public OrderRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int Create(Orders order)
        {
            var query = @"INSERT INTO Orders (
	                                CustomerID
	                                ,OrderDate
	                                ,TotalAmount
	                                )
                                VALUES (
	                                @customerID
	                                ,@orderDate
	                                ,@totalAmount
	                                );
                                SELECT SCOPE_IDENTITY()";
            var values = new
            {
                CustomerID = order.CustomerID,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
            };
            using var connection = _dbContext.CreateConnection();
            return connection.ExecuteScalar<int>(query, values);
        }
    }
}
