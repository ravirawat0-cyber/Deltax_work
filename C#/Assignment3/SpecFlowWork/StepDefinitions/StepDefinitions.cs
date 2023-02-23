using System;
using IMDB;
using Newtonsoft.Json;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Analytics;
using TechTalk.SpecFlow.Assist;
using TechTalk.SpecFlow.CommonModels;

namespace SpecFlowWork.StepDefinitions
{
    [Binding]
    public class StepDefinitions
    {
        IMDB.IMDB movieIMDB = new IMDB.IMDB();
        public string movieName, yearOfRealease, plot, actorIds, producerId;
        List<Movie> movieList = new List<Movie>();

        [Given(@"The user select Add Movie")]
        public void GivenTheUserSelectAddMovie()
        {
            Console.WriteLine("The user select add movie option");
        }

        [When(@"User request to add movie with following details:")]
        public void WhenUserRequestToAddMovieWithFollowingDetails(Table table)
        {
            movieName = table.Rows[0]["Name"];
            yearOfRealease = table.Rows[0]["YearOfRealease"];
            plot = table.Rows[0]["Plot"];
            actorIds = table.Rows[0]["Actors"];
            producerId = table.Rows[0]["Producer"];

        }

        [Then(@"Movie add to the list")]
        public void ThenMovieAddToTheList()
        {
           
            movieIMDB.AddMovie(movieName, int.Parse(yearOfRealease), plot, actorIds, producerId);
        }                                                                             

        [Then(@"The movie list look like this '([^']*)'")]
        public void ThenTheMovieListLookLikeThis(string expectedMovies)
        {
            var actualMovies = JsonConvert.SerializeObject(movieIMDB.GetAllMovie());
            Assert.AreEqual(expectedMovies, actualMovies);
        }



        [BeforeScenario("ListMovie")]
        public void AddSampleMovieForAdd()
        {
            movieIMDB.AddMovie("IronMan" , 2009 ,"Iron suit" , "1" , "1");
        }
        // movie list
        
        [Given(@"the user select List Movie")]
        public void GivenTheUserSelectListMovie()
        {
            Console.WriteLine("User select list Movie option");
        }

        [Then(@"fetch all the movie details")]
        public void ThenFetchAllTheMovieDetails()
        {
            movieList = movieIMDB.GetAllMovie();
        }

        [Then(@"all movie detail look like this '([^']*)'")]
        public void ThenAllMovieDetailLookLikeThis(string expectedMovies)
        {
            var actualMovies = JsonConvert.SerializeObject(movieList);
            Assert.AreEqual(expectedMovies, actualMovies);
        }

      

     
    }
}
