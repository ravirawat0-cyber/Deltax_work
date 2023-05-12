Feature: Producer Resource

Scenario: Get Producer All
	Given I am a client
	When I make GET Request '<URL>'
	Then response code must be '<ResponseCode>'
	And response data must look like '<ResponseData>'
	Examples: 
	| URL           | ResponseCode | ResponseData                                                                                                                                                                    |
	| api/producers | 200          | [{"id":1,"name":"Mock Producer","bio":"Mock bio","dob":"1990-12-12T00:00:00","sex":"M"},{"id":2,"name":"Mock Producer","bio":"Mock bio","dob":"1990-12-12T00:00:00","sex":"M"}] |

@GetById
Scenario: Get Producer by ID
	Given I am a client
    When I make GET Request '<URL>'
	Then response code must be '<ResponseCode>'
	And response data must look like '<ResponseData>'

	@ValidCase
	Examples:
	| URL             | ResponseCode | ResponseData                                                                           |
	| api/producers/1 | 200          | {"id":1,"name":"Mock Producer","bio":"Mock bio","dob":"1990-12-12T00:00:00","sex":"M"} |
	| api/producers/2 | 200          | {"id":2,"name":"Mock Producer","bio":"Mock bio","dob":"1990-12-12T00:00:00","sex":"M"} |
	
	@InvalidCase
	Examples:
	| URL              | ResponseCode | ResponseData                  |
	| api/producers/20 | 404          | Producer with ID 20 not found |
	| api/producers/6  | 404          | Producer with ID 6 not found  |

@Create	
Scenario: Create Producer
	Given I am a client
	When  I am make a POST request to '<URL>' with the following Data '<RequestData>'
	Then response code must be '<ResponseCode>'
	And response data must look like '<ResponseData>'

	@ValidCase
	Examples:
	| URL           | RequestData                                                                     | ResponseCode | ResponseData |
	| api/producers | {"Name": "Kevin Hart", "Bio": "Kevin bio", "DOB": "1990-12-12", "Sex": "F"}     | 200          | 3            |
	| api/producers | {"Name": "David Franklin", "Bio": "Kevin bio", "DOB": "2000-12-12", "Sex": "F"} | 200          | 3            |

	@InvalidCase
	Examples:
	| URL           | RequestData                                                                 | ResponseCode | ResponseData                            |
	| api/producers | {"Name": "", "Bio": "Kevin bio", "DOB": "1990-12-12", "Sex": "F"}           | 400          | Name cannot be empty or null            |
	| api/producers | {"Name": "Kevin Hart", "Bio": "", "DOB": "1990-12-12", "Sex": "F"}          | 400          | Bio cannot be empty or null             |
	| api/producers | {"Name": "Kevin Hart", "Bio": "Kevin bio", "DOB": "2023-12-12", "Sex": "F"} | 400          | DOB cannot be greater than current date |
	| api/producers | {"Name": "Kevin Hart", "Bio": "Kevin bio", "DOB": "1990-12-12", "Sex": ""}  | 400          | Sex cannot be empty or null             |


@Update
Scenario: Update Producer
	Given I am a client
	When  I make PUT Request '<URL>' with the following Data '<RequestData>'
	Then response code must be '<ResponseCode>'
	And response data must look like '<ResponseData>'

	@ValidCase
	Examples:
	| URL             | RequestData                                                                                   | ResponseCode | ResponseData |
	| api/producers/1 | {"Name": "Kevin Hart", "Bio": "Kevin is hollywood star", "DOB": "1990-12-12", "Sex": "M"}     | 200          |              |
	| api/producers/1 | {"Name": "David Hart", "Bio": "Kevin is hollywood star", "DOB": "1990-12-12", "Sex": "M"}     | 200          |              |
	| api/producers/1 | {"Name": "David Hart", "Bio": "Kevin is hollywood producer", "DOB": "1990-12-12", "Sex": "M"} | 200          |              |
	| api/producers/1 | {"Name": "David Hart", "Bio": "Kevin is hollywood producer", "DOB": "2000-12-12", "Sex": "M"} | 200          |              |

	@InvalidCase
	Examples:
	| URL              | RequestData                                                                               | ResponseCode | ResponseData                              |
	| api/producers/1  | {"Name": "", "Bio": "Kevin bio", "DOB": "1990-12-12", "Sex": "F"}                         | 400          | Name cannot be empty or null              |
	| api/producers/1  | {"Name": "Kevin Hart", "Bio": "", "DOB": "1990-12-12", "Sex": "F"}                        | 400          | Bio cannot be empty or null               |
	| api/producers/1  | {"Name": "Kevin Hart", "Bio": "Kevin bio", "DOB": "2023-12-12", "Sex": "F"}               | 400          | DOB cannot be greater than current date   |
	| api/producers/1  | {"Name": "Kevin Hart", "Bio": "Kevin bio", "DOB": "1990-12-12", "Sex": ""}                | 400          | Sex cannot be empty or null               |
	| api/producers/20 | {"Name": "Kevin Hart", "Bio": "Kevin is hollywood star", "DOB": "1990-12-12", "Sex": "M"} | 404          | There is no Producer to Update with Id 20 |

@Delete
Scenario: Delete Producer
	Given I am a client
	When I make Delete Request '<URL>'
	Then response code must be '<ResponseCode>'
	And response data must look like '<ResponseData>'

	@ValidCase
	Examples:
	| URL             | ResponseCode | ResponseData |
	| api/producers/2 | 200          |              |
	| api/producers/1 | 200          |              |

	@InValidCase
	Examples:
	| URL              | ResponseCode | ResponseData                  |
	| api/producers/20 | 404          | Producer with ID 20 not found |
	| api/producers/22 | 404          | Producer with ID 22 not found |

