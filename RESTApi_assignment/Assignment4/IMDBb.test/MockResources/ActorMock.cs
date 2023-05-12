using IMDB.Models.DbModels;
using IMDB.Repository;
using IMDB.Repository.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.test.MockResources
{
    public class ActorMock
    {
        public static readonly Mock<IActorRepository> ActorRepoMock = new Mock<IActorRepository>();

        public static List<Actor> ListOfActors = new List<Actor>();

        public static void MockActorRepo()
        {
            ActorRepoMock.Setup(x => x.GetAll()).Returns(ListOfActors);

            ActorRepoMock.Setup(x => x.GetById(It.IsAny<int>())).Returns((int id) =>
            {
                return ListOfActors.FirstOrDefault(x => x.Id == id);
            });

            ActorRepoMock.Setup(x => x.Create(It.IsAny<Actor>())).Returns((Actor actor) =>
            {
                actor.Id = ListOfActors.Count() + 1;
                ListOfActors.Add(actor);
                return actor.Id;
            });

            ActorRepoMock.Setup(x => x.Delete(It.IsAny<int>())).Callback((int id) =>
            {
                var actorToRemove = ListOfActors.FirstOrDefault(x => x.Id == id);
                ListOfActors.Remove(actorToRemove);   
            });

            ActorRepoMock.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Actor>())).Callback((int id, Actor actor) =>
            {
                var actorToUpdate = ListOfActors.FirstOrDefault(x => x.Id == id);
                if (actorToUpdate != null)
                {
                    actorToUpdate.Name = actor.Name;
                    actorToUpdate.Bio = actor.Bio;
                    actorToUpdate.Sex = actor.Sex;
                    actorToUpdate.DOB = actor.DOB;
                }
            });
        }
    }
}
