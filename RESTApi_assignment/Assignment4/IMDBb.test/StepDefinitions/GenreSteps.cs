using IMDB.test.MockResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using IMDB.Models.DbModels;

namespace IMDB.test.StepDefinitions
{
    [Scope(Feature = "Genre Resource")]
    [Binding]
    public class GenreSteps : BaseSteps
    {
        public GenreSteps(CustomWebApplicationFactory factory)
            : base(factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddScoped(_ => GenreMock.GenreRepoMock.Object);
                });
            }))
        {
        }

        [BeforeScenario]
        public static void Mocks()
        {
            SetupGenres();
            GenreMock.MockGenreRepo();
        }
        private static void SetupGenres()
        {
            GenreMock.ListOfGenres = new List<Genre>
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
    }
}
