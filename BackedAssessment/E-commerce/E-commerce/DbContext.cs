using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace E_commerce
{
    public class DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("Ecommerce");
        }
        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
