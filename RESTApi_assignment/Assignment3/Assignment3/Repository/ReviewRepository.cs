using Assignment3.Models.DbModels;
using Assignment3.Repository.Interfaces;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Windows.Markup;

namespace Assignment3.Repository
{
    public class ReviewRepository : BaseRepository<Review>
    {
        private readonly DbContext _context;
        public ReviewRepository(DbContext context)
            : base(context)
        {
            _context = context;
        }

        public int Create(Review entity)
        {
            var query = @"INSERT INTO Reviews (
	                        Message
	                        ,MovieId
	                        )
                        VALUES (
	                        @message
	                        ,@MovieId
	                        )
                       SELECT SCOPE_IDENTITY()";
            var values = new { Message = entity.Message, MovieId = entity.MovieId };
            var id  = Create(query, values);
            return id;
        }
        public void Delete(int movieId, int id)
        {
            var query = @"DELETE
                          FROM Reviews
                          WHERE Id = @id
	                          AND MovieId = @movieId";
            var value = new { Id = id , MovieId = movieId};
            Delete(query, value);
        }
        public IEnumerable<Review> GetAll(int movieId)
        {
            var query = @"SELECT *
                         FROM Reviews (NOLOCK)
                         WHERE MovieId = @movieId";
            var value = new { MovieId = movieId };
            using var connection = _context.CreateConnection();
            var reviewList = connection.Query<Review>(query, value);
            return reviewList;
        }

        public Review GetById(int movieId, int id)
        {
            var query = @"SELECT *
                          FROM Reviews (NOLOCK)
                          WHERE Id = @id
                                AND MovieId = @movieId";
            var value = new { Id = id , MovieId = movieId };
            return GetById(query, value);
        }

        public void Update(Review entity)
        {
            var query = @"UPDATE Reviews
                          SET Message = @message
                          WHERE Id = @id
                                AND MovieId = @movieId";
            var values = new
            {
                Id = entity.Id,
                Message = entity.Message,
                MovieId = entity.MovieId,
            };
            Update(query, values);
        }
    }
}
