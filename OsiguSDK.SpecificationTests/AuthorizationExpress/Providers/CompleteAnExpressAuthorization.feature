Feature: Complete An Express Authorization
	In order to complete the transaction of an express authorization
	As a Provider
	I want to be able to complete the express authorization

#No se puede validar en sandbox
Scenario: Complete An Express Authorization Sucessfully
	Given I have the express authorization provider client
	And I have created a valid express authorization
	And I have the request data for complete an express authorization
	When I make the complete express authorization request to the endpoint
	Then the result should be ok
	And the result should be the express authorization including the invoice sent

Scenario: Complete An Express Authorization with an invalid provider slug
	Given I have the express authorization provider client with invalid slug
	And I have created a valid express authorization
	And I have the request data for complete an express authorization
	When I make the complete express authorization request to the endpoint
	Then the result should be not found
	
#No se puede validar en sandbox
Scenario: Complete An Express Authorization with an invalid authorization
	Given I have the express authorization provider client
	And I have created a valid express authorization
	And I have entered an invalid authorization id
	And I have the request data for complete an express authorization
	When I make the complete express authorization request to the endpoint
	Then the result should be bad request

#No se puede validar en sandbox
Scenario: Complete An Express Authorization with negative amount
	Given I have the express authorization provider client
	And I have created a valid express authorization
	And I have the request data for complete an express authorization
	And The amount of invoice is negative
	When I make the complete express authorization request to the endpoint
	Then the result should be bad request

#No se puede validar en sandbox
Scenario: Complete An Express Authorization with amount equal to cero
	Given I have the express authorization provider client
	And I have created a valid express authorization
	And I have the request data for complete an express authorization
	And The amount of invoice is equal to cero
	When I make the complete express authorization request to the endpoint
	Then the result should be bad request

#No se puede validar en sandbox
Scenario: Complete An Express Authorization with amount greater than sum of products
	Given I have the express authorization provider client
	And I have created a valid express authorization
	And I have the request data for complete an express authorization
	And The amount of invoice is greater than sum of products
	When I make the complete express authorization request to the endpoint
	Then the result should be bad request

#No se puede validar en sandbox
Scenario: Complete An Express Authorization with amount less than sum of products
	Given I have the express authorization provider client
	And I have created a valid express authorization
	And I have the request data for complete an express authorization
	And The amount of invoice is less than sum of products
	When I make the complete express authorization request to the endpoint
	Then the result should be bad request

#Validar los campos requeridos en DEV
Scenario: Validate required fields when Complete An Express Authorization
	Given I have the express authorization provider client
	And I have created a valid express authorization
	And I have the request data for complete an express authorization
	And The required fields are missing
	When I make the complete express authorization request to the endpoint
	Then the result should be bad request