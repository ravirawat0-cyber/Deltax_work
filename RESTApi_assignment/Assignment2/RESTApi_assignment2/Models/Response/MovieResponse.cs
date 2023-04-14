using System.Collections.Generic;

namespace RESTApi_assignment2.Models.Response
{
    public class MovieResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int YearOfRelease { get; set; }
        public string Plot { get; set; }
        public ProducerRespone Producer { get; set; }
        public string CoverImageUrl { get; set; }

        public List<ActorRespone> Actors { get; set; }

        public List<GenreResponse> Genres { get; set; }

    }
}
