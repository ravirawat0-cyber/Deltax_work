using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
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
            if (string.IsNullOrWhiteSpace(name)) throw new Exception("Name of movie cannot be empty.");
            if (!(Int32.TryParse(yearOfRelease, out int yor))) throw new Exception("Year of release should be a positive integer.");
            if (string.IsNullOrWhiteSpace(plot)) throw new Exception("Plot of the movie cannot be empty.");
            if (actorIds.Length == 0 ) throw new Exception("Actor IDs cannot be empty.");
            if (actorIds.Length == 0) throw new Exception("Producer ID cannot be empty.");

            var movie = new Movie();
            var movieList = _movieRepository.GetAll();
            movie.Id = movieList.Any() ? movieList.Max(obj => obj.Id) + 1 : 1;       
            movie.Name = name;
            movie.YearOfRelease = int.Parse(yearOfRelease);
            movie.Plot = plot;
            string[] actorIDs = actorIds.Split(',');

            foreach (var index in actorIDs)
            {
                if (int.Parse(index) > _actorServices.GetAllActors().Count || !(int.TryParse(index, out int valid))) throw new Exception("Enter valid actors Id");
                movie.Actors.Add(_actorServices.GetActorById(int.Parse(index)));
            }

            if (int.Parse(producerId) > _actorServices.GetAllActors().Count || !(int.TryParse(producerId, out int vali))) throw new Exception("Enter valid producer Id");
            movie.Producer = _producerServices.GetProducerById(int.Parse(producerId));
            _movieRepository.AddMovie(movie);
        }

        public void DeleteMovieById(int id)
        {
            if (_movieRepository.GetAll().Count < id || id <= 0) throw new Exception("Invalid Id");
            _movieRepository.DeleteMovie(id);
        }
    }
}
