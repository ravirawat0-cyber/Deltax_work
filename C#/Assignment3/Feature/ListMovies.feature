Feature: ListMovies
	As a user
	I want to be able to see a list of all movies

@mytag
Scenario: User views a list of all movies
	Given the user wants to view a list of all movies
	When the user selects the "List Movies" option
	Then a list of all movies with their actors and producers is displayed