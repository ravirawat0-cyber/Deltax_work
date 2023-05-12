using Assignment3.Models.DbModels;
using Assignment3.Models.Request;
using Assignment3.Repository.Interfaces;
using Dapper;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace Assignment3.Repository
{
    public class MovieRepository: BaseRepository<Movie>
    {
        private readonly DbContext _context;
        public MovieRepository(DbContext context): base(context) 
        {
             _context = context;
        }
        public int Create(MovieRequest entity)
        {
            var values = new
            {
                Name = entity.Name,
                YearofRelease = entity.YearOfRelease,
                Plot = entity.Plot,
                CoverImageUrl = entity.CoverImageUrl,
                ProducerId = entity.ProducerId,
                ActorIds = entity.ActorIds,
                GenreIds = entity.GenreIds
            };

            using var connection = _context.CreateConnection();
            var id = connection.ExecuteScalar<int>("usp_AddMovie", 
                                         values, 
                                         commandType: CommandType.StoredProcedure);
            return id;
        }

        public void Delete(int id)
        {
            var query = @"DELETE
                          FROM Movies
                          WHERE Id = @id";
            var values = new { Id = id };
            Delete(query, values);
        }

        public IEnumerable<Movie> GetAll()
        {
            var query = @"SELECT *
                          FROM Movies (NOLOCK)";
            return GetAll(query);
        }
            
        public Movie GetById(int id)
        {
            var query = @"SELECT *
                          FROM Movies (NOLOCK)
                          WHERE Id = @id";
            var values = new { Id = id };
            return GetById(query, values);
        }

        public void Update(int id, MovieRequest entity)
        {
            var values = new
            {
                MovieId = id,
                Name = entity.Name,
                YearofRelease = entity.YearOfRelease,
                Plot = entity.Plot,
                CoverImageUrl = entity.CoverImageUrl,
                ProducerId = entity.ProducerId,
                ActorIds = entity.ActorIds,
                GenreIds = entity.GenreIds
            };
            using var connection = _context.CreateConnection();
            connection.Execute("usp_UpdateMovie", 
                                values,
                                commandType: CommandType.StoredProcedure);     
        }
    }
}
