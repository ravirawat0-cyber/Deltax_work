Feature: 


@AddMovie
Scenario: User adds a new movie
	Given User provide following details:
	| Name    | YearOfRelease | Plot       | ActorIds | ProducerId |
	| IronMan | 2009          | Iron suit  | 1        | 1          |
	When The Movie is added to the list
	Then The movie list look like this '[{"Actors":[{"Id":1,"Name":"Robert Downey Jr.","DateOfBirth":"1990-03-15T00:00:00"}],"Id":1,"Name":"IronMan","YearOfRelease":2009,"Plot":"Iron suit","Producer":{"Id":1,"Name":"Kevin Feige","DateOfBirth":"1973-06-02T00:00:00"}},{"Actors":[{"Id":2,"Name":"Chris Hemsworth","DateOfBirth":"1983-08-11T00:00:00"}],"Id":2,"Name":"Batman","YearOfRelease":2005,"Plot":"Dark knight rises","Producer":{"Id":1,"Name":"Kevin Feige","DateOfBirth":"1973-06-02T00:00:00"}},{"Actors":[{"Id":1,"Name":"Robert Downey Jr.","DateOfBirth":"1990-03-15T00:00:00"}],"Id":3,"Name":"IronMan","YearOfRelease":2009,"Plot":"Iron suit","Producer":{"Id":1,"Name":"Kevin Feige","DateOfBirth":"1973-06-02T00:00:00"}}]'

@ListMovie
Scenario: User views a list of all movies
	Given fetch all the movie details
	Then  The movie list look like this '[{"Actors":[{"Id":1,"Name":"Robert Downey Jr.","DateOfBirth":"1990-03-15T00:00:00"}],"Id":1,"Name":"IronMan","YearOfRelease":2009,"Plot":"Iron suit","Producer":{"Id":1,"Name":"Kevin Feige","DateOfBirth":"1973-06-02T00:00:00"}},{"Actors":[{"Id":2,"Name":"Chris Hemsworth","DateOfBirth":"1983-08-11T00:00:00"}],"Id":2,"Name":"Batman","YearOfRelease":2005,"Plot":"Dark knight rises","Producer":{"Id":1,"Name":"Kevin Feige","DateOfBirth":"1973-06-02T00:00:00"}}]'


@DeleteMovie
Scenario: User delete a movie
	Given The user want to delete movie with id 1
	Then The movie list look like this '[{"Actors":[{"Id":2,"Name":"Chris Hemsworth","DateOfBirth":"1983-08-11T00:00:00"}],"Id":2,"Name":"Batman","YearOfRelease":2005,"Plot":"Dark knight rises","Producer":{"Id":1,"Name":"Kevin Feige","DateOfBirth":"1973-06-02T00:00:00"}}]'


@InvalidCaseAddMovie
Scenario: User adds a new movie with invalid data
	Given User provide following details:
		| Name    | YearOfRelease | Plot       | ActorIds | ProducerId |
	    |         | 2009          | Iron suit  | 1,2     |   1       |
	When The Movie is added to the list
	Then The response message should be 'Name of movie cannot be empty.'

@InvalidCaseDeleteMovie
Scenario: User want to delete a movie with invalid data
	Given The user want to delete movie with id 3
	Then The response message should be 'Invalid Movie ID'




