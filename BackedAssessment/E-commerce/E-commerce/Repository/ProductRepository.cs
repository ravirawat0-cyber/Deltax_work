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
    }
}
