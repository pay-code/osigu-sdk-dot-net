Feature: Submit product removal as an Insurer
	In order to remove a product
	As an Insurer
	I want to send an authorized request to remove it

Scenario: Request remove a product with an invalid token
	Given I have the insurer products client with an invalid token
	When I make the remove a product request to the endpoint
	Then the result should be unauthorized for removing a product

Scenario: Request remove a product with an invalid slug
	Given I have the insurer products client with an invalid slug
	When I make the remove a product request to the endpoint
	Then the result should be access denied for removing a product

Scenario: Request remove an unregistered product
	Given I have the insurer products client
	And I have an unregistered product information
	When I make the remove a product request to the endpoint
	Then the response should be 404 with product not found

Scenario: Request remove a product with an invalid status
	Given I have the insurer products client
	And I have the request data for a new product
	When I make the add a product insurer request to the endpoint
	Then i have a 200 response of adding that product
	When I make the remove a product request to the endpoint
	Then the response should be ok with code 204 
	When I make the remove a product request to the endpoint
	Then the response should be ok with code 404 

Scenario: Request remove a valid product
	Given I have the insurer products client
	And I have the request data for a new product
	When I make the add a product insurer request to the endpoint
	Then i have a 200 response of adding that product
	When I make the remove a product request to the endpoint
	Then the response should be ok with code 204 