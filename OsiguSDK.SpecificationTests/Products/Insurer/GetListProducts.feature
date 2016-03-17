Feature: Get a list of product by an Insurer
	In order to get a list of all products 
	As an Insurer
	I want to send an authorized request to get them

Scenario: Submit a request with an invalid token
	Given I have the insurer products client with an invalid token
	When I make the get list of products request to the endpoint
	Then the result should be unauthorized for get a list of products

Scenario: Submit a request with an invalid slug
	Given I have the insurer products client with an invalid slug
	When I make the get list of products request to the endpoint
	Then the result should be access denied for get a list of products

Scenario: Submit a valid request without parameters
	Given I have the insurer products client
	When I make the get list of products request to the endpoint
	Then the results should be list of products