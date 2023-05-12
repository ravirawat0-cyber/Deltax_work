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
    public class ProducerMock 
    { 

        public static readonly Mock<IProducerRepository> ProducerRepoMock = new Mock<IProducerRepository>();

        public static List<Producer> ListOfProducers = new List<Producer>();

        public static void MockProducerRepo()
        {
            ProducerRepoMock.Setup(x => x.GetAll()).Returns(ListOfProducers);

            ProducerRepoMock.Setup(x => x.GetById(It.IsAny<int>())).Returns((int id) =>
            {
                return ListOfProducers.FirstOrDefault(x => x.Id == id);
            });

            ProducerRepoMock.Setup(x => x.Create(It.IsAny<Producer>())).Returns((Producer producer) =>
            {
                producer.Id = ListOfProducers.Count() + 1;
                ListOfProducers.Add(producer);
                return producer.Id;
            });

            ProducerRepoMock.Setup(x => x.Delete(It.IsAny<int>())).Callback((int id) =>
            {
                var producerToRemove = ListOfProducers.FirstOrDefault(x => x.Id == id);
                ListOfProducers.Remove(producerToRemove);
            });

            ProducerRepoMock.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Producer>())).Callback((int id, Producer producer) =>
            {
                var producerToUpdate = ListOfProducers.FirstOrDefault(x => x.Id == id);
                producerToUpdate.Name = producer.Name;
                producerToUpdate.Bio = producer.Bio;
                producerToUpdate.Sex = producer.Sex;
                producerToUpdate.DOB = producer.DOB;
            });
        }
    }
}
