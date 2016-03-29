Feature: Getting a product as an Insurer
	In order to get information about a product
	As an Insurer
	I want to send an authorized request to get it

Scenario: Submit a request with an invalid token
	Given I have the insurer products client with an invalid token
	When I make the get a product request to the endpoint
	Then the result should be unauthorized for getting a product

Scenario: Submit a request with an invalid slug
	Given I have the insurer products client with an invalid slug
	When I make the get a product request to the endpoint
	Then the result should be access denied for getting a product

Scenario: Submit a request with an invalid product id
	Given I have the insurer products client
	And I have an invalid product id
	When I make the get a product request to the endpoint
	Then I have an error response of getting that product

Scenario: Submit a request with a valid product id
	Given I have the insurer products client
	And I have the request data for a new insurer product
	When I make the add a product insurer request to the endpoint
	Then I have ok response of adding that product
	When I make the get a product request to the endpoint
	Then the result should be the product information