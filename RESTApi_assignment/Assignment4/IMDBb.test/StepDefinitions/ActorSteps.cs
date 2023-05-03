using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMDB.Models.DbModels;
using IMDB.test.MockResources;
using Microsoft.Extensions.DependencyInjection;

namespace IMDB.test.StepDefinitions
{
    [Scope(Feature = "Actor Resource")]
    [Binding]
    public class ActorSteps : BaseSteps
    {
        public ActorSteps(CustomWebApplicationFactory factory)
            : base(factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Mock Repo
                    services.AddScoped(_ => ActorMock.ActorRepoMock.Object);
                });
            }))
        {
        }

        [BeforeScenario]
        public static void Mocks()
        {
            ActorMock.ListOfActors = new List<Actor>
            {
                new Actor
                {
                    Id = 1,
                    Name = "Mock Actor",
                    Bio = "Tom bio",
                    Sex = "M",
                    DOB = DateTime.Parse("1990-12-12")
                },
                new Actor
                {
                    Id = 2,
                    Name = "Mock Actor",
                    Bio = "Robert bio",
                    Sex = "M",
                    DOB = DateTime.Parse("1990-12-12")
                }
            };
            ActorMock.MockActorRepo();
        }
    }
}
