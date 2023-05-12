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
    [Scope(Feature = "Producer Resource")]
    [Binding]
    public class ProducerSteps : BaseSteps
    {
        public ProducerSteps(CustomWebApplicationFactory factory)
            : base(factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddScoped(_ => ProducerMock.ProducerRepoMock.Object);
                });
            }))
        {
        }

        [BeforeScenario]
        public static void Mocks()
        {
            SetupProducers();
            ProducerMock.MockProducerRepo();
        }
        private static void SetupProducers()
        {
            ProducerMock.ListOfProducers = new List<Producer>
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
    }
}
