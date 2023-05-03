Feature: Actor Resource

Scenario: Get Actor All
	Given I am a client
	When I make GET Request '<URL>'
	Then response code must be '<ResponseCode>'
	And response data must look like '<ResponseData>'
	Examples: 
	| URL        | ResponseCode | ResponseData                                                                                                                                                               |
	| api/actors | 200          | [{"id":1,"name":"Mock Actor","bio":"Tom bio","dob":"1990-12-12T00:00:00","sex":"M"},{"id":2,"name":"Mock Actor","bio":"Robert bio","dob":"1990-12-12T00:00:00","sex":"M"}] |
	| api/actors | 200          |                                                                                                                                                                            |

Scenario: Get Actor by ID
	Given I am a client
    When I make GET Request '<URL>'
	Then response code must be '<ResponseCode>'
	And response data must look like '<ResponseData>'

	@ValidCase
	Examples:
	| URL          | ResponseCode | ResponseData                                                                       |
	| api/actors/1 | 200          | {"id":1,"name":"Mock Actor","bio":"Tom bio","dob":"1990-12-12T00:00:00","sex":"M"} |
	
	@InvalidCase
	Examples:
	| URL           | ResponseCode | ResponseData               |
	| api/actors/20 | 404          | Actor with ID 20 not found |

Scenario: Create actor
	Given I am a client
	When  I am make a POST request to '<URL>' with the following Data '<RequestData>'
	Then response code must be '<ResponseCode>'
	And response data must look like '<ResponseData>'

	@ValidCase
	Examples:
	| URL        | RequestData                                                                | ResponseCode | ResponseData |
	| api/actors | {"Name": "Tom Holland", "Bio": "Tom bio", "DOB": "1990-12-12", "Sex": "F"} | 200          | 3            |

	@InvalidCase
	Examples:
	| URL        | RequestData                                                                | ResponseCode | ResponseData                            |
	| api/actors | {"Name": "", "Bio": "Tom bio", "DOB": "1990-12-12", "Sex": "F"}            | 400          | Name cannot be empty or null            |
	| api/actors | {"Name": "Tom Holland", "Bio": "", "DOB": "1990-12-12", "Sex": "F"}        | 400          | Bio cannot be empty or null             |
	| api/actors | {"Name": "Tom Holland", "Bio": "Tom bio", "DOB": "2023-12-12", "Sex": "F"} | 400          | DOB cannot be greater than current date |
	| api/actors | {"Name": "Tom Holland", "Bio": "Tom bio", "DOB": "1990-12-12", "Sex": ""}  | 400          | Sex cannot be empty or null             |

Scenario: Update actor
	Given I am a client
	When  I make PUT Request '<URL>' with the following Data '<RequestData>'
	Then response code must be '<ResponseCode>'
	And response data must look like '<ResponseData>'

	@ValidCase
	Examples:
	| URL          | RequestData                                                                              | ResponseCode | ResponseData |
	| api/actors/1 | {"Name": "Tom Holland", "Bio": "Tom is hollywood star", "DOB": "1990-12-12", "Sex": "M"} | 200          |              |

	@InvalidCase
	Examples:
	| URL           | RequestData                                                                              | ResponseCode | ResponseData                            |
	| api/actors/1  | {"Name": "", "Bio": "Tom bio", "DOB": "1990-12-12", "Sex": "F"}                          | 400          | Name cannot be empty or null            |
	| api/actors/1  | {"Name": "Tom Holland", "Bio": "", "DOB": "1990-12-12", "Sex": "F"}                      | 400          | Bio cannot be empty or null             |
	| api/actors/1  | {"Name": "Tom Holland", "Bio": "Tom bio", "DOB": "2023-12-12", "Sex": "F"}               | 400          | DOB cannot be greater than current date |
	| api/actors/1  | {"Name": "Tom Holland", "Bio": "Tom bio", "DOB": "1990-12-12", "Sex": ""}                | 400          | Sex cannot be empty or null             |
	| api/actors/20 | {"Name": "Tom Holland", "Bio": "Tom is hollywood star", "DOB": "1990-12-12", "Sex": "M"} | 404          | There is no Actor to Update with Id 20  |

Scenario: Delete actor
	Given I am a client
	When I make Delete Request '<URL>'
	Then response code must be '<ResponseCode>'
	And response data must look like '<ResponseData>'

	@ValidCase
	Examples:
	| URL          | ResponseCode | ResponseData |
	| api/actors/1 | 200          |              |

	@InValidCase
	Examples:
	| URL           | ResponseCode | ResponseData                        |
	| api/actors/20 | 404          | No Actor to Delete with given ID 20 |

