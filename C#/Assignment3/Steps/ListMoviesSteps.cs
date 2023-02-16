using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace CsharpAdvanced.Steps
{    
    [Binding]
    class ListMoviesSteps
    {
        [Given(@"the user wants to view a list of all movies")]
        public void GivenTheUserWantsToViewAListOfAllMovies()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"the user selects the ""(.*)"" option")]
        public void WhenTheUserSelectsTheOption(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"a list of all movies with their actors and producers is displayed")]
        public void ThenAListOfAllMoviesWithTheirActorsAndProducersIsDisplayed()
        {
            ScenarioContext.Current.Pending();
        }

    }
}
