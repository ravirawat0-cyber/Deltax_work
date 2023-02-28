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
        private MovieServices movieServices = new MovieServices();
        List<Movie> movieList = new List<Movie>();
        [Given(@"The user select Add Movie")]
        public void GivenTheUserSelectAddMovie()
        {
            Console.WriteLine("user select Add movie option ");
        }

        [When(@"User request to add movie with following details:")]
        public void WhenUserRequestToAddMovieWithFollowingDetails(Table table)
        {
            foreach (var row in table.Rows)
            {
                movieServices.AddMovie(row["Name"], int.Parse(row["YearOfRelease"]), row["Plot"], row["ActorIds"], row["ProducerId"]);
            }
        }

        [Then(@"The movie list look like this '([^']*)'")]
        public void ThenTheMovieListLookLikeThis(string expectedMovies)
        {

            var actualMovies = JsonConvert.SerializeObject(movieServices.GetAllMovies());
            Console.WriteLine(actualMovies);
            Assert.AreEqual(actualMovies, expectedMovies);
        }


        [BeforeScenario("ListMovie")]
        public void AddSampleMovieForAdd()
        {
            movieServices.AddMovie("IronMan", 2009, "Iron suit", "1", "1");
            movieServices.AddMovie("Batman", 2005, "Black suit", "1,2", "1");
        }

        //List movie option 
        [Given(@"the user select List Movie")]
        public void GivenTheUserSelectListMovie()
        {
            Console.WriteLine("user select list movie option");
        }

        [Then(@"fetch all the movie details")]
        public void ThenFetchAllTheMovieDetails()
        {
            movieList = movieServices.GetAllMovies();
        }

        [Then(@"all movie detail look like this '([^']*)'")]
        public void ThenAllMovieDetailLookLikeThis(string expectedMovies)
        {
            var actualMovies = JsonConvert.SerializeObject(movieList);
            Assert.AreEqual(actualMovies, expectedMovies);
        }

    }
}
