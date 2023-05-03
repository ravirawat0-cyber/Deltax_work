using IMDB.Helper;
using IMDB.Models.DbModels;
using IMDB.Models.Request;
using IMDB.Models.Response;
using IMDB.Repository.Interfaces;
using IMDB.Services;
using IMDB.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace IMDB.test.MockResources
{
    public class MovieMock
    {
        public static readonly Mock<IMovieRepository> MovieRepoMock = new Mock<IMovieRepository>();
        public static readonly Mock<IDataHelper> DataHelperMock = new Mock<IDataHelper>();
        public static readonly Mock<IActorRepository> ActorRepoMock = new Mock<IActorRepository>();
        public static readonly Mock<IProducerRepository> ProducerRepoMock = new Mock<IProducerRepository>();
        public static readonly Mock<IGenreRepository> GenreRepoMock = new Mock<IGenreRepository>();

        public static Dictionary<int, List<string>> ActorsMovies = new Dictionary<int, List<string>>();
        public static Dictionary<int, List<string>> GenresMovies = new Dictionary<int, List<string>>();
        public static List<Movie>  ListOfMovies = new List<Movie>();
        public static List<Actor> ListOfActors = new List<Actor>();
        public static List<Genre> ListOfGenres = new List<Genre>();
        public static List<Producer> ListOfProducers = new List<Producer>();

        public static void MockActorRepo()
        {
            ActorRepoMock.Setup(x => x.GetByGivenIds(It.IsAny<List<string>>())).Returns((List<string> ids) =>
            {
                return ListOfActors.Where(x => ids.Contains(x.Id.ToString()));

            });

            ActorRepoMock.Setup(x => x.CheckIdsExistInDatabase(It.IsAny<List<string>>())).Returns((List<string> ids) =>
            {
                var missingIds = ids.Where(id => !ListOfActors.Any(actor => actor.Id.ToString() == id))
                                 .Select(id => int.Parse(id));
                return missingIds;
            });
        }
        public static void MockGenreRepo()
        {
            GenreRepoMock.Setup(x => x.GetByGivenIds(It.IsAny<List<string>>())).Returns((List<string> ids) =>
            {
                return ListOfGenres.Where(x => ids.Contains(x.Id.ToString()));
            });

            GenreRepoMock.Setup(x => x.CheckIdsExistInDatabase(It.IsAny<List<string>>())).Returns((List<string> ids) =>
            {
                var missingIds = ids.Where(id => !ListOfGenres.Any(genre => genre.Id.ToString() == id))
                                 .Select(id => int.Parse(id));
                return missingIds;
            });
        }
      
        public static void MockProducerRepo()
        {
            ProducerRepoMock.Setup(x => x.GetById(It.IsAny<int>())).Returns((int id) =>
            {
                return ListOfProducers.FirstOrDefault(x => x.Id == id);
            });
        }

        public static void MockDataHelper()
        {
            DataHelperMock.Setup(x => x.GetActorIdsFromActorMovieTable(It.IsAny<int>())).Returns((int movieId) =>
            {
                return ActorsMovies[movieId];
            });

            DataHelperMock.Setup(x => x.GetGenreIdsFromGenreMovieTable(It.IsAny<int>())).Returns((int movieId) =>
            {
                return GenresMovies[movieId];
            });

            DataHelperMock.Setup(x => x.DeleteFromActorMovieTable(It.IsAny<int>())).Callback((int movieId) =>
            {
                if (ActorsMovies.ContainsKey(movieId))
                {
                    ActorsMovies.Remove(movieId);
                }
            });
            DataHelperMock.Setup(x => x.DeleteFromGenreMovieTable(It.IsAny<int>())).Callback((int movieId) =>
            {
                GenresMovies.Remove(movieId);   
            });
        }
        public static void MockMovieRepo()
        {
            MovieRepoMock.Setup(x => x.GetAll()).Returns(ListOfMovies);

            MovieRepoMock.Setup(x => x.GetById(It.IsAny<int>())).Returns((int id) =>
            {
                return ListOfMovies.FirstOrDefault(m => m.Id == id);
            });

            MovieRepoMock.Setup(x => x.Create(It.IsAny<MovieRequest>())).Returns((MovieRequest movieRequest) =>
            {
                var movieId = ListOfMovies.Count() + 1;
                var movie = new Movie
                {
                    Id = movieId,
                    Name = movieRequest.Name,
                    YearOfRelease = movieRequest.YearOfRelease,
                    Plot = movieRequest.Plot,
                    ProducerId = movieRequest.ProducerId,
                    CoverImageUrl = movieRequest.CoverImageUrl,
                };
                ListOfMovies.Add(movie);
                ActorsMovies.Add(movieId, movieRequest.ActorIds.Split(',').ToList());
                GenresMovies.Add(movieId, movieRequest.GenreIds.Split(',').ToList());
                return movieId;
            });

            MovieRepoMock.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<MovieRequest>())).Callback((int movieId, MovieRequest movieRequest) =>
            {
                var currentMovie = ListOfMovies.FirstOrDefault(m => m.Id == movieId);
                var movie = new Movie
                {
                    Id = movieId,
                    Name = movieRequest.Name,
                    YearOfRelease = movieRequest.YearOfRelease,
                    Plot = movieRequest.Plot,
                    ProducerId = movieRequest.ProducerId,
                    CoverImageUrl = movieRequest.CoverImageUrl
                };

                ListOfMovies.Remove(currentMovie);
                ListOfMovies.Add(movie);
                var actorIds = movieRequest.ActorIds.Split(',').ToList();
                ActorsMovies[movieId] = actorIds;

                var genreIds = movieRequest.GenreIds.Split(',').ToList();
                GenresMovies[movieId] = genreIds;
            });

            MovieRepoMock.Setup(x => x.Delete(It.IsAny<int>())).Callback((int id) =>
            {
                var movieToRemove = ListOfMovies.FirstOrDefault(x => x.Id == id);
                ListOfMovies.Remove(movieToRemove);
            });
        }
    }
}
