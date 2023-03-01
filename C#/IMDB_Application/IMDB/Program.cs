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
                    
                        string name, yor, plot, actorIds, producerId;        
                        while (true)
                        {
                            try
                            {
                                Console.WriteLine("Enter a movie Name: ");
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
                                Console.WriteLine("Enter yor of release of the movie");
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
                            try
                            {
                                Console.Write("Select actors form the list (comma seperated):\n");
                                foreach (Actor actor in actorServices.GetAllActors())
                                {
                                    Console.WriteLine(actor.Id + " " + actor.Name);
                                }
                                actorIds = Console.ReadLine();
                                string[] actorIdArray = actorIds.Split(',');
                                if (string.IsNullOrEmpty(actorIds) ) throw new Exception();
                                foreach (var id  in actorIdArray)
                                {
                                     if (int.Parse(id) > actorServices.GetAllActors().Count || !(int.TryParse(id, out int check))) throw new Exception();
                                }
                                break;
                            }
                            catch (Exception e) { Console.WriteLine("-------Enter valid Id--------"); }
                        }

                        while (true)
                        {
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
                                Console.WriteLine("Enter the Actor Name: ");
                                actorName = Console.ReadLine();
                                if(string.IsNullOrEmpty(actorName) || int.TryParse(actorName, out int valid)) throw new Exception();
                                break;
                            }
                            catch (Exception e) { Console.WriteLine("----Please enter valid actorName---- "); }
                        }
                        while (true)
                        {
                            try
                            {
                                Console.WriteLine("Enter the Date of Birth (MM/dd/yyyy): ");
                                dobActor = Console.ReadLine();
                                if (string.IsNullOrEmpty(dobActor) || !(DateTime.TryParseExact(dobActor, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))) throw new Exception();
                                break;
                            }
                            catch (Exception e) { Console.WriteLine("----Please enter valid DOB in MM/dd/yyyy---- "); }
                        }
                        actorServices.AddActor( actorName,  dobActor);
                        Console.WriteLine("--------------------------------------------");
                        break;

                    case 4:
                        string producerName, dobProducer;
                        while (true)
                        {
                            try
                            {
                                Console.WriteLine("Enter the Producer Name: ");
                                producerName = Console.ReadLine();
                                if (string.IsNullOrEmpty(producerName) || int.TryParse(producerName, out int valid)) throw new Exception();
                                break;
                            }
                            catch (Exception e) { Console.WriteLine("----Please enter valid producerName---- "); }
                        }
                        while (true)
                        {
                            try
                            {
                                Console.WriteLine("Enter the Date of Birth (MM/dd/yyyy): ");
                                dobProducer = Console.ReadLine();
                                // if (string.IsNullOrEmpty(dobProducer) || int.TryParse(producerName, out int valid)) throw new Exception();
                                if (string.IsNullOrEmpty(dobProducer) ||!(DateTime.TryParseExact(dobProducer, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))) throw new Exception();
                                break;
                            }
                            catch (Exception e) { Console.WriteLine("----Please enter valid DOB in MM/dd/yyyy---- "); }
                        }
                        producerServices.AddProducer(producerName, dobProducer);
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
                                Console.WriteLine("Enter the Id to Delete Movie: ");
                                foreach (var obj in movieServices.GetAllMovies())
                                {
                                    Console.WriteLine(obj.Id + " " + obj.Name);
                                }
                                var movieId = int.Parse(Console.ReadLine());
                                movieServices.DeleteMovieById(movieId);
                                Console.WriteLine("-------------Movie is Deleted------------");
                                break;
                            }
                            catch (Exception e) { Console.WriteLine("----Please enter valid ID----- "); }
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