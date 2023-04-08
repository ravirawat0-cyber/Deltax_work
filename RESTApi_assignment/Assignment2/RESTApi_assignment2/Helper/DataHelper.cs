using RESTApi_assignment2.Models.DbModels;
using System.Collections.Generic;

namespace RESTApi_assignment2.Helper
{
    public class DataHelper:IDataHelper
    {

        private readonly Dictionary<int, List<int>> _actorsMovies = new Dictionary<int, List<int>>();
        private readonly Dictionary<int, List<int>> _genresMovies = new Dictionary<int, List<int>>();
        private readonly List<int> _movieIds = new List<int>();

        public void AddToActorsMoviesDict(int key, List<int> value)
        {
            if (_actorsMovies.ContainsKey(key))
            {
                _actorsMovies[key] = value;
            }
            else
            {
                _actorsMovies.Add(key, value);
            }
        }

        public void AddToGenresMoviesDict(int key, List<int> value)
        {
            if (_actorsMovies.ContainsKey(key))
            {
                _genresMovies[key] = value;
            }
            else
            {
                _genresMovies.Add(key, value);
            }
        }

        public void AddToMovieIdList(int id)
        {
            if (!(_movieIds.Contains(id)));
            {
                _movieIds.Add(id);
            }
        }
        public bool IsMoviePresent(int id)
        {
            if (_movieIds.Contains(id)) return true;
            else return false;
        }

        public List<int> GetValueFromActorsMoviesDict(int key)
        {
            if (!_genresMovies.TryGetValue(key, out List<int> value))
            {
                throw new KeyNotFoundException("key not found in ActorsMovies");
            }
            return value;
        }

        public List<int> GetValueFromGenresMoviesDict(int key)
        {
            if (!_genresMovies.TryGetValue(key, out List<int> value))
            {
                throw new KeyNotFoundException("key not found in GenresMovies");
            }
            return value;
        }

        public void DeleteRelatonHelpers(int key)
        {
            _actorsMovies.Remove(key);
            _genresMovies.Remove(key);
            _movieIds.Remove(key);
        }
    }
}
