using static Dapper.SqlMapper;
using System.Collections.Generic;
using Assignment3.Models.DbModels;
using System.Collections;
using System.Xml.Schema;
using System.Linq;

namespace Assignment3.Repository.Interfaces
{
    public class BaseRepository<T> where T : class
    {
         private  readonly DbContext _context;

        public BaseRepository(DbContext dbContext)
        {
            _context = dbContext;
        }
        public int Create(string query,object values )
        {
            using var connection = _context.CreateConnection();
            return connection.ExecuteScalar<int>(query, values);
        }

        public void Delete(string query, object value)
        {
            using var connection = _context.CreateConnection();
            connection.Execute(query, value);
        }

        public IEnumerable<T> GetAll(string query)
        {
            using var connection = _context.CreateConnection();
            return connection.Query<T>(query);
        }

        public T GetById(string query , object value)
        {
            using var connection = _context.CreateConnection();
            return connection.QueryFirstOrDefault<T>(query, value);
        }

        public void Update(string query, object values)
        {
            using var connection = _context.CreateConnection();
            connection.Execute(query, values);
        }

        public IEnumerable<T> GetByGivenIds(string query, object value)
        {
            using var connection = _context.CreateConnection();
            return connection.Query<T>(query, value);
        }
        public IEnumerable<int> CheckIdsExistInDatabase(string query, object value, string[] ids)
        {
            using var connection = _context.CreateConnection();
            var foundActors = connection.Query<int>(query, value).ToList();
            var missingIds = ids.Select(int.Parse).Except(foundActors);
            return missingIds;
        }
    }

}
