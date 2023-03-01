using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using IMDB.Domain;
using IMDB.Repository;
using IMDB.Repository.Interfaces;
using IMDB.Services.Interfaces;

namespace IMDB.Services
{
    public class MovieServices : IMovieServices
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IActorServices _actorServices;
        private readonly IProducerRepository _producerServices;

        public MovieServices()
        {
            _movieRepository = new MovieRepository();
            _actorServices = new ActorServices();
            _producerServices = new ProducerRepository();
        }

        public List<Movie> GetAllMovies()
        {
            return _movieRepository.GetAll();
        }

        public void AddMovie(string name, string yearOfRelease, string plot, string actorIds, string producerId)
        {
            var movie = new Movie();
            var movieList = _movieRepository.GetAll();
            movie.Id = movieList.Any() ? movieList.Max(obj => obj.Id) + 1 : 1;       
            movie.Name = name;
            movie.YearOfRelease = int.Parse(yearOfRelease);
            movie.Plot = plot;
            string[] actorIDs = actorIds.Split(',');

            foreach (var index in actorIDs)
            {
                movie.Actors.Add(_actorServices.GetActorById(int.Parse(index)));
            }

            movie.Producer = _producerServices.GetProducerById(int.Parse(producerId));
            _movieRepository.AddMovie(movie);
        }

        public void DeleteMovieById(int id)
        {
            if (_movieRepository.GetAll().Count < id) throw new Exception();
            _movieRepository.DeleteMovie(id);
        }
    }
}
