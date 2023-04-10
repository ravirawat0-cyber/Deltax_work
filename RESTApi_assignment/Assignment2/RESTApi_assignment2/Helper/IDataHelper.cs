using System.Collections.Generic;

namespace RESTApi_assignment2.Helper
{
    public interface IDataHelper
    {
        void AddToActorsMoviesDict(int key, List<int> value);
        List<int> GetValueFromActorsMoviesDict(int key);
        void AddToGenresMoviesDict(int key, List<int> value);
        List<int> GetValueFromGenresMoviesDict(int key);
        void AddToMovieIdList(int id);
        bool IsMoviePresent(int id);
        void DeleteMovieRelaton(int key);
    }
}
