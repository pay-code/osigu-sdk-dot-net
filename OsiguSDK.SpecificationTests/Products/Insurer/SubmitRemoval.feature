Feature: Submit product removal as an Insurer
	In order to remove a product
	As an Insurer
	I want to send an authorized request to remove it

Scenario: Request remove a product with an invalid token
	Given I have the insurer products client with an invalid token
	And I have the request data for a new insurer product
	When I make the remove a product request to the endpoint
	Then the result should be unauthorized for removing a product

Scenario: Request remove a product with an invalid slug
	Given I have the insurer products client with an invalid slug
	And I have the request data for a new insurer product
	When I make the remove a product request to the endpoint
	Then the result should be access denied for removing a product

Scenario: Request remove an unregistered insurer product 
	Given I have the insurer products client
	And I have the request data for a new insurer product
	When I make the remove a product request to the endpoint
	Then the response should be an error for product not found

Scenario: Request remove a valid insurer product
	Given I have the insurer products client
	And I have the request data for a new insurer product
	When I make the add a product insurer request to the endpoint
	Then I have ok response of adding that product
	When I make the remove a product request to the endpoint
	Then the response should be ok for removing the product

Scenario: Request remove a product with an invalid status
	Given I have the insurer products client
	And I have the request data for a new insurer product
	When I make the add a product insurer request to the endpoint
	Then I have ok response of adding that product
	When I make the remove a product request to the endpoint
	Then the response should be ok for removing the product
	When I make the remove a product request to the endpoint
	Then the response should be an error for removing the product