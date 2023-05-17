Feature: E-commerce Api


Scenario: Fetch all products available
Given I am a client
When I make a GET request to "/products"
Then the response code must be 200
And the response data must look like a list of all products

Scenario: Fetch top 10 products based on price (ascending)
Given I am a client
When I make a GET request to "/products?sort=ascending&limit=10"
Then the response code must be 200
And the response data must look like a list of the top 10 products sorted by price in ascending order

Scenario: Fetch top 10 products based on price (descending)
Given I am a client
When I make a GET request to "/products?sort=descending&limit=10"
Then the response code must be 200
And the response data must look like a list of the top 10 products sorted by price in descending order

Scenario: Create a new order with products/items
Given I am a client
And I have selected the products/items for the order
When I make a POST request to "/orders" with the order details
Then the response code must be 201
And the response data must look like the details of the newly created order

Scenario: Fetch all orders made by the customer in the past month
Given I am a client
When I make a GET request to "/orders/customers/{Id}?date=past_month"
Then the response code must be 200
And the response data must look like a list of all orders made by the customer in the past month