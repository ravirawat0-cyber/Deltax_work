using IMDB.Models.DbModels;
using IMDB.Models.Response;
using IMDB.Repository.Interfaces;
using Dapper;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Markup;

namespace IMDB.Repository
{
    public class ActorRepository : BaseRepository<Actor> , IActorRepository
    {
        private readonly DbContext _dbContext;

        public ActorRepository(DbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public int  Create(Actor entity)
        {
            var query = @"INSERT INTO Actors (
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
	                                );
                                SELECT SCOPE_IDENTITY()";
            var values = new {Name = entity.Name, 
                              Bio = entity.Bio, 
                              Dob = entity.DOB,
                              Sex = entity.Sex};
            var id = Create(query, values);
            return id;
        }

        public void Delete(int id)
        {
            var query = @"DELETE
                        FROM Actors
                        WHERE Id = @id";
            var value= new { Id = id };
            Delete(query, value);
        }

        public  IEnumerable<Actor> GetAll()
        {
            var query = @"SELECT *
                        FROM Actors (NOLOCK)";
            var actorList = GetAll(query);
            return actorList;
        }

        public Actor GetById(int id)
        {
            var query = @"SELECT *
                        FROM Actors (NOLOCK)
                        WHERE Id = @id";
            var value = new { Id = id };
            return GetById(query, value);  
        }

        public void Update(int id, Actor entity)
        {
            var query = @"UPDATE Actors
                        SET Name = @name
	                        ,Bio = @bio
	                        ,DOB = @dob
	                        ,Sex = @sex
                        WHERE Id = @id";
            var values = new  {Id = id, 
                               Name = entity.Name,
                               Bio = entity.Bio, 
                               DOB = entity.DOB,
                               Sex = entity.Sex};
            Update(query, values);  
        }

        public IEnumerable<int> CheckIdsExistInDatabase(List<string> ids)
        {  
           string query = @"Select Id
                            From Actors (NOLOCK)
                            WHERE Id IN @ActorIds";
           var value = new {ActorIds = ids};
           return CheckIdsExistInDatabase(query, value, ids);
        }

        public IEnumerable<Actor> GetByGivenIds(List<string> ids)
        {
            var query = @"Select *
                            From Actors (NOLOCK)
                            WHERE Id IN @ActorIds";
            var value = new { ActorIds = ids };
            return GetByGivenIds(query, value);
        }


    }
}
