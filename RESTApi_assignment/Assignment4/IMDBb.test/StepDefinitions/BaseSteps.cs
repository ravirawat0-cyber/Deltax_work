using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text;
using Xunit;

namespace IMDB.test.StepDefinitions
{
    [Binding]
    public class BaseSteps
    {
        protected WebApplicationFactory<TestStartup> Factory;
        protected HttpClient Client { get; set; }
        protected HttpResponseMessage Response { get; set; }

        public BaseSteps(WebApplicationFactory<TestStartup> baseFactory)
        {
            Factory = baseFactory;
        }

        [Given(@"I am a client")]
        public void ImClient()
        {
            Client = Factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri($"http://localhost/")
            });
        }

        [When(@"I make GET Request '(.*)'")]
        public virtual async Task MakeGet(string resourceEndpoint)
        {
            var uri = new Uri(resourceEndpoint, UriKind.Relative);
            Response = await Client.GetAsync(uri);
        }

        [Then(@"response code must be '(.*)'")]
        public void ResponseCompare(int statusCode)
        {
            var expectedStatusCode = (HttpStatusCode)statusCode;
            Assert.Equal(expectedStatusCode, Response.StatusCode);
        }

        [Then(@"response data must look like '(.*)'")]
        public void CompareResponse(string p0)
        {
            var responseData = Response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            Assert.Equal(p0, responseData);
        }

        [When(@"I am make a POST request to '([^']*)' with the following Data '([^']*)'")]
        public virtual async Task MakePost(string resourceEndPoint,
            string postDataJson)
        {
            var postRelativeUri = new Uri(resourceEndPoint, UriKind.Relative);
            var content = new StringContent(postDataJson, Encoding.UTF8, "application/json");
            Response = await Client.PostAsync(postRelativeUri, content);
        }

        [When(@"I make PUT Request '([^']*)' with the following Data '([^']*)'")]
        public virtual async Task MakePut(string resourceEndPoint, string putDataJson)
        {
            var postRelativeUri = new Uri(resourceEndPoint, UriKind.Relative);
            var content = new StringContent(putDataJson, Encoding.UTF8, "application/json");
            Response = await Client.PutAsync(postRelativeUri, content);
        }

        [When(@"I make Delete Request '(.*)'")]
        public virtual async Task MakeDelete(string resourceEndPoint)
        {
            var postRelativeUri = new Uri(resourceEndPoint, UriKind.Relative);
            Response = await Client.DeleteAsync(postRelativeUri);
        }
    }
}