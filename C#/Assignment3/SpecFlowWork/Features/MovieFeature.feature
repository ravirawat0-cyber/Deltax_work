Feature: 
	As a user
	I want to add movie and list movie

@AddMovie
Scenario: User adds a new movie
	Given The user select Add Movie
	When  User request to add movie with following details:
	| Name    | YearOfRealease | Plot           | Actors | Producer |
	| IronMan | 2009 | Iron suit      | 1        | 1          |
	Then Movie add to the list
	Then The movie list look like this '[{"Actors":[{"Name":"Robert Downey Jr.","Id":1}],"Name":"IronMan","YearOfRealease":2009,"Plot":"Iron suit","Producer":{"Name":"Kevin Feige","Id":1}}]'
	

@ListMovie
Scenario: User views a list of all movies
	Given the user select List Movie
	Then fetch all the movie details
	Then all movie detail look like this '[{"Actors":[{"Name":"Robert Downey Jr.","Id":1}],"Name":"IronMan","YearOfRealease":2009,"Plot":"Iron suit","Producer":{"Name":"Kevin Feige","Id":1}}]'

