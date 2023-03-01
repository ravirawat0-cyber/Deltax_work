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
        private MovieServices _movieServices = new MovieServices();
        private string name, yor, plot, actorIds, producerId;
        List<Movie> movieList = new List<Movie>();

        [Given(@"The user select option (.*)")]
        public void GivenTheUserSelectOption(int p0)
        {
            if (p0 == 1) Console.WriteLine("user selected add movie option");
        }

        [When(@"User request to add movie with following details:")]
        public void WhenUserRequestToAddMovieWithFollowingDetails(Table table)
        {
            name = table.Rows[0]["Name"];
            yor = table.Rows[0]["YearOfRelease"];
            plot = table.Rows[0]["Plot"];
            actorIds = table.Rows[0]["ActorIds"];
            producerId = table.Rows[0]["ProducerId"];
        }

        [Then(@"Movie add to the list")]
        public void ThenMovieAddToTheList()
        {
            _movieServices.AddMovie(name, yor, plot, actorIds, producerId);
        }

        [Then(@"The movie list look like this '([^']*)'")]
        public void ThenTheMovieListLookLikeThis(string expectedMovies)
        {
            var actualMovies = JsonConvert.SerializeObject(_movieServices.GetAllMovies());
            Console.WriteLine(actualMovies);
            Assert.AreEqual(actualMovies, expectedMovies);
        }

        [BeforeScenario("ListMovie")]
        public void AddSampleMovieForAdd()
        {
            _movieServices.AddMovie("IronMan", "2009", "Iron suit", "1", "1");
        }

        //List movie option 

        [Given(@"the user selects option (.*)")]
        public void GivenTheUserSelectsOption(int p0)
        {
            if(p0 == 2) Console.WriteLine("User selected list movie option");
        }

        [Then(@"fetch all the movie details")]
        public void ThenFetchAllTheMovieDetails()
        {
            movieList = _movieServices.GetAllMovies();
        }

        [Then(@"all movie detail look like this '([^']*)'")]
        public void ThenAllMovieDetailLookLikeThis(string expectedMovies)
        {
            var actualMovies = JsonConvert.SerializeObject(movieList);
            Assert.AreEqual(actualMovies, expectedMovies);
        }


        [BeforeScenario("DeleteMovie")]
        public void AddSampleMovieAdd()
        {
            _movieServices.AddMovie("IronMan", "2009", "Iron suit", "1", "1");
            _movieServices.AddMovie("Batman", "2005", "Dark knight rises" , "2,3", "1");
        }

        [Given(@"The user chose option (.*) from the available options")]
        public void GivenTheUserChoseOptionFromTheAvailableOptions(int p0)
        {
            if (p0 == 5) Console.WriteLine("User selected delete movie option");
        }

        [When(@"The user delete the movie through Id from the list (.*)")]
        public void WhenTheUserDeleteTheMovieThroughIdFromTheList(int id)
        {
           _movieServices.DeleteMovieById(id);
        }

        [Then(@"movie list look like '([^']*)'")]
        public void ThenMovieListLookLike(string expectedMovies)
        {
            var actualMovies = JsonConvert.SerializeObject(_movieServices.GetAllMovies());
            Assert.AreEqual(actualMovies, expectedMovies);
        }
    }
}
