using System;
using System.Collections.Generic;
using System.Linq;

namespace IMDB
{
    public class IMDB
    {
       private List<Movie> allMovies = new List<Movie>();
       public List<Actor> actorList = new List<Actor>()
        {
            new Actor(1,"Robert Downey Jr."),
            new Actor(2,"Chris Hemsworth"),
            new Actor(3, "Chris Evans"),
            new Actor(4, "Will Smith"),
            new Actor(5, "Henry Cavill")
        };
       public List<Producer> producerList = new List<Producer>()
        {
            new Producer(1,"Kevin Feige"),
            new Producer(2, "Taika waititi"),
            new Producer(3, "Jhon salley"),
            new Producer(4, "Richard Lester")
        };
       public List<Movie> GetAllMovie()
        {
            return allMovies;
        }
       public void AddMovie( string name ,int yor,string plot, string actorIds, string producerId)
        {
            var movie = new Movie();
            if (string.IsNullOrEmpty(name)) throw new Exception("Movie name cannot be empty");
            if (yor == 0) throw new Exception("Please enter year of release");
            if (string.IsNullOrEmpty(plot)) throw new Exception("Please enter the plot of the movie");
            if (string.IsNullOrEmpty(actorIds)) throw new Exception("Please select actor id from the list");
            if (string.IsNullOrEmpty(producerId)) throw new Exception("Please select producer id from the list");
            movie.Name = name;
            movie.YearOfRealease = yor;
            movie.Plot = plot;
            string[] actorId = actorIds.Split(',');
           
            foreach(var index in actorId)
            {
               
                movie.Actors.Add(SearchList(actorList, index));
            }
            movie.Producer = SearchList(producerList, producerId);
            allMovies.Add(movie);
        }

       public T SearchList<T>(List<T> nameList, string index) where T: Person
        {
            try
            {
                int id = Convert.ToInt32(index);
                var result = nameList.FirstOrDefault(person => person.Id == id);
                return result;
            }
            catch (InvalidCastException)
            {
                throw new Exception("Please select only numbers to choose actors");
            }
            catch (InvalidOperationException)
            {
                throw new Exception("Please select indexes from the given list");
            }
        }
       public void ListMovie()
        {
            if (allMovies.Count == 0) throw new Exception("-------------NO Movie Add-----------");
            foreach (var obj in allMovies)
            {   
                Console.WriteLine("Movie name: " + obj.Name);
                Console.WriteLine("Year of Release: " + obj.YearOfRealease);
                foreach (var actor in obj.Actors)
                {
                    Console.WriteLine(actor.Name + ",");
                }
                Console.WriteLine("Producer: " + obj.Producer.Name);
                Console.WriteLine("----------------------------------------");
            }
        }
        // Get the list of all movies released after year 2010
       public void MoviesAfter2010()
        {
            Console.WriteLine("--------Movie released after 2010--------");
            List<Movie> recentMovies = allMovies.Where(m => m.YearOfRealease > 2010).ToList();
            foreach(Movie mov in recentMovies)
            {
                Console.WriteLine(mov.Name);

            }
            Console.WriteLine("-----------------------------------------");
        }
       // Get the name of all movies
       public void MovieName()
        {
            Console.WriteLine("--------All the name of Movies--------");
            foreach (Movie mov in allMovies)
            {
                Console.WriteLine(mov.Name);
            }
            Console.WriteLine("---------------------------------------");
        }
        //Get the name and year of release of all movies
       public void NameAndYear()
        {
            Console.WriteLine("------------------Name and the Year of movies--------------------");
            foreach (Movie mov in allMovies)
            {   
              Console.WriteLine($"Name: {mov.Name}\nYear of Release: {mov.YearOfRealease}\n");    
            }
            Console.WriteLine("------------------------------------------------------------------");
        }
     // Get the latest movie whose name contains ‘Avatar’
       public void GetLatestMoviesAvatar()
        {  

            Console.WriteLine("--------Latest movies contains whose name contains avatar --------");
            Movie latestAvatarMovie = allMovies.Where(m => m.Name.Contains("Avatar")).OrderByDescending(m => m.YearOfRealease).FirstOrDefault();
            if (latestAvatarMovie != null) Console.WriteLine(latestAvatarMovie.Name);
            else Console.WriteLine("There is no latest movie with Avatar");
            Console.WriteLine("------------------------------------------------------------------");
        }
        //Get the list of all those movies in which the actor ’Will Smith’ has acted
       public void GetWillSmithMovies()
        {
            Console.WriteLine("--------Movies in which the actor Will Smith worked as actor--------");
            List<Movie> willSmithMovies = allMovies.Where(m => m.Actors.Any(a => a.Name == "Will Smith" )).ToList();
            if (willSmithMovies.Count > 0)
            {
                foreach (Movie mov in willSmithMovies)
                {
                    Console.WriteLine(mov.Name);
                }
            }
            else Console.WriteLine("There is no movie in which actor will smith worked as actor");
            Console.WriteLine("---------------------------------------------------------------------");
        }    
    }
}
