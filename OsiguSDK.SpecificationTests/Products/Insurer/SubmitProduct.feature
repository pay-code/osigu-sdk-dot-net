Feature: SubmitProduct as an Insurer
	In order to add a product
	As an Insurer
	I want to send an authorized request to associate and add the product


Scenario: Submit a request with an invalid token
	Given I have the insurer products client with an invalid token
	When I make the get list of products request to the endpoint
	Then the result should be unauthorized for get a list of products

Scenario: Submit a request with an invalid slug
	Given I have the insurer products client with an invalid slug
	When I make the get list of products request to the endpoint
	Then the result should be access denied for get a list of products

Scenario: Submit a new valid product
	Given I have the insurer products client
	And I have the request data for a new product
	When I make the add a product insurer request to the endpoint
	Then the response should be ok with code 204
