using IMDB.Models.DbModels;
using IMDB.Models.Response;
using IMDB.test.MockResources;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow.EnvironmentAccess;
using ActorResponse = IMDB.Models.Response.ActorResponse;

namespace IMDB.test.StepDefinitions
{
    [Scope(Feature = "Movie Resource")]
    [Binding]
    public class MovieSteps:BaseSteps
    {
        public MovieSteps(CustomWebApplicationFactory factory)
            :base(factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddScoped(_ => MovieMock.MovieRepoMock.Object);
                    services.AddScoped(_ => MovieMock.DataHelperMock.Object);
                    services.AddScoped(_ => MovieMock.ActorRepoMock.Object);
                    services.AddScoped(_ => MovieMock.ProducerRepoMock.Object);
                    services.AddScoped(_ => MovieMock.GenreRepoMock.Object);
                });
            }))
        {
        }

        [BeforeScenario]
        public static void Mocks()
        {
            SetupProducers();
            SetupActors();
            SetupActorMovies();
            SetupGenres();
            SetupGenreMovies();
            SetupMovies();
            MovieMock.MockProducerRepo();
            MovieMock.MockActorRepo();
            MovieMock.MockDataHelper();
            MovieMock.MockGenreRepo();
            MovieMock.MockMovieRepo();
        }
        private static void SetupProducers()
        {
            MovieMock.ListOfProducers = new List<Producer>
            {
                new Producer
                {
                    Id = 1,
                    Name = "Mock Producer",
                    Bio = "Mock bio",
                    Sex = "M",
                    DOB = DateTime.Parse("1990-12-12")
                },
                new Producer
                {
                    Id = 2,
                    Name = "Mock Producer",
                    Bio = "Mock bio",
                    Sex = "M",
                    DOB = DateTime.Parse("1990-12-12")
                }
            };
        }

        private static void SetupActors()
        {
            MovieMock.ListOfActors = new List<Actor>
            {
                new Actor
                {
                    Id = 1,
                    Name = "Mock Actor",
                    Bio = "Mock bio",
                    Sex = "M",
                    DOB = DateTime.Parse("1990-12-12")
                },
                new Actor
                {
                    Id = 2,
                    Name = "Mock Actor",
                    Bio = "Mock bio",
                    Sex = "M",
                    DOB = DateTime.Parse("1990-12-12")
                }
            };
        }

        private static void SetupActorMovies()
        {
            MovieMock.ActorsMovies = new Dictionary<int, List<string>>
            {
                {1, new List<string>{"1", "2"}},
                {2, new List<string>{"2", "1"}},
            };
        }

        private static void SetupGenres()
        {
            MovieMock.ListOfGenres = new List<Genre>
            {
                new Genre
                {
                    Id = 1,
                    Name = "Mock Comedy"
                },
                new Genre
                {
                    Id = 2,
                    Name = "Mock Action"
                }
            };
        }

        private static void SetupGenreMovies()
        {
            MovieMock.GenresMovies = new Dictionary<int, List<string>>
            {
                {1, new List<string>{"1", "2"}},
                {2, new List<string>{"2", "1"}},
            };
        }

        private static void SetupMovies()
        {
            MovieMock.ListOfMovies = new List<Movie>
            {
                new Movie
                {
                    Id = 1,
                    Name = "Mock Movie",
                    YearOfRelease = 2017,
                    Plot = "Mock Plot",
                    ProducerId = 1,
                    CoverImageUrl = "firebase URL"
                },
                new Movie
                {
                    Id = 2,
                    Name = "Mock Movie 2",
                    YearOfRelease = 2017,
                    Plot = "Mock Plot",
                    ProducerId = 2,
                    CoverImageUrl = "firebase URL"
                }
            };
        }
    }
}
