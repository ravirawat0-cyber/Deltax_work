--IMDB Database Creation:

--Create a new database called IMDB.
CREATE DATABASE IMDB;
USE IMDB;

--Create a new schema called Foundation.
CREATE SCHEMA Foundation;

--Use the schema to create the tables in the design created in Assignment 1.
CREATE TABLE Foundation.Producers(
   ID INT NOT NULL PRIMARY KEY,
   Name VARCHAR(255) NOT NULL,
   Sex VARCHAR(255) NOT NULL,
   Dob DATE NOT NULL,
   Bio VARCHAR(255) NOT NULL
);

CREATE TABLE Foundation.Actors(
   ID INT NOT NULL PRIMARY KEY,
   Name VARCHAR(255) NOT NULL,
   Sex VARCHAR(255) NOT NULL,
   Dob DATE NOT NULL,
   Bio VARCHAR(255) NOT NULL
);

CREATE TABLE Foundation.Movies(
  ID INT NOT NULL PRIMARY KEY,
  Name VARCHAR(255) NOT NULL,
  ReleaseYear INT NOT NULL,
  Plot VARCHAR(255) NOT NULL,
  Poster VARCHAR(255) NOT NULL,
  ProducerID INT NOT NULL,
  FOREIGN KEY (ProducerID) REFERENCES Foundation.Producers(ID)
);

CREATE TABLE Foundation.ActorsMovies(
	ID INT NOT NULL PRIMARY KEY,
	MovieID INT NOT NULL,
	ActorID INT NOT NULL,
	FOREIGN KEY (MovieID) REFERENCES FOUNDATION.Movies(ID),
	FOREIGN KEY (ActorID) REFERENCES FOUNDATION.Actors(ID)
);

--Fill in the required data making sure of the following conditions:
--It should have a movie with more than 2 actors in it.
--There must be two actors who have worked together in two movies or more.
--There must be a producer who has produced more than 3 movies.

INSERT INTO Foundation.Producers (ID, Name, Sex, Dob, Bio) 
VALUES 
(1, 'Kevin Feige', 'Male','1973-12-06', 'Kevin Feige is american producer'), 
(2, 'Gwyneth Paltrow', 'Female', '1990-11-07', 'American actress and businesswoman'), 
(3, 'Samuel L. Jackson', 'Male', '1988-09-21', 'Samuel Leroy Jackson is an American actor and producer'), 
(4, 'Robert Downey Jr.', 'Male', '1998-12-02', 'RDJ is an American actor and producer'), 
(5, 'Jon Favreau', 'Male', '1978-12-01', 'JKF is an American producer and filmmaker');

INSERT INTO Foundation.Actors (ID, Name, Sex, Dob, Bio)
VALUES
    (1, 'Robert Downey Jr.', 'Male', '1965-04-04', 'Robert Downey Jr. is an American actor and producer'),
    (2, 'Chris Evans', 'Male', '1981-06-13', 'Chris Evans is an American actor.'),
    (3, 'Scarlett Johansson', 'Female', '1984-11-22', 'Scarlett Johansson is an American actress and singer.'),
    (4, 'Mark Ruffalo', 'Male', '1967-11-22', 'Mark Ruffalo is an American actor and producer.'),
    (5, 'Chris Hemsworth', 'Male', '1983-08-11', 'Chris Hemsworth is an Australian actor.');

INSERT INTO Foundation.Movies (ID, Name, ReleaseYear, Plot, Poster, ProducerID)
VALUES 
(1, 'The Avengers', 2012, 'Action, Adventure, Sci-Fi', 'URL' , 1),
(2, 'Captain America: The First Avenger', 2011, 'Action, Adventure, Sci-Fi', 'URL',2),
(3, 'Iron Man 2', 2010, 'Action, Adventure, Sci-Fi', 'URL', 3),
(4, 'The Incredible Hulk', 2008, 'Action, Adventure, Sci-Fi', 'URL',4),
(5, 'Thor', 2011, 'Action, Adventure, Fantasy', 'URL', 1),
(6, 'Jumanji: Welcome to the Jungle', 2017, 'Action, Adventure', 'URL', 1);

INSERT INTO Foundation.ActorsMovies (ID, MovieID, ActorID)
VALUES
	(1, 1, 1),
	(2, 1, 2),
	(3, 1, 3),
	(4, 2, 1),
	(5, 2, 4),
	(6, 2, 5);

--Add two new columns called CreatedAt and UpdatedAt using the alter Table command.
--Create a default constraint for CreatedAt to store the current Date.
ALTER TABLE Foundation.Movies
ADD CreatedAt DATE NOT NULL CONSTRAINT DF_CreatedAt DEFAULT GETDATE(),
    UpdatedAt DATE NOT NULL CONSTRAINT DF_UpdatedAt DEFAULT GETDATE();

--Alter the table to add a language(varchar) and a profit(Int) column to the movies table.
ALTER TABLE Foundation.Movies
ADD Language VARCHAR(50) NOT NULL DEFAULT 'English',
    Profit INT NOT NULL DEFAULT 0;
