using System.Collections.Generic;

namespace Assignment3.Models.Request
{
    public class MovieRequest
    {
        public string Name { get; set; }
        public int YearOfRelease { get; set; }
        public string Plot { get; set; }
        public string ActorIds { get; set; }
        public string GenreIds { get; set; }
        public int ProducerId { get; set; }
        public string CoverImageUrl { get; set; }
    }
}
