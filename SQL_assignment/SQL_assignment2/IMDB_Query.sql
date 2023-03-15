--IMDB Database Creation:

--Create a new database called IMDB.
CREATE DATABASE IMDB;
USE IMDB;

--Create a new schema called Foundation.
CREATE SCHEMA Foundation;

--Use the schema to create the tables in the design created in Assignment 1.
CREATE TABLE Foundation.Producers(
   ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY  ,
   Name VARCHAR(255),
   Sex VARCHAR(6),
   Dob DATE,
   Bio VARCHAR(MAX)
);

CREATE TABLE Foundation.Actors(
   ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
   Name VARCHAR(255),
   Sex VARCHAR(6),
   Dob DATE,
   Bio VARCHAR(MAX)
);

CREATE TABLE Foundation.Movies(
  ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
  Name VARCHAR(255) ,
  YearOfRelease INT,
  Plot VARCHAR(MAX),
  Poster VARCHAR(MAX),
  ProducerID INT NOT NULL
);

CREATE TABLE Foundation.Actors_Movies(
    ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	MovieID INT NOT NULL,
	ActorID INT NOT NULL
);

--Fill in the required data making sure of the following conditions:
--It should have a movie with more than 2 actors in it.
--There must be two actors who have worked together in two movies or more.
--There must be a producer who has produced more than 3 movies.

INSERT INTO Foundation.Producers (Name, Sex, Dob, Bio) 
VALUES 
('Kevin Feige', 'Male','1973-12-06', 'Kevin Feige is american producer'), 
('Gwyneth Paltrow', 'Female', '1990-11-07', 'American actress and businesswoman'), 
('Samuel L. Jackson', 'Male', '1988-09-21', 'Samuel Leroy Jackson is an American actor and producer'), 
('Robert Downey Jr.', 'Male', '1998-12-02', 'RDJ is an American actor and producer'), 
('Jon Favreau', 'Male', '1978-12-01', 'JKF is an American producer and filmmaker');

INSERT INTO Foundation.Actors (Name, Sex, Dob, Bio)
VALUES
    ('Robert Downey Jr.', 'Male', '1965-04-04', 'Robert Downey Jr. is an American actor and producer'),
    ('Chris Evans', 'Male', '1981-06-13', 'Chris Evans is an American actor.'),
    ('Scarlett Johansson', 'Female', '1984-11-22', 'Scarlett Johansson is an American actress and singer.'),
    ('Mark Ruffalo', 'Male', '1967-11-22', 'Mark Ruffalo is an American actor and producer.'),
    ('Chris Hemsworth', 'Male', '1983-08-11', 'Chris Hemsworth is an Australian actor.');

INSERT INTO Foundation.Movies (Name, YearOfRelease, Plot, Poster, ProducerID)
VALUES 
('The Avengers', 2012, 'Action, Adventure, Sci-Fi', 'URL' , 1),
('Captain America: The First Avenger', 2011, 'Action, Adventure, Sci-Fi', 'URL',2),
('Iron Man 2', 2010, 'Action, Adventure, Sci-Fi', 'URL', 3),
('The Incredible Hulk', 2008, 'Action, Adventure, Sci-Fi', 'URL',4),
('Thor', 2011, 'Action, Adventure, Fantasy', 'URL', 1),
('Jumanji: Welcome to the Jungle', 2017, 'Action, Adventure', 'URL', 1);

INSERT INTO Foundation.Actors_Movies (MovieID, ActorID)
VALUES
	(1, 1),
	(1, 2),
	(1, 3),
	(2, 1),
	(2, 4),
	(2, 5);

--Add two new columns called CreatedAt and UpdatedAt using the alter Table command.
--Create a default constraint for CreatedAt to store the current Date.

ALTER TABLE Foundation.Movies
ADD CreatedAt DATE NOT NULL CONSTRAINT DF_CreatedAt DEFAULT GETDATE(),
    UpdatedAt DATE;


-- Already add Primary key
-- Here is how we can add primary key
/*
ALTER TABLE Foundation.Producers
ADD CONSTRAINT PK_Producers PRIMARY KEY (ID);

ALTER TABLE Foundation.Movies
ADD CONSTRAINT PK_Movies PRIMARY KEY (ID);

ALTER TABLE Foundation.Actors
ADD CONSTRAINT PK_Actors PRIMARY KEY (ID);

ALTER TABLE Foundation.Actors_Movies
ADD CONSTRAINT PK_Actors_Movies PRIMARY KEY (ID);
*/

--ADD foreign keys as required.
ALTER TABLE Foundation.Movies
ADD CONSTRAINT FK_Movies_Producers FOREIGN KEY (ProducerID) REFERENCES Foundation.Producers(ID);

ALTER TABLE Foundation.Actors_Movies
ADD 
CONSTRAINT FK_ActorsMovies_Movies FOREIGN KEY (MovieID) REFERENCES Foundation.Movies(ID),
CONSTRAINT FK_ActorsMovies_Actors FOREIGN KEY (ActorID) REFERENCES Foundation.Actors(ID);


--Alter the table to add a language(varchar) and a profit(Int) column to the movies table.
ALTER TABLE Foundation.Movies
ADD Language VARCHAR(50),
    Profit INT;
