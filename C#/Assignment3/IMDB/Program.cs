using System;
using System.Collections.Generic;

namespace IMDB
{
    partial class Program 
    {   
        static void Main(string[] args)
        {
            IMDB movieService = new IMDB();
            int flag = 0;
            while (flag == 0)
            {
                Console.WriteLine("Please select an option: " +
                                 "\n 1. Add Movie \n 2. List Movies \n 3. Movies released after 2010 \n 4. Get the name of all movies" +
                               "\n 5. Get the name and year of release of all movies \n 6. Get the latest movie whose name contains Avatar " +
                              "\n 7. Get the list of all those movies in which the actor ’Will Smith’ has acted \n 8. Exit");
                var input = int.Parse(Console.ReadLine());

                switch(input)
                {
                    case 1:
                        GetInputs(out string name, out int yor, out string plot, out string actorsIds, out string producerId);
                        try
                        {
                          //  var obj1 = new IMDB();
                            movieService.AddMovie( name, yor, plot, actorsIds, producerId);
                            Console.WriteLine("-------------New Movie added-------------");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Erorr: "+ e);
                        }
                        break;

                    case 2:
                        movieService.ListMovie();
                        break;
                    case 3:
                       
                        movieService.MoviesAfter2010();
                        break;

                    case 4:
                        movieService.MovieName();
                        break;

                    case 5:
                        movieService.NameAndYear();
                        break;
                    case 6:
                        movieService.GetLatestMoviesAvatar();
                        break;
                    case 7:
                        movieService.GetWillSmithMovies();
                        break;
                    case 8:
                        flag = 1;
                        break;
                    default:
                        Console.WriteLine("Not a valid input");
                        break;
                }
            }
             void  GetInputs(out string name, out int yor, out string plot, out string actorsIds, out string producerId)
                {
                
                Console.WriteLine("Enter name of the movie: ");
                name = Console.ReadLine();

                Console.WriteLine("Enter yor of release of the movie");
                Int32.TryParse(Console.ReadLine(), out yor);

                Console.WriteLine("Enter the plot of movie: ");
                plot = Console.ReadLine();

                Console.Write("Select actors form the list (comma seperated): \n");
                
                foreach(Actor actor in movieService.actorList)
                {
                    Console.WriteLine(actor.Id + " " + actor.Name);
                }
                actorsIds = Console.ReadLine();

                Console.WriteLine("Select producer from the list: \n");
                foreach(var producer in movieService.producerList)
                {
                    Console.WriteLine(producer.Id+" "+ producer.Name);
                }
                producerId = Console.ReadLine();
            }
        } 
    }   
}
