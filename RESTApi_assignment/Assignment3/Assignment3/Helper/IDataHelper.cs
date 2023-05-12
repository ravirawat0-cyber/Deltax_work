using System.Collections.Generic;

namespace Assignment3.Helper
{
    public interface IDataHelper
    {
        void DeleteFromActorMovieTable(int movieId);
        void DeleteFromGenreMovieTable(int movieId);
        void DeleteFromReviewsTable(int id);
        IEnumerable<string> GetActorIdsFromActorMovieTable(int movieId);
        IEnumerable<string> GetGenreIdsFromGenreMovieTable(int movieId);
    }
}
