using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace CsharpAdvanced.Steps
{
     [Binding]
    class AddMoviesSteps
    {
        [Given(@"the user wants to add a new movie")]
        public void GivenTheUserWantsToAddANewMovie()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"the user select ""(.*)"" option")]
        public void WhenTheUserSelectOption(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"enters movie details \(name, year of release, plot\)")]
        public void WhenEntersMovieDetailsNameYearOfReleasePlot()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"selects actors from the list")]
        public void WhenSelectsActorsFromTheList()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"selects a producer from the list")]
        public void WhenSelectsAProducerFromTheList()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the movie is added to the movie list")]
        public void ThenTheMovieIsAddedToTheMovieList()
        {
            ScenarioContext.Current.Pending();
        }

    }
}
