using RESTApi_assignment2.Models.DbModels;
using RESTApi_assignment2.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace RESTApi_assignment2.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly List<Movie> _moviesList = new List<Movie>();
        public void Create(Movie entity)
        {
            
            _moviesList.Add(entity);
        }

        public void Delete(int id)
        {
            var movie = GetById(id);
           _moviesList.Remove(movie);
            
        }

        public List<Movie> GetAll()
        {
            return _moviesList;
        }

        public Movie GetById(int id)
        {
            return _moviesList.FirstOrDefault(m => m.Id == id);
        }

        public void Update(Movie entity)
        {
            var existingMovie = GetById(entity.Id);
            _moviesList.Remove(existingMovie);
            _moviesList.Add(entity);       
        }
    }
}
