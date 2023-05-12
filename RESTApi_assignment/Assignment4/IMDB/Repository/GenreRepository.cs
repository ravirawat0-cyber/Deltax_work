using IMDB.Models.DbModels;
using IMDB.Repository.Interfaces;
using Dapper;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IMDB.Repository
{
    public class GenreRepository: BaseRepository<Genre>, IGenreRepository
    {
        private readonly DbContext _dbContext;

        public GenreRepository(DbContext context)
            : base(context)
        {
            _dbContext = context;
        }
        public int Create(Genre entity)
        {
            var query = @"INSERT INTO Genres (Name)
                          VALUES (@Name)
                          SELECT SCOPE_IDENTITY()";
            var value = new { Name = entity.Name };
            var id = Create(query, value);
            return id;
        }

        public void Delete(int id)
        {
            var query = @"DELETE
                          FROM Genres
                          WHERE Id = @id";
            var value = new { Id = id };
            Delete(query, value);
        }

        public IEnumerable<Genre> GetAll()
        {
            var query = @"SELECT *
                          FROM Genres (NOLOCK)";
            return GetAll(query);
        }

        public Genre GetById(int id)
        {
            var query = @"SELECT *
                          FROM Genres (NOLOCK)
                          WHERE Id = @id ";
            var value = new { Id = id };
            return GetById(query, value);
        }

        public void Update(int id, Genre entity)
        {
            var query = @"UPDATE Genres
                          SET Name = @name
                          WHERE ID = @id";
            var values = new {Id =  id, Name = entity.Name };
            Update(query, values);  
        }
        public IEnumerable<int> CheckIdsExistInDatabase(List<string> ids)
        {
            string query = @"Select Id
                            From Genres (NOLOCK)
                            WHERE Id IN @GenreIds";
            var value = new { GenreIds = ids };
            return CheckIdsExistInDatabase(query, value, ids);
        }

        public IEnumerable<Genre> GetByGivenIds(List<string> ids)
        {
            var query = @"Select *
                            From Genres (NOLOCK)
                            WHERE Id IN @GenreIds";
            var value = new { GenreIds = ids };
            return GetByGivenIds(query, value); 
        }
    }
}
