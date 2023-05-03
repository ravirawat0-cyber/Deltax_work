using IMDB.Models.DbModels;
using Dapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace IMDB.Helper
{
    public class DataHelper:IDataHelper
    {

        private readonly DbContext _context;

        public DataHelper(DbContext context)
        {
            _context = context;
        }

        public IEnumerable<string> GetActorIdsFromActorMovieTable(int movieId)
        {
            var query = @"SELECT ActorId
                          FROM Actors_Movies
                          WHERE MovieId = @movieId";
            var values = new { MovieId = movieId };
            using var connection = _context.CreateConnection();
            var actorIds = connection.Query<string>(query, values).ToList();
            return actorIds;
        }

        public IEnumerable<string> GetGenreIdsFromGenreMovieTable(int movieId)
        {
            var query = @"SELECT GenreId
                          FROM Genres_Movies
                          WHERE MovieId = @movieId";
            var values = new { MovieId = movieId };
            using var connection = _context.CreateConnection();
            var genreIds = connection.Query<string>(query, values).ToList();
            return genreIds;
        }

        public void DeleteFromActorMovieTable(int movieId)
        {
            var query = @"DELETE FROM Actors_Movies
                          WHERE MovieId = @movieId";
            var value = new { MovieId = movieId };
            using var connection = _context.CreateConnection();
            connection.Execute(query, value);
        }

        public void DeleteFromGenreMovieTable(int movieId)
        {
            var query = @"DELETE FROM Genres_Movies
                          WHERE MovieId = @movieId";
            var value = new { MovieId = movieId };
            using var connection = _context.CreateConnection();
            connection.Execute(query, value);
        }

        public void DeleteFromReviewsTable(int movieId)
        {
            var query = @"Delete From Reviews
                          WHERE MovieId = @movieId";
            var value = new { MovieId = movieId };
            using var connection = _context.CreateConnection();
            connection.Execute(query, value);
        }
    }
}
