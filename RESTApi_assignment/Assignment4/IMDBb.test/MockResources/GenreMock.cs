using IMDB.Models.DbModels;
using IMDB.Repository.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.test.MockResources
{
    public class GenreMock
    {
        public static readonly Mock<IGenreRepository> GenreRepoMock = new Mock<IGenreRepository>();
        public static List<Genre> ListOfGenres = new List<Genre>();

        public static void MockGenreRepo()
        {
            GenreRepoMock.Setup(x => x.GetAll()).Returns(ListOfGenres);

            GenreRepoMock.Setup(x => x.GetById(It.IsAny<int>())).Returns((int id) =>
            {
                return ListOfGenres.FirstOrDefault(x => x.Id == id);
            });

            GenreRepoMock.Setup(x => x.Create(It.IsAny<Genre>())).Returns((Genre genre) =>
            {
                genre.Id = ListOfGenres.Count() + 1;
                ListOfGenres.Add(genre);
                return ListOfGenres.Count();
            });

            GenreRepoMock.Setup(x => x.Delete(It.IsAny<int>())).Callback((int id) =>
            {
                var genreToRemove = ListOfGenres.FirstOrDefault(x => x.Id == id);
                if (genreToRemove != null)
                {
                    ListOfGenres.Remove(genreToRemove);
                }
            });

            GenreRepoMock.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Genre>())).Callback((int id, Genre genre) =>
            {
                var genreToUpdate = ListOfGenres.FirstOrDefault(x => x.Id == id);
                if (genreToUpdate != null)
                {
                    genreToUpdate.Name = genre.Name;
                }
            });
        }
    }
}
