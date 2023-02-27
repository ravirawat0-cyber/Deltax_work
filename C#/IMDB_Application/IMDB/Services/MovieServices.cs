using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            return _movieRepository.GetAllMovies();
        }

        public void AddMovie(string name, int yearOfRelease, string plot, string actorIds, string producerId)
        {
            var movie = new Movie();
            if (string.IsNullOrEmpty(name)) throw new Exception("Movie name cannot be Empty");
            if (yearOfRelease == 0) throw new Exception("Please enter year of release");
            if (string.IsNullOrEmpty(plot)) throw new Exception("Please enter the plot of the movie");
            if (string.IsNullOrEmpty(actorIds)) throw new Exception("Please select actor id from the list");
            if (string.IsNullOrEmpty(producerId)) throw new Exception("Please select producer id from the list");

            // Impletment auto assign id 
            var movieList = _movieRepository.GetAllMovies();
            movie.Id = movieList.Any() ? movieList.Max(obj => obj.Id) + 1 : 1;       
            movie.Name = name;
            movie.YearOfRelease = yearOfRelease;
            movie.Plot = plot;
            string[] actorId = actorIds.Split(',');

            foreach (var index in actorId)
            {
                movie.Actors.Add(_actorServices.GetActorById(int.Parse(index)));
            }

            movie.Producer = _producerServices.GetProducerById(int.Parse(producerId));
            _movieRepository.AddMovie(movie);
        }

        public void DeleteMovieById(int id)
        {
            if (id == 0) throw new Exception("Enter valid id");
            _movieRepository.DeleteMovie(id);
        }
    }
}
