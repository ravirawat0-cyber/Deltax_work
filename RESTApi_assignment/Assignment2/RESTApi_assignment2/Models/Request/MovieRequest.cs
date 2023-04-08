using System.Collections.Generic;

namespace RESTApi_assignment2.Models.Request
{
    public class MovieRequest
    {
        public string Name { get; set; }
        public int YearOfRelease { get; set; }
        public string Plot { get; set; }
        public List<int> ActorIds { get; set; }
        public List<int> GenreIds { get; set; }
        public int ProducerId { get; set; }
        public string CoverImageUrl { get; set; }
    }
}
