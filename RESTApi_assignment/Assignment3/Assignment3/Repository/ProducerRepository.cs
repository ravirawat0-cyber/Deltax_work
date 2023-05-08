using Assignment3.Models.DbModels;
using Assignment3.Repository.Interfaces;
using Dapper;
using System.Collections.Generic;
using System.Linq;

namespace Assignment3.Repository
{
    public class ProducerRepository : BaseRepository<Producer>
    {
        public ProducerRepository(DbContext context)
            :base(context) { }
        public int Create(Producer entity)
        {
            var query = @"INSERT INTO Producers (
	                        Name
	                        ,Bio
	                        ,DOB
	                        ,Sex
	                        )
                        VALUES (
	                        @Name
	                        ,@Bio
	                        ,@DOB
	                        ,@Sex
	                        )
                       SELECT SCOPE_IDENTITY()";

            var values = new { Name = entity.Name,
                               Bio = entity.Bio,
                               Dob = entity.DOB,
                               Sex = entity.Sex };
            var id = Create(query, values);
            return id;
        }

        public void Delete(int id)
        {
            var query = @"DELETE
                          FROM Producers
                          WHERE Id = @id";
            var value = new { Id = id };
            Delete(query, value);
        }

        public IEnumerable<Producer> GetAll()
        {
            var query = @"SELECT *
                          FROM Producers (NOLOCK)";
            return GetAll(query);
        }

        public Producer GetById(int id)
        {
            var query = @"SELECT *
                          FROM Producers (NOLOCK)
                          WHERE Id = @id";
            var value = new { Id = id };
            return GetById(query, value);
        }

        public void Update(int id, Producer entity)
        {
            var query = @"UPDATE Producers
                          SET Name = @name
	                        ,Bio = @bio
	                        ,DOB = @dob
	                        ,Sex = @sex
                          WHERE Id = @id";
            var values = new { Id = id,
                               Name = entity.Name,
                               Bio = entity.Bio,
                               DOB = entity.DOB, 
                               Sex = entity.Sex };
            Update(query, values);    
        }


    }
}
