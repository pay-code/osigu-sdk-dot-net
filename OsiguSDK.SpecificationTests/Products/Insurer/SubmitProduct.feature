Feature: SubmitProduct as an Insurer
	In order to add a product
	As an Insurer
	I want to send an authorized request to associate and add the product

Scenario: Request to add a product with an invalid token
	Given I have the insurer products client with an invalid token
	And I have the request data for a new insurer product
	When I make the add a product insurer request to the endpoint
	Then the result should be unauthorized for adding a product

Scenario: Request to add a product with an invalid slug
	Given I have the insurer products client with an invalid slug
	And I have the request data for a new insurer product
	When I make the add a product insurer request to the endpoint
	Then the result should be access denied for adding a product

Scenario: Submit an existing product as an insurer
	Given I have the insurer products client
	And I have the request data for a new insurer product
	When I make the add a product insurer request to the endpoint
	Then I have ok response of adding that product
	When I make the add a product insurer request to the endpoint
	Then the response should be no content for adding a that product
	
Scenario: Submit a product with an invalid drug type as an insurer
	Given I have the insurer products client
	And I have the request data for a new product whit an invalid drug type
	When I make the add a product insurer request to the endpoint
	Then the response should be unproccessable for adding that product

Scenario: Submit a product with an existing product name
	Given I have the insurer products client
	And I have the request data for a new insurer product
	When I make the add a product insurer request to the endpoint
	Then I have ok response of adding that product
	And I have the request data for a new product whit a repeated name
	When I make the add a product insurer request to the endpoint
	Then I have ok response of adding that product

Scenario: Submit a product witout all the required data
	Given I have the insurer products client
	And I have the request data for a new product whitout all the request parameters
	When I make the add a product insurer request to the endpoint
	Then the response should be unproccessable for adding that product

Scenario: Submit a new valid product
	Given I have the insurer products client
	And I have the request data for a new insurer product
	When I make the add a product insurer request to the endpoint
	Then I have ok response of adding that product
