Feature: Genre Resource 

Scenario: Get Genre All
	Given I am a client
	When I make GET Request '<URL>'
	Then response code must be '<ResponseCode>'
	And response data must look like '<ResponseData>'
	Examples: 
	| URL        | ResponseCode | ResponseData                                                  |
	| api/genres | 200          | [{"id":1,"name":"Mock Comedy"},{"id":2,"name":"Mock Action"}] |

@GetById
Scenario: Get Genre by ID
	Given I am a client
    When I make GET Request '<URL>'
	Then response code must be '<ResponseCode>'
	And response data must look like '<ResponseData>'

	@ValidCase
	Examples:
	| URL          | ResponseCode | ResponseData                  |
	| api/genres/1 | 200          | {"id":1,"name":"Mock Comedy"} |
	
	@InvalidCase
	Examples:
	| URL           | ResponseCode | ResponseData               |
	| api/genres/20 | 404          | Genre with ID 20 not found |

@Create	
Scenario: Create Genre
	Given I am a client
	When  I am make a POST request to '<URL>' with the following Data '<RequestData>'
	Then response code must be '<ResponseCode>'
	And response data must look like '<ResponseData>'

	@ValidCase
	Examples:
	| URL        | RequestData                | ResponseCode | ResponseData |
	| api/genres | {"Name": "Mock Adventure"} | 200          | 3            |

	@InvalidCase
	Examples:
	| URL        | RequestData   | ResponseCode | ResponseData                 |
	| api/genres | {"Name": "" } | 400          | Name cannot be empty or null |

@Update
Scenario: Update Genre
	Given I am a client
	When  I make PUT Request '<URL>' with the following Data '<RequestData>'
	Then response code must be '<ResponseCode>'
	And response data must look like '<ResponseData>'

	@ValidCase
	Examples:
	| URL          | RequestData            | ResponseCode | ResponseData |
	| api/genres/1 | {"Name": "Mock Horror"} | 200          |              |

	@InvalidCase
	Examples:
	| URL           | RequestData             | ResponseCode | ResponseData                           |
	| api/genres/1  | {"Name": ""}            | 400          | Name cannot be empty or null           |
	| api/genres/20 | {"Name": "Mock Horror"} | 404          | There is no Genre to Update with Id 20 |

@Delete
Scenario: Delete Genre
	Given I am a client
	When I make Delete Request '<URL>'
	Then response code must be '<ResponseCode>'
	And response data must look like '<ResponseData>'

	@ValidCase
	Examples:
	| URL          | ResponseCode | ResponseData |
	| api/genres/2 | 200          |              |

	@InValidCase
	Examples:
	| URL           | ResponseCode | ResponseData               |
	| api/genres/20 | 404          | Genre with ID 20 not found |

