Feature: Complete An Express Authorization
	In order to complete the transaction of an express authorization
	As a Provider
	I want to be able to complete the express authorization

Scenario: Complete An Express Authorization Sucessfully
	Given I have the provider express authorization client
	And I have entered a valid provider slug
	And I have entered a valid authorization id
	And I have the request data for complete an express authorization
	When I make the complete express authorization request to the endpoint
	Then the result should be ok
	And the result should be the express authorization including the invoice sent

Scenario: Complete An Express Authorization with an invalid provider slug
	Given I have the provider express authorization client
	And I have entered an invalid provider slug
	And I have entered a valid authorization id
	And I have the request data for complete an express authorization
	When I make the complete express authorization request to the endpoint
	Then the result should be bad request
	

Scenario: Complete An Express Authorization with an invalid authorization
	Given I have the provider express authorization client
	And I have entered a valid provider slug
	And I have entered an invalid authorization id
	And I have the request data for complete an express authorization
	When I make the complete express authorization request to the endpoint
	Then the result should be bad request
	


Scenario: Complete An Express Authorization with negative amount
	Given I have the provider express authorization client
	And I have entered a valid provider slug
	And I have entered a valid authorization id
	And The amount of invoice is negative
	And I have the request data for complete an express authorization
	When I make the complete express authorization request to the endpoint
	Then the result should be bad request


Scenario: Complete An Express Authorization with amount equal to cero
	Given I have the provider express authorization client
	And I have entered a valid provider slug
	And I have entered a valid authorization id
	And The amount of invoice is equal to cero
	And I have the request data for complete an express authorization
	When I make the complete express authorization request to the endpoint
	Then the result should be bad request

Scenario: Complete An Express Authorization with amount greater than sum of products
	Given I have the provider express authorization client
	And I have entered a valid provider slug
	And I have entered a valid authorization id
	And The amount of invoice is greater than sum of products
	And I have the request data for complete an express authorization
	When I make the complete express authorization request to the endpoint
	Then the result should be bad request

Scenario: Complete An Express Authorization with amount less than sum of products
	Given I have the provider express authorization client
	And I have entered a valid provider slug
	And I have entered a valid authorization id
	And The amount of invoice is less than sum of products
	And I have the request data for complete an express authorization
	When I make the complete express authorization request to the endpoint
	Then the result should be bad request


Scenario: Validate required fields when Complete An Express Authorization
	Scenario: Complete An Express Authorization with amount less than sum of products
	Given I have the provider express authorization client
	And I have entered a valid provider slug
	And I have entered a valid authorization id
	And The required fields are missing
	And I have the request data for complete an express authorization
	When I make the complete express authorization request to the endpoint
	Then the result should be bad request