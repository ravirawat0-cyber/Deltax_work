Feature: Add Movie
	As a user
	I want to be able to add a new movie

@mytag
Scenario: User adds a new movie
	Given the user wants to add a new movie
	When the user select "Add movie" option
	And enters movie details (name, year of release, plot)
	And selects actors from the list
	And selects a producer from the list
	Then the movie is added to the movie list

