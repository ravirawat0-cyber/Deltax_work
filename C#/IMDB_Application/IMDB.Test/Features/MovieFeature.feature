Feature: 

@AddMovies
Scenario: Add a movie
    When I request to add a movie with details: Name: '<Name>', YearOfRelease: '<YearOfRelease>', Plot '<Plot>', Actors: '<ActorIds>', Producer: '<ProducerId>'
	Then the response message should be sent <responseMessage>

	@ValidCase
	Examples: 
	| Name    | YearOfRelease | Plot       | ActorIds | ProducerId | responseMessage          |
	| Iron Man | 2009          | Iron suit | 1,2      | 1          | Movie added successfully |
	
	@InvalidCase
	Examples: 
	| Name     | YearOfRelease | Plot      | ActorIds | ProducerId | responseMessage                               |
	|          | 2009          | Iron suit | 1,2      | 1          | Name of movie cannot be empty.                |
	| Iron Man | abc           | Iron suit | 1,2      | 1          | Year of release should be a positive integer. |
	| Iron Man | 2009          |           | 1,2      | 1          | Plot of the movie cannot be empty.            |
	| Iron Man | 2009          | Iron suit |          | 1          | Actor IDs cannot be empty.                    |
	| Iron Man | 2009          | Iron suit | 1,2      |            | Producer ID cannot be empty.                  |
	| Iron Man | 2009          | Iron suit | a        | 1          | Enter valid actors Id                         |
	| Iron Man | 2009          | Iron suit | 1,2,3    | 1,2        | You can only choose 1 producer from the list. |
				
@ListMovies
Scenario: List the details of all the movies
	When the user requests to list the movies
	Then the response data should be
	| Id | Name     | YearOfRelease | Plot              | Actors                                                                                                                                          | Producer                                                           |
	| 1  | Iron Man | 2009          | Iron suit         | [{"Id":1,"Name":"Robert Downey Jr.","DateOfBirth":"1990-03-15T00:00:00"},{"Id":2,"Name":"Chris Hemsworth","DateOfBirth":"1983-08-11T00:00:00"}] | {"Id":1,"Name":"Kevin Feige","DateOfBirth":"1973-06-02T00:00:00"}  |
	| 2  | Batman   | 2005          | Dark knight rises | [{"Id":3,"Name":"Ryan Gosling","DateOfBirth":"1990-06-12T00:00:00"}]                                                                            | {"Id":2,"Name":"Mark Johnson","DateOfBirth":"2009-09-12T00:00:00"} |

@DeleteMovie
Scenario: User delete a movie
	When The user want to delete movie with id: '<Id>'
	Then the response message should be sent <responseMessage>

	@ValidCase
	Examples:
	| Id | responseMessage            |
	| 1  | Movie deleted successfully |

	@InvalidCase
	Examples:
	| Id | responseMessage   |
	| 3  | Invalid Movie ID |
	| 55 | Invalid Movie ID |

