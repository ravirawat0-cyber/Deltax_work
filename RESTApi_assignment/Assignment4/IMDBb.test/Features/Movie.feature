Feature:Movie Resource

Scenario: Get Movie All
	Given I am a client
	When I make GET Request '<URL>'
	Then response code must be '<ResponseCode>'
	And response data must look like '<ResponseData>'
	Examples: 
	| URL        | ResponseCode | ResponseData                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        |
	| api/movies | 200          | [{"id":1,"name":"Mock Movie","yearOfRelease":2017,"plot":"Mock Plot","producer":{"id":1,"name":"Mock Producer","bio":"Mock bio","dob":"1990-12-12T00:00:00","sex":"M"},"coverImageUrl":"firebase URL","actors":[{"id":1,"name":"Mock Actor","bio":"Mock bio","dob":"1990-12-12T00:00:00","sex":"M"},{"id":2,"name":"Mock Actor","bio":"Mock bio","dob":"1990-12-12T00:00:00","sex":"M"}],"genres":[{"id":1,"name":"Mock Comedy"},{"id":2,"name":"Mock Action"}]},{"id":2,"name":"Mock Movie 2","yearOfRelease":2017,"plot":"Mock Plot","producer":{"id":2,"name":"Mock Producer","bio":"Mock bio","dob":"1990-12-12T00:00:00","sex":"M"},"coverImageUrl":"firebase URL","actors":[{"id":1,"name":"Mock Actor","bio":"Mock bio","dob":"1990-12-12T00:00:00","sex":"M"},{"id":2,"name":"Mock Actor","bio":"Mock bio","dob":"1990-12-12T00:00:00","sex":"M"}],"genres":[{"id":1,"name":"Mock Comedy"},{"id":2,"name":"Mock Action"}]}] |


Scenario: Get Movie by ID
	Given I am a client
    When I make GET Request '<URL>'
	Then response code must be '<ResponseCode>'
	And response data must look like '<ResponseData>'

	@ValidCase
	Examples:
	| URL          | ResponseCode | ResponseData                                                                                                                                                                                                                                                                                                                                                                                                                                                      |
	| api/movies/1 | 200          | {"id":1,"name":"Mock Movie","yearOfRelease":2017,"plot":"Mock Plot","producer":{"id":1,"name":"Mock Producer","bio":"Mock bio","dob":"1990-12-12T00:00:00","sex":"M"},"coverImageUrl":"firebase URL","actors":[{"id":1,"name":"Mock Actor","bio":"Mock bio","dob":"1990-12-12T00:00:00","sex":"M"},{"id":2,"name":"Mock Actor","bio":"Mock bio","dob":"1990-12-12T00:00:00","sex":"M"}],"genres":[{"id":1,"name":"Mock Comedy"},{"id":2,"name":"Mock Action"}]}   |
	| api/movies/2 | 200          | {"id":2,"name":"Mock Movie 2","yearOfRelease":2017,"plot":"Mock Plot","producer":{"id":2,"name":"Mock Producer","bio":"Mock bio","dob":"1990-12-12T00:00:00","sex":"M"},"coverImageUrl":"firebase URL","actors":[{"id":1,"name":"Mock Actor","bio":"Mock bio","dob":"1990-12-12T00:00:00","sex":"M"},{"id":2,"name":"Mock Actor","bio":"Mock bio","dob":"1990-12-12T00:00:00","sex":"M"}],"genres":[{"id":1,"name":"Mock Comedy"},{"id":2,"name":"Mock Action"}]} |

	@InvalidCase
	Examples:
	| URL           | ResponseCode | ResponseData               |
	| api/movies/10 | 404          | Movie with ID 10 not found |
	| api/movies/20 | 404          | Movie with ID 20 not found |


Scenario: Create Movie
	Given I am a client
	When  I am make a POST request to '<URL>' with the following Data '<RequestData>'
	Then response code must be '<ResponseCode>'
	And response data must look like '<ResponseData>'

	@ValidCase
	Examples:
	| URL        | RequestData                                                                                                                                                                                                                                                                                             | ResponseCode | ResponseData |
	| api/movies | {"Name":"Mock Movie 3","YearOfRelease":2017,"Plot":"Mock Plot","ProducerId":1,"ActorIds":"2,1","GenreIds" : "1,2","CoverImageUrl":"https://firebasestorage.googleapis.com/v0/b/imdb-88f70.appspot.com/o/7d12593c-9d19-4be3-82e8-e05f82838162.jpg?alt=media&token=61e071f3-4a8f-4959-b7b8-aa65bbeee0dd"} | 200          | 3            |
	| api/movies | {"Name":"Mock Movie","YearOfRelease":2020,"Plot":"Mock Plot","ProducerId":2,"ActorIds":"1","GenreIds" : "1,2","CoverImageUrl":"https://firebasestorage.googleapis.com/v0/b/imdb-88f70.appspot.com/o/7d12593c-9d19-4be3-82e8-e05f82838162.jpg?alt=media&token=61e071f3-4a8f-4959-b7b8-aa65bbeee0dd"}     | 200          | 3            |

	@InvalidCase
	Examples:
	| URL        | RequestData                                                                                                                                                                 | ResponseCode | ResponseData                            |
	| api/movies | {"Name":"","YearOfRelease":2017,"Plot":"Mock Plot","ProducerId":1,"ActorIds":"2,1","GenreIds":"1,2","CoverImageUrl":"https://firebasestorage.googleapis.com"}               | 400          | Movie name cannot be null or empty      |
	| api/movies | {"Name":"Mock Movie","YearOfRelease":2017,"Plot":"","ProducerId":1,"ActorIds":"2,1","GenreIds" : "1,2","CoverImageUrl":"https://firebasestorage.googleapis.com"}            | 400          | Movie plot cannot be null or empty      |
	| api/movies | {"Name":"Mock Movie","YearOfRelease":2017,"Plot":"Mock plot","ProducerId":5,"ActorIds":"2,1","GenreIds" : "1,2","CoverImageUrl":"https://firebasestorage.googleapis.com"}   | 404          | Producer with ID 5 not found            |
	| api/movies | {"Name":"Mock Movie","YearOfRelease":2017,"Plot":"Mock plot","ProducerId":1,"ActorIds":"1,5,7","GenreIds" : "1,2","CoverImageUrl":"https://firebasestorage.googleapis.com"} | 404          | No Actor present with given Ids 5,7     |
	| api/movies | {"Name":"Mock Movie","YearOfRelease":2017,"Plot":"Mock plot","ProducerId":1,"ActorIds":"1,2","GenreIds" : "6,2","CoverImageUrl":"https://firebasestorage.googleapis.com"}   | 404          | No Genre present with given Ids 6       |
	| api/movies | {"Name":"Mock Movie","YearOfRelease":2017,"Plot":"Mock plot","ProducerId":1,"ActorIds":"1,2","GenreIds" : "6,2,9","CoverImageUrl":"https://firebasestorage.googleapis.com"} | 404          | No Genre present with given Ids 6,9     |
	| api/movies | {"Name":"Mock Movie","YearOfRelease":2017,"Plot":"Mock plot","ProducerId":1,"ActorIds":"1,2","GenreIds" : "1,2","CoverImageUrl":""  }                                       | 400          | Cover image URL cannot be null or empty |
	| api/movies | {"Name":"Mock Movie","YearOfRelease":0,"Plot":"Mock plot","ProducerId":1,"ActorIds":"1,2","GenreIds":"1,2","CoverImageUrl":"https://firebasestorage.googleapis.com"  }      | 400          | Invalid year of release                 |

Scenario: Update Movie
	Given I am a client
	When  I make PUT Request '<URL>' with the following Data '<RequestData>'
	Then response code must be '<ResponseCode>'
	And response data must look like '<ResponseData>'

