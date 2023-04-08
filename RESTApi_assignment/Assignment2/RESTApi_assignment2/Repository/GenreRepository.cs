using RESTApi_assignment2.Models.DbModels;
using RESTApi_assignment2.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace RESTApi_assignment2.Repository
{
    public class GenreRepository: IGenreRepository
    {
        private readonly List<Genre> _genreList = new List<Genre>();
        public void Create(Genre entity)
        {
            _genreList.Add(entity);
        }

        public void Delete(int id)
        {
            var genre = GetById(id);
            _genreList.Remove(genre);
        }

        public List<Genre> GetAll()
        {
            return _genreList;
        }

        public Genre GetById(int id)
        {
            return _genreList.FirstOrDefault(m => m.Id == id);
        }

        public void Update(Genre entity)
        {
            var existingGenre = GetById(entity.Id);
            _genreList.Remove(existingGenre);
            _genreList.Add(entity);
        }
    }
}
