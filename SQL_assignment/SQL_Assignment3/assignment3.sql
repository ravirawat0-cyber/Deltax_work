use IMDB;

--1. Write a query to get the age of the Actors in Days(Number of days).
SELECT id,
		NAME,
		sex,
		dob,
		bio,
		Datediff(year, dob, Getdate()) AS AgeInDays
FROM   foundation.actors; 

--2. Write a query to get the list of Actors who have worked with a given producer X.
SELECT a.*
FROM   foundation.actors a
       INNER JOIN foundation.actors_movies am
               ON a.id = am.actorid
       INNER JOIN foundation.movies m
               ON m.id = am.movieid   
       INNER JOIN foundation.producers p
               ON p.id = m.producerid
WHERE  p.NAME = 'Kevin Feige'; 

--3. Write a query to get the list of actors who have acted together in two or more movies.
SELECT A1.NAME,
       A2.NAME,
       Count(*) AS NumMoviesActedTogether
FROM   foundation.actors_movies AM1
       INNER JOIN foundation.actors_movies AM2
               ON AM1.movieid = AM2.movieid
                  AND AM1.actorid > AM2.actorid
       INNER JOIN foundation.actors A1
               ON A1.id = AM1.actorid
       INNER JOIN foundation.actors A2
               ON A2.id = AM2.actorid
       INNER JOIN foundation.movies M
               ON M.id = AM1.movieid
GROUP  BY A1.NAME,
          A2.NAME
HAVING Count(*) >= 2
ORDER BY nummoviesactedtogether DESC; 


--4. Write a query to get the youngest actor.
SELECT TOP 1 * FROM Foundation.Actors 
ORDER BY Dob;

--5. Write a query to get the actors who have never worked together.
SELECT DISTINCT A1.NAME
FROM   foundation.actors A1
       LEFT JOIN foundation.actors_movies AM1
              ON A1.id = AM1.actorid
       LEFT JOIN foundation.actors_movies AM2
              ON AM1.movieid = AM2.movieid
       LEFT JOIN foundation.actors A2
              ON AM2.actorid = A2.id
                 AND A1.id <> A2.id
WHERE A2.id IS NULL; 

--6. Write a query to get the number of movies in each language.
SELECT language, Count(language) AS no
FROM foundation.movies
GROUP BY language; 

--7. Write a query to get me the total profit of all the movies in each language separately.
SELECT Language, SUM(profit) AS 'Total Profit'
FROM Foundation.Movies
GROUP BY Language;

--8. Write a query to get the total profit of movies which have actor X in each language.
SELECT m.NAME,
       Sum(profit)
FROM   foundation.movies m
       INNER JOIN foundation.actors_movies am
               ON am.movieid = m.id
       INNER JOIN foundation.actors a
               ON a.id = am.actorid
WHERE  a.NAME = 'Robert Downey Jr.'
GROUP  BY m.NAME; 


--------------Stored Procedures------------
--1. Write an SP to insert a movie:
	--Take the movie details
	--Take the Actor Details ( Actors IDs)
	--Takes the producer Details (Producer IDs)
	--Adds to the required tables.

CREATE PROCEDURE usp_InsertMovie
(
  @Name VARCHAR(255),
  @YearOfRelease INT,
  @Plot VARCHAR(MAX),
  @Poster VARCHAR(MAX),
  @ProducerID INT,
  @ActorIDs VARCHAR(MAX)
)
AS 
BEGIN 
	--Insert movie details into Movies table
	DECLARE @MovieID INT;
	INSERT INTO Foundation.Movies(Name, YearOfRelease, Plot, Poster, ProducerID)
	VALUES (@Name, @YearOfRelease, @Plot, @Poster, @ProducerID);
	SET @MovieID = SCOPE_IDENTITY();

	-- Associate the actors with the new movie in the Actors_Movies table
    INSERT INTO Foundation.Actors_Movies(MovieID, ActorID)
	SELECT @MovieID, value FROM STRING_SPLIT(@ActorIDs, ',');
END

usp_InsertMovie
	@Name = 'The Ant Man',
	@YearOfRelease = 2022,
	@Plot = 'Moive based on Quantum',
	@Poster = 'URL',
	@ProducerID = 4,
	@ActorIDs = '2,4,5';

--2.Write an SP to Delete the Movie
  --Takes the movie Id
CREATE PROCEDURE usp_DeleteMovieWithID
(
  @MovieID INT
)
AS
BEGIN 
	DELETE FROM Foundation.Actors_Movies WHERE MovieID = @MovieID;
	DELETE FROM Foundation.Movies WHERE ID = @MovieID;
END

usp_DeleteMovieWithID @MovieID = 7;

--3.Write an SP to Delete a Producer
   -- Takes ProducerId
   -- Delete the movies directed by the producer as well
CREATE PROCEDURE usp_DeleteProducerWithID
(
	@ProducerID INT
)
AS 
BEGIN 

	DECLARE @MovIeToDelete TABLE (ID INT);
	-- Get all the movies directed by the producer and add it to temporary table
	INSERT INTO @MovieToDelete (ID)
	SELECT ID FROM Foundation.Movies WHERE ProducerID = @ProducerID;

	-- Delete all associated actor-movie records
	DELETE FROM Foundation.Actors_Movies WHERE MovieID IN (SELECT ID FROM @MovIeToDelete);

	-- Delete all Movie produced by producer in Foundation.Movies table
	DELETE FROM Foundation.Movies WHERE ProducerID = @ProducerID;

	-- Delete the producer record 
	DELETE FROM Foundation.Producers WHERE ID = @ProducerID;
END 

usp_DeleteProducerWithID @ProducerID = 1;




--4. Write an SP to Delete a Actor
  --Takes ActorId

CREATE PROCEDURE usp_DeleteActorWithID
(
  @ActorID INT
)
AS
BEGIN
  -- Delete all actor-movie association records for the actor to be deleted
  DELETE FROM Foundation.Actors_Movies WHERE ActorID = @ActorID;
  
  -- Delete the actor record
  DELETE FROM Foundation.Actors WHERE ID = @ActorID;
  
END

usp_DeleteActorWithID @ActorID = 5;

