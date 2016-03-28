Feature: Get a list of product by a Provider
	In order to get a list of all products 
	As an Provider
	I want to send an authorized request to get them

Scenario: Submit a request with an invalid token
	Given I have the provider products client without authorization
	When I make the get list of provider products request to the endpoint
	Then the result should be unauthorized

Scenario: Submit a request with an invalid slug
	Given I have the provider products client without valid slug
	When I make the get list of provider products request to the endpoint
	Then the result should be no permission

Scenario: Submit a valid request without parameters
	Given I have the provider products client
	When I make the get list of provider products request to the endpoint
	Then the results should be list of products