using System.Collections.Specialized;

namespace RESTApi_assignment2.Models.Response
{
    public class ReviewResponse
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string Message { get; set; }
    }
}
