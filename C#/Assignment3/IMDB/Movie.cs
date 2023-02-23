using System.Collections.Generic;

namespace IMDB
{
    public class Movie
    {  
       
        public string Name { get; set; }
        public int YearOfRealease { get; set; }
        public string Plot { get; set; }
        public Producer Producer { get; set; }
        public List<Actor> Actors = new List<Actor>();


    }
}
