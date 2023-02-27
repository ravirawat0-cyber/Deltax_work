using IMDB.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Services.Interfaces
{
    public interface IMovieServices
    {
        List<Movie> GetAllMovies();
        void AddMovie(string name, int yearOfRelease, string plot, string actorIds, string producerId);
        void DeleteMovieById(int id);
    }
}
