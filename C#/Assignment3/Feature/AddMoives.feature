Feature: Add Movie
	As a user
	I want to be able to add a new movie

@mytag
Scenario: User adds a new movie
	Given The user select "Add movie" option
	And Take input based on the option ex:1
	Then enters movie details (name, year of release, plot):
	 | Name            | YearOfRelease | Plot                  |
	 | The Dark Knight | 2008          | A criminal mastermind |
	Then show the list of actors from the list:
	 | Id | Name               |
	 | 1  | Robert Downey Jr.  |
	 | 2  | Mark Ruffalo       |
	 | 3  | Chris Evans        |
	 | 4  | Chirs Hemsworth    |
	 | 5  | Scarlett Johansson |
	 | 6  | Tom Holland        |
	And Take list of actors as input:
	| Actors  |
	| <Actor> |
	Then Show the list of producer from the list:
	 | Id | Name               |
	 | 1  | Avi Arad           |
	 | 2  | Larry Franco       |
	 | 3  | Kevin Feige        |
	 | 4  | Anthony Hopkins    |
	 | 5  | Ryan Coogler       |
	 | 6  | Ian Bryce          |
	And Take producer as input:
	| Producer |
	|<Producer>|
	Then the movie is added to the movie list with following details:
	| Name            | YearOfRelease | Plot                   | Actors                      | Producer            |
    | The Dark Knight | 2008          | A criminal mastermind  | Heath Ledger, Christian Bale| Christopher Nolan   |
	And Verify if added successfully

