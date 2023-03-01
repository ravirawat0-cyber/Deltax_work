Feature: 
	As a user
	I want to add movie and list movie

@AddMovie
Scenario: User adds a new movie
	Given The user select option 1
	When  User request to add movie with following details:
	| Name    | YearOfRelease | Plot       | ActorIds | ProducerId |
	| IronMan | 2009          | Iron suit  | 1        | 1          |
	Then Movie add to the list
	Then The movie list look like this '[{"Actors":[{"Id":1,"Name":"Robert Downey Jr.","DateOfBirth":"1990-03-15T00:00:00"}],"Id":1,"Name":"IronMan","YearOfRelease":2009,"Plot":"Iron suit","Producer":{"Id":1,"Name":"Kevin Feige","DateOfBirth":"1973-06-02T00:00:00"}}]'


@ListMovie
Scenario: User views a list of all movies
	Given the user selects option 2
	Then fetch all the movie details
	And all movie detail look like this '[{"Actors":[{"Id":1,"Name":"Robert Downey Jr.","DateOfBirth":"1990-03-15T00:00:00"}],"Id":1,"Name":"IronMan","YearOfRelease":2009,"Plot":"Iron suit","Producer":{"Id":1,"Name":"Kevin Feige","DateOfBirth":"1973-06-02T00:00:00"}}]'


@DeleteMovie
Scenario: User delete a movie
	Given The user chose option 5 from the available options
	When The user delete the movie through Id from the list 1
	Then movie list look like '[{"Actors":[{"Id":2,"Name":"Chris Hemsworth","DateOfBirth":"1983-08-11T00:00:00"},{"Id":3,"Name":"Will Smith","DateOfBirth":"1968-09-25T00:00:00"}],"Id":2,"Name":"Batman","YearOfRelease":2005,"Plot":"Dark knight rises","Producer":{"Id":1,"Name":"Kevin Feige","DateOfBirth":"1973-06-02T00:00:00"}}]'