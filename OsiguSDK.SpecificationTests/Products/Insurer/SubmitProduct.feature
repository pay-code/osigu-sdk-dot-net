Feature: SubmitProduct as an Insurer
	In order to add a product
	As an Insurer
	I want to send an authorized request to associate and add the product

Scenario: Request to add a product with an invalid token
	Given I have the insurer products client with an invalid token
	When I make the add a product insurer request to the endpoint
	Then the result should be unauthorized for adding a product

Scenario: Request to add a product with an invalid slug
	Given I have the insurer products client with an invalid slug
	When I make the add a product insurer request to the endpoint
	Then the result should be access denied for adding a product

Scenario: Submit an existing product
	Given I have the insurer products client
	And I have the request data for a new product
	When I make the add a product insurer request to the endpoint
	Then I have a 200 response of adding that product
	When I make the add a product insurer request to the endpoint
	Then the response should be an error for adding a that product
	
Scenario: Submit a product with an invalid drug type
	Given I have the insurer products client
	And I have the request data for a new product whit an invalid drug type
	When I make the add a product insurer request to the endpoint
	Then the response should be an error for adding that product

Scenario: Submit a product with an existing product name
	Given I have the insurer products client
	And I have the request data for a new product
	When I make the add a product insurer request to the endpoint
	Then I have a 200 response of adding that product
	And I have the request data for anew producto whit a repeated name
	When I make the add a product insurer request to the endpoint
	Then the response should be an error for adding a that product

Scenario: Submit a product witout all the required data
	Given I have the insurer products client
	And I have the request data for a new product whitout all the request parameters
	When I make the add a product insurer request to the endpoint
	Then the response should be an error for adding that product

Scenario: Submit a new valid product
	Given I have the insurer products client
	And I have the request data for a new product
	When I make the add a product insurer request to the endpoint
	Then I have a 200 response of adding that product
