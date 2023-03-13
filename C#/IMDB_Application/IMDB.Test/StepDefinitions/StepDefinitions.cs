using IMDB.Domain;
using IMDB.Services;
using IMDB.Services.Interfaces;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Xml.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace IMDB.Test.StepDefinitions
{
    [Binding]
    public class StepDefinitions
    {
        private static IActorServices _actorService = new ActorServices();
        private static IProducerService _producerService = new ProducerServices();
        private IMovieServices _movieService = new MovieServices(_actorService, _producerService);
       
        private string _response;
        private Exception _exception;
        private List<Movie> _movieList;

        [BeforeScenario("AddMovies", "ListMovies", "DeleteMovie")]
        public void AddDefault()
        {
            _actorService.AddActor("Robert Downey Jr.", "03/15/1990");
            _actorService.AddActor("Chris Hemsworth", "08/11/1983");
            _actorService.AddActor("Ryan Gosling", "06/12/1990");
            _producerService.AddProducer("Kevin Feige", "06/02/1973");
            _producerService.AddProducer("Mark Johnson", "09/12/2009");
            _movieService.AddMovie("Iron Man", "2009", "Iron suit", "1,2", "1");
            _movieService.AddMovie("Batman", "2005", "Dark knight rises", "3", "2");
        }

        [When(@"I request to add a movie with details: Name: '([^']*)', YearOfRelease: '([^']*)', Plot '([^']*)', Actors: '([^']*)', Producer: '([^']*)'")]
        public void WhenIRequestToAddAMovieWithDetailsNameYearOfReleasePlotActorsProducer(string name, string yor, string plot, string actorIds, string producerId)
        {
            try
            {
                _response = _movieService.AddMovie(name, yor, plot, actorIds, producerId);
            }
            catch (Exception e)
            {
                _exception = e;
            }
        }



        [Then(@"the response message should be sent (.*)"), Scope(Tag = "ValidCase" ) ]
        public void ThenTheResponseMessageShouldBeSent(string expectedMessage)
        {
            Assert.AreEqual(expectedMessage, _response);
        }

        [Then(@"the response message should be sent (.*)"), Scope(Tag = "InvalidCase") ]
        public void ThenTheErrorMessageShouldBeSent(string expectedMessage)
        {
            Assert.AreEqual(expectedMessage, _exception.Message);
        }

        [When(@"the user requests to list the movies")]
        public void WhenTheUserRequestsToListTheMovies()
        {
            _movieList = _movieService.GetAllMovies();
        }

        [Then(@"the response data should be")]
        public void ThenTheResponseDataShouldBe(Table table)
        {
            var listComparison = _movieList.Select(movie => new
            {
                movie.Id,
                movie.Name,
                movie.YearOfRelease,
                movie.Plot,
                Actors = JsonConvert.SerializeObject(movie.Actors),
                Producer = JsonConvert.SerializeObject(movie.Producer),
            }).ToList();
            table.CompareToSet(listComparison);
        }


        [When(@"The user want to delete movie with id: '([^']*)'")]
        public void WhenTheUserWantToDeleteMovieWithId(string id)
        {
            try
            {
                _response = _movieService.DeleteMovieById(int.Parse(id));
            }
            catch (Exception e)
            {
                _exception = e;
            }
        }
  

      





    }
}
