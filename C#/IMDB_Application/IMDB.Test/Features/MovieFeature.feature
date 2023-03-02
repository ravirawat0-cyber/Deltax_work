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
	And The movie list look like this '[{"Actors":[{"Id":1,"Name":"Robert Downey Jr.","DateOfBirth":"1990-03-15T00:00:00"}],"Id":1,"Name":"IronMan","YearOfRelease":2009,"Plot":"Iron suit","Producer":{"Id":1,"Name":"Kevin Feige","DateOfBirth":"1973-06-02T00:00:00"}}]'

@ListMovie
Scenario: User views a list of all movies
	Given the user selects option 2
	Then fetch all the movie details
	And all movie detail look like this '[{"Actors":[{"Id":1,"Name":"Robert Downey Jr.","DateOfBirth":"1990-03-15T00:00:00"}],"Id":1,"Name":"IronMan","YearOfRelease":2009,"Plot":"Iron suit","Producer":{"Id":1,"Name":"Kevin Feige","DateOfBirth":"1973-06-02T00:00:00"}}]'


@DeleteMovie
Scenario: User delete a movie
	Given The user chose option 5 from the available options
	And The list of movie name with id shown:
		| Id | Name    |
		| 1  | IronMan |
		| 2  | Batman  |
	When The user delete the movie through Id from the list 1
	Then movie list look like '[{"Actors":[{"Id":2,"Name":"Chris Hemsworth","DateOfBirth":"1983-08-11T00:00:00"},{"Id":3,"Name":"Will Smith","DateOfBirth":"1968-09-25T00:00:00"}],"Id":2,"Name":"Batman","YearOfRelease":2005,"Plot":"Dark knight rises","Producer":{"Id":1,"Name":"Kevin Feige","DateOfBirth":"1973-06-02T00:00:00"}}]'



@InvalidCaseAddMovie
Scenario: User adds a new movie with invalid data
	Given The user select options: 1 
	When User Request to add movie with following invalid details:
		| Name    | YearOfRelease | Plot       | ActorIds | ProducerId |
	    |         | 2009          | Iron suit  | 1,2        |   4       |
	Then Movie with following detail add to the list
	And The response message should be 'Name of movie cannot be empty.'

@InvalidCaseDeleteMovie
Scenario: User want to delete a movie with invalid data
	Given The user choose option 5 from the available options
	And the list of movie name with ID shown
		| Id | Name    |
		| 1  | IronMan |
		| 2  | Batman  |
	When The user want to delete movie with id 3
	Then Then the response should be 'Invalid Id'




