Feature: Getting a product as an Provider
	In order to get information about a product
	As an Provider
	I want to send an authorized request to get it

Scenario: Submit a request with an invalid token
	Given I have the provider products client without authorization
	When I make the get a product provider request to the endpoint
	Then the result should be unauthorized

Scenario: Submit a request with an invalid slug
	Given I have the provider products client without valid slug
	When I make the get a product provider request to the endpoint
	Then the result should be no permission

Scenario: Submit a request with an invalid product id
	Given I have the provider products client
	And I have an invalid provider product id
	When I make the get a product provider request to the endpoint
	Then the result should be the product does not exist

Scenario: Submit a request with a valid product id
	Given I have the provider products client
	And I have the request data for a new product
	When I request the submit a product endpoint
	When I make the get a product provider request to the endpoint
	Then the result should be the providers product information