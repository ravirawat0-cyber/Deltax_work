using System.Collections.Specialized;

namespace IMDB.Models.Response
{
    public class ReviewResponse
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string Message { get; set; }
    }
}