@ValidCase
	Examples:
	| URL          | RequestData                                                                                                                                                                                                                                                                                                           | ResponseCode | ResponseData |
	| api/movies/1 | {"Name":"Mock Movie Updated","YearOfRelease":2017,"Plot":"Mock Plot","ProducerId":1,"ActorIds":"2,1","GenreIds" : "1,2","CoverImageUrl":"https://firebasestorage.googleapis.com/v0/b/imdb-88f70.appspot.com/o/7d12593c-9d19-4be3-82e8-e05f82838162.jpg?alt=media&token=61e071f3-4a8f-4959-b7b8-aa65bbeee0dd"}         | 200          |              |
	| api/movies/1 | {"Name":"Mock Movie","YearOfRelease":2019,"Plot":"Mock Plot","ProducerId":1,"ActorIds":"2,1","GenreIds" : "1,2","CoverImageUrl":"https://firebasestorage.googleapis.com/v0/b/imdb-88f70.appspot.com/o/7d12593c-9d19-4be3-82e8-e05f82838162.jpg?alt=media&token=61e071f3-4a8f-4959-b7b8-aa65bbeee0dd"}                 | 200          |              |
	| api/movies/1 | {"Name":"Mock Movie Updated","YearOfRelease":2017,"Plot":"Updated Mock Plot","ProducerId":1,"ActorIds":"2,1","GenreIds" : "1,2","CoverImageUrl":"https://firebasestorage.googleapis.com/v0/b/imdb-88f70.appspot.com/o/7d12593c-9d19-4be3-82e8-e05f82838162.jpg?alt=media&token=61e071f3-4a8f-4959-b7b8-aa65bbeee0dd"} | 200          |              |
	| api/movies/1 | {"Name":"Mock Movie Updated","YearOfRelease":2017,"Plot":"Mock Plot","ProducerId":2,"ActorIds":"2,1","GenreIds" : "1,2","CoverImageUrl":"https://firebasestorage.googleapis.com/v0/b/imdb-88f70.appspot.com/o/7d12593c-9d19-4be3-82e8-e05f82838162.jpg?alt=media&token=61e071f3-4a8f-4959-b7b8-aa65bbeee0dd"}         | 200          |              |
	| api/movies/1 | {"Name":"Mock Movie Updated","YearOfRelease":2017,"Plot":"Mock Plot","ProducerId":1,"ActorIds":"1,2","GenreIds" : "1,2","CoverImageUrl":"https://firebasestorage.googleapis.com/v0/b/imdb-88f70.appspot.com/o/7d12593c-9d19-4be3-82e8-e05f82838162.jpg?alt=media&token=61e071f3-4a8f-4959-b7b8-aa65bbeee0dd"}         | 200          |              |
	| api/movies/1 | {"Name":"Mock Movie Updated","YearOfRelease":2017,"Plot":"Mock Plot","ProducerId":1,"ActorIds":"1,2","GenreIds" : "2,1","CoverImageUrl":"https://firebasestorage.googleapis.com/v0/b/imdb-88f70.appspot.com/o/7d12593c-9d19-4be3-82e8-e05f82838162.jpg?alt=media&token=61e071f3-4a8f-4959-b7b8-aa65bbeee0dd"}         | 200          |              |
	| api/movies/1 | {"Name":"Mock Movie Updated","YearOfRelease":2017,"Plot":"Mock Plot","ProducerId":1,"ActorIds":"2,1","GenreIds" : "1,2","CoverImageUrl":"Updated/https://firebasestorage.googleapis.com/v0/b/imdb-88f70.appspot.com/o/7d12593c-9d19-4be3-82e8-e05f82838162.jpg?alt=media&token=61e071f3-4a8f-4959-b7b8-aa65bbeee0dd"} | 200          |              |

	@InvalidCase
	Examples:
	| URL          | RequestData                                                                                                                                                                       | ResponseCode | ResponseData                            |
	| api/movies/1 | {"Name":"","YearOfRelease":2017,"Plot":"Mock Plot","ProducerId":1,"ActorIds":"2,1","GenreIds":"1,2","CoverImageUrl":"https://firebasestorage.googleapis.com"}                     | 400          | Movie name cannot be null or empty      |
	| api/movies/1 | {"Name":"Mock Movie","YearOfRelease":2017,"Plot":"","ProducerId":1,"ActorIds":"2,1","GenreIds" : "1,2","CoverImageUrl":"https://firebasestorage.googleapis.com"}                  | 400          | Movie plot cannot be null or empty      |
	| api/movies/1 | {"Name":"Mock Movie","YearOfRelease":2017,"Plot":"Mock plot","ProducerId":5,"ActorIds":"2,1","GenreIds" : "1,2","CoverImageUrl":"https://firebasestorage.googleapis.com"}         | 404          | Producer with ID 5 not found            |
	| api/movies/1 | {"Name":"Mock Movie","YearOfRelease":2017,"Plot":"Mock plot","ProducerId":1,"ActorIds":"1,5,7","GenreIds" : "1,2","CoverImageUrl":"https://firebasestorage.googleapis.com"}       | 404          | No Actor present with given Ids 5,7     |
	| api/movies/1 | {"Name":"Mock Movie","YearOfRelease":2017,"Plot":"Mock plot","ProducerId":1,"ActorIds":"1,2","GenreIds" : "6,2","CoverImageUrl":"https://firebasestorage.googleapis.com"}         | 404          | No Genre present with given Ids 6       |
	| api/movies/1 | {"Name":"Mock Movie","YearOfRelease":2017,"Plot":"Mock plot","ProducerId":1,"ActorIds":"1,2","GenreIds" : "6,2,9","CoverImageUrl":"https://firebasestorage.googleapis.com"}       | 404          | No Genre present with given Ids 6,9     |
	| api/movies/1 | {"Name":"Mock Movie","YearOfRelease":2017,"Plot":"Mock plot","ProducerId":1,"ActorIds":"1,2","GenreIds" : "1,2","CoverImageUrl":""  }                                             | 400          | Cover image URL cannot be null or empty |
	| api/movies/1 | {"Name":"Mock Movie","YearOfRelease":0,"Plot":"Mock plot","ProducerId":1,"ActorIds":"1,2","GenreIds":"1,2","CoverImageUrl":"https://firebasestorage.googleapis.com"  }            | 400          | Invalid year of release                 |
	| api/movies/5 | {"Name":"Mock Movie Updated","YearOfRelease":2017,"Plot":"Mock Plot","ProducerId":1,"ActorIds":"2,1","GenreIds" : "1,2","CoverImageUrl":"https://firebasestorage.googleapis.com"} | 404          | There is no Movie to Update with Id 5   |


Scenario: Delete Movie
	Given I am a client
	When I make Delete Request '<URL>'
	Then response code must be '<ResponseCode>'
	And response data must look like '<ResponseData>'

	@ValidCase
	Examples:
	| URL          | ResponseCode | ResponseData |
	| api/movies/1 | 200          |              |
	| api/movies/2 | 200          |              |

	@InValidCase
	Examples:
	| URL           | ResponseCode | ResponseData               |
	| api/movies/20 | 404          | Movie with ID 20 not found |
	| api/movies/23 | 404          | Movie with ID 23 not found |
