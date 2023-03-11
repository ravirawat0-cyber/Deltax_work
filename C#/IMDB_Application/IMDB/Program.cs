using System.Diagnostics.Metrics;
using System;
using System.Globalization;
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
            var movieServices = new MovieServices(actorServices, producerServices);
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
                            string name, yor, plot, actorIds, producerId;
                            while (true)
                            {
                                try
                                {
                                    Console.WriteLine("Enter a movie Name : ");
                                    name = Console.ReadLine();
                             
                                    if (string.IsNullOrEmpty(name)) throw new Exception();
                                    break;
                                }
                                catch (Exception e) { Console.WriteLine("----Please enter valid movie name-----"); }
                            }

                            while (true)
                            {
                                try
                                {
                                    Console.WriteLine("Enter yor of release of the movie: ");
                                    yor = Console.ReadLine();
                                    if (yor == null || !(int.TryParse(yor, out int check))) throw new Exception();
                                    break;
                                }
                                catch (Exception e) { Console.WriteLine("----Please enter valid Year of Release-----"); }
                            }
                            while (true)
                            {
                                try
                                {
                                    Console.WriteLine("Enter the plot of movie: ");
                                    plot = Console.ReadLine();
                                    if (string.IsNullOrEmpty(plot)) throw new Exception();
                                    break;
                                }
                                catch (Exception e) { Console.WriteLine("----Movie plot cannot be empty-----"); }
                            }

                            while (true)
                            {
                                if (actorServices.GetAllActors().Count == 0)
                                    throw new Exception("----There is no Actor in the list Please enter actor first----");
                                try
                                {
                                    Console.Write("Select actors form the list (comma seperated):\n");
                                    foreach (Actor actor in actorServices.GetAllActors())
                                    {
                                        Console.WriteLine(actor.Id + " " + actor.Name);
                                    }
                                    actorIds = Console.ReadLine();
                                    string[] actorIdArray = actorIds.Split(',');
                                    if (string.IsNullOrEmpty(actorIds)) throw new Exception();
                                    foreach (var id in actorIdArray)
                                    {
                                        if (int.Parse(id) > actorServices.GetAllActors().Count || !(int.TryParse(id, out int check))) throw new Exception("-------Enter valid Id--------");
                                    }
                                    break;
                                }
                                catch (Exception e) { Console.WriteLine(e.Message); }
                            }

                            while (true)
                            {
                                if (producerServices.GetAllProducers().Count == 0)
                                    throw new Exception("----There is no Producer in the list Please enter producer first----");
                                try
                                {

                                    Console.WriteLine("Select producer from the list: ");
                                    foreach (var producer in producerServices.GetAllProducers())
                                    {
                                        Console.WriteLine(producer.Id + " " + producer.Name);
                                    }
                                    producerId = Console.ReadLine();
                                    if (string.IsNullOrEmpty(producerId) || int.Parse(producerId) > producerServices.GetAllProducers().Count || !(int.TryParse(producerId, out int check))) throw new Exception();
                                    break;
                                }
                                catch (Exception e) { Console.WriteLine("-------Enter valid Id--------"); }
                            }
                            movieServices.AddMovie(name, yor, plot, actorIds, producerId);
                            Console.WriteLine("-------------New Movie added-------------");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            break;
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
                        string actorName, dobActor;
                        while (true)
                        {
                            try
                            {
                                Console.WriteLine("Enter the Actor Name (or type 'q' to exit): ");
                                actorName = Console.ReadLine();
                                if (actorName == "q") break;
                                if (int.TryParse(actorName, out int valid) || string.IsNullOrEmpty(actorName)) throw new Exception("---Actor Name cannot be empty or number---");

                                Console.WriteLine("Enter the Date of Birth (MM/dd/yyyy) (or type 'q' to exit): ");
                                dobActor = Console.ReadLine();
                                if (dobActor == "q") break;
                                if (string.IsNullOrEmpty(dobActor) || !(DateTime.TryParseExact(dobActor, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))) throw new Exception("--Actor DOB is not in correct format or empty--");

                                actorServices.AddActor(actorName, dobActor);
                                Console.WriteLine("Actor added successfully!");
                                break;  //for multiple input just need to remove this break 
                            }
                            catch (Exception e) { Console.WriteLine(e.Message); }
                        }
                        Console.WriteLine("--------------------------------------------");
                        break;


                    case 4:
                        string producerName, dobProducer;
                        while (true)
                        {
                            try
                            {
                                Console.WriteLine("Enter the Producer Name (or type 'q' to exit): ");
                                producerName = Console.ReadLine();
                                if (producerName == "q") break;
                                if (int.TryParse(producerName, out int valid) || string.IsNullOrEmpty(producerName)) throw new Exception("---Producer Name cannot be empty or number---");

                                Console.WriteLine("Enter the Date of Birth (MM/dd/yyyy) (or type 'q' to exit): ");
                                dobProducer = Console.ReadLine();
                                if (dobProducer == "q") break;
                                if (string.IsNullOrEmpty(dobProducer) || !(DateTime.TryParseExact(dobProducer, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))) throw new Exception("---Producer DOB is empty or not in correct format---");

                                producerServices.AddProducer(producerName, dobProducer);
                                Console.WriteLine("Producer added successfully!");
                                break;  //for multiple input just need to remove this break 
                            }
                            catch (AggregateException e)
                            {
                                Console.WriteLine("interger name");
                            }
                            catch (Exception e) { Console.WriteLine(e.Message); }
                         
                        }
                        Console.WriteLine("--------------------------------------------");
                        break;

                    case 5:
                        while (true)
                        {
                            try
                            {
                                if (movieServices.GetAllMovies().Count == 0)
                                {
                                    Console.WriteLine("-------There is no movie to delete--------");
                                    break;
                                }
                                Console.WriteLine("Enter the Id to Delete Movie (or type 'q' to quit): ");
                                foreach (var obj in movieServices.GetAllMovies())
                                {
                                    Console.WriteLine(obj.Id + " " + obj.Name);
                                }
                                var IdorQuit = Console.ReadLine();
                                if (IdorQuit == "q")
                                {
                                    Console.WriteLine("Exiting delete movie menu...");
                                    break;
                                }
                                var movieId = int.Parse(IdorQuit);
                                movieServices.DeleteMovieById(movieId);
                                Console.WriteLine("-------------Movie is Deleted------------");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("----Please enter valid ID or 'q' to quit----- ");
                            }
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