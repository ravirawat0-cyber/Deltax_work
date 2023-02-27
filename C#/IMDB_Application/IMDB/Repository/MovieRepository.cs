using IMDB.Domain;
using IMDB.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly List<Movie> _movieList;    
        public MovieRepository()
        {
            _movieList = new List<Movie>();
        }

        public List<Movie> GetAllMovies()
        {
            return _movieList;
        }

        public void AddMovie(Movie movie)
        {
            _movieList.Add(movie);
        }

        public void DeleteMovie(int id)
        {
            _movieList.RemoveAll(m => m.Id == id);
            //adjusting the id 
            for (int i = 0; i < _movieList.Count; i++)
            {
                _movieList[i].Id = i + 1;
            }
        }
    }
}
