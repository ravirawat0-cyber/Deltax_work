using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using IMDB.Domain;
using IMDB.Repository;
using IMDB.Services;
using IMDB.Services.Interfaces;

namespace IMDB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            IActorServices actorServices = new ActorServices();
            IProducerService producerServices = new ProducerServices();
            var movieServices = new MovieServices();
            int flag = 0;
            while (flag == 0)
            {

                Console.WriteLine("Please select an option: " +
                                  "\n 1. Add Movie \n 2. List Movies \n 3. Add Actor \n 4. Add Producer" +
                                  "\n 5. Delete Movie \n 6. Exit ");
                Console.WriteLine("What do you want to do ?");
                var input = int.Parse(Console.ReadLine());
                switch (input)
                {
                    case 1:
                        try
                        {
                            Console.WriteLine("Enter name of the movie: ");
                            string name = Console.ReadLine();
                            Console.WriteLine("Enter yor of release of the movie");
                            int yor = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter the plot of movie: ");
                            string plot = Console.ReadLine();
                            Console.Write("Select actors form the list (comma seperated):\n ");
                            foreach (Actor actor in actorServices.GetAllActors())
                            {
                                Console.WriteLine(actor.Id + " " + actor.Name);
                            }
                            string actorIds = Console.ReadLine();
                            Console.WriteLine("Select producer from the list: ");
                            foreach (var producer in producerServices.GetAllProducers())
                            {
                                Console.WriteLine(producer.Id + " " + producer.Name);
                            }
                            string producerId = Console.ReadLine();
                            movieServices.AddMovie(name, yor, plot, actorIds, producerId);
                            Console.WriteLine("-------------New Movie added-------------");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        break;

                    case 2:
                        if (movieServices.GetAllMovies().Count > 0)
                        {
                            foreach (var obj in movieServices.GetAllMovies())
                            {
                                Console.WriteLine("Movie name: " + obj.Name);
                                Console.WriteLine("Year of Release: " + obj.YearOfRelease);
                                foreach (var actor in obj.Actors)
                                {
                                    Console.WriteLine(actor.Name + ",");
                                }

                                Console.WriteLine("Producer: " + obj.Producer.Name);
                                Console.WriteLine("-------------------------------------------");
                            }
                          
                        }
                        else
                        {
                            Console.WriteLine("---------There is no movie stored----------");
                        }
                        break;

                    case 3:
                        Console.WriteLine("Enter the Actor Name: ");
                        string actorName = Console.ReadLine();

                        Console.WriteLine("Enter the DOB in MM/DD/YY: ");
                        string dobActor = Console.ReadLine();

                        actorServices.AddActor( actorName,  dobActor);
                        Console.WriteLine("--------------------------------------------");
                        break;

                    case 4:
                        Console.WriteLine("Enter the Producer Name: ");
                        string producerName = Console.ReadLine();
                        Console.WriteLine("Enter the DOB in MM/DD/YY: ");
                        string dobProducer = Console.ReadLine();

                        producerServices.AddProducer(producerName, dobProducer);
                        break;

                    case 5:
                        if (movieServices.GetAllMovies().Count > 0)
                        {
                            foreach (var obj in movieServices.GetAllMovies())
                            {
                                Console.WriteLine(obj.Id + " " + obj.Name);
                            }
                            var movieId = int.Parse(Console.ReadLine());
                            movieServices.DeleteMovieById(movieId);
                            Console.WriteLine("-------------Movie is Deleted------------");
                        }
                        else
                        {
                            Console.WriteLine("There is No movie stored ");
                        }
                        break;
                    case 6:
                        flag = 1;
                        Console.WriteLine("Thank you");
                        break;
                    default:
                        Console.WriteLine("Not a valid input");
                        break;
                }

            }

           
        }
    }
}