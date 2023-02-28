Feature: 
	As a user
	I want to add movie and list movie

@AddMovie
Scenario: User adds a new movie
	Given The user select Add Movie
	When  User request to add movie with following details:
	| Name    | YearOfRelease | Plot       | ActorIds | ProducerId |
	| IronMan | 2009          | Iron suit  | 1        | 1          |
	| Batman  | 2005          | black suit | 1,2      | 2          |
	Then The movie list look like this '[{"Actors":[{"Id":1,"Name":"Robert Downey Jr.","DateOfBirth":"1990-03-15T00:00:00"}],"Id":1,"Name":"IronMan","YearOfRelease":2009,"Plot":"Iron suit","Producer":{"Id":1,"Name":"Kevin Feige","DateOfBirth":"1973-06-02T00:00:00"}},{"Actors":[{"Id":1,"Name":"Robert Downey Jr.","DateOfBirth":"1990-03-15T00:00:00"},{"Id":2,"Name":"Chris Hemsworth","DateOfBirth":"1983-08-11T00:00:00"}],"Id":2,"Name":"Batman","YearOfRelease":2005,"Plot":"black suit","Producer":{"Id":2,"Name":"Taika waititi","DateOfBirth":"1975-08-16T00:00:00"}}]'


@ListMovie
Scenario: User views a list of all movies
	Given the user select List Movie
	Then fetch all the movie details
	Then all movie detail look like this '[{"Actors":[{"Id":1,"Name":"Robert Downey Jr.","DateOfBirth":"1990-03-15T00:00:00"}],"Id":1,"Name":"IronMan","YearOfRelease":2009,"Plot":"Iron suit","Producer":{"Id":1,"Name":"Kevin Feige","DateOfBirth":"1973-06-02T00:00:00"}},{"Actors":[{"Id":1,"Name":"Robert Downey Jr.","DateOfBirth":"1990-03-15T00:00:00"},{"Id":2,"Name":"Chris Hemsworth","DateOfBirth":"1983-08-11T00:00:00"}],"Id":2,"Name":"Batman","YearOfRelease":2005,"Plot":"Black suit","Producer":{"Id":1,"Name":"Kevin Feige","DateOfBirth":"1973-06-02T00:00:00"}}]'