using System;
using IMDB.Domain;
using IMDB.Services;
using IMDB.Services.Interfaces;
using Newtonsoft.Json;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace IMDB.Test.StepDefinitions
{
    [Binding]
    public class StepDefinitions
    {
      
        static IActorServices _actorServices = new ActorServices(); 
        static IProducerService _producerServices = new ProducerServices();
        private MovieServices _movieServices = new MovieServices(_actorServices, _producerServices);

        private string name, yor, plot, actorIds, producerId;
        List<Movie> movieList = new List<Movie>();
        private Exception _exception;

        [Given(@"User provide following details:")]
        public void GivenUserProvideFollowingDetails(Table table)
        {
            name = table.Rows[0]["Name"];
            yor = table.Rows[0]["YearOfRelease"];
            plot = table.Rows[0]["Plot"];
            actorIds = table.Rows[0]["ActorIds"];
            producerId = table.Rows[0]["ProducerId"];
        }

        [When(@"The Movie is added to the list")]
        public void WhenTheMovieIsAddedToTheList()
        {
            try
            {
                _movieServices.AddMovie(name, yor, plot, actorIds, producerId);
            }
            catch (Exception e)
            {
                _exception = e;
            }
        }

        [Then(@"The movie list look like this '([^']*)'")]
        public void ThenTheMovieListLookLikeThis(string expectedMovies)
        {
            var actualMovies = JsonConvert.SerializeObject(_movieServices.GetAllMovies());
            Assert.AreEqual(actualMovies, expectedMovies);
        }

        [Given(@"fetch all the movie details")]
        public void ThenFetchAllTheMovieDetails()
        {
            movieList = _movieServices.GetAllMovies();
        }

        [Given(@"The user want to delete movie with id (.*)")]
        public void WhenTheUserWantToDeleteMovieWithId(int id)
        {
            try
            {
                _movieServices.DeleteMovieById(id);
            }
            catch (Exception e)
            {
                _exception = e;
            }
        }

        [Then(@"The response message should be '([^']*)'")]
        public void ThenTheResponseMessageShouldBe(string expectedResponse)
        {
            Assert.AreEqual(expectedResponse, _exception.Message);
        }

        [BeforeScenario("AddMovie","ListMovie", "DeleteMovie" )]
        public void AddSampleMovieForAdd()
        {
            _actorServices.AddActor("Robert Downey Jr." , "03/15/1990");
            _actorServices.AddActor("Chris Hemsworth", "08/11/1983");
            _producerServices.AddProducer("Kevin Feige", "06/02/1973");
            _movieServices.AddMovie("IronMan", "2009", "Iron suit", "1", "1");
            _movieServices.AddMovie("Batman", "2005", "Dark knight rises", "2", "1");
            
        }
    }
}
