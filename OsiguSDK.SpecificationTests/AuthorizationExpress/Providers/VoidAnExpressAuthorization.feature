Feature: Void An Express Authorization
	In order to void an express authorization
	As a Provider
	I want to be able to void an express authorization

Scenario: Void an express authorization successfully after created
	Given I have the express authorization provider client
	And I have entered a valid authorization id
	When I make the void express authorization request to the endpoint
	Then the result should be no content

Scenario: Void an express authorization successfully after items added
	Given I have the express authorization provider client
	And I have created a valid express authorization
	When I make the void express authorization request to the endpoint
	Then the result should be no content

Scenario: Void an express authorization successfully after completed
	Given I have the express authorization provider client
	And I have created a valid express authorization
	And I have completed the express authorizaion
	When I make the void express authorization request to the endpoint
	Then the result should be no content

#Pendiente al momento que se puedan cambiar una autorizacion de estado del lado el insurer
Scenario: Void an express authorization when status is different to pending review or null
	Given I have the express authorization provider client
	And I have entered a valid authorization id
	And The authorization status is different to pending review or null
	When I make the void express authorization request to the endpoint
	Then the result should be bad request

Scenario: Void an express authorization with an invalid provider slug
	Given I have the express authorization provider client with invalid slug
	And I have entered a valid authorization id
	When I make the void express authorization request to the endpoint
	Then the result should be not found

Scenario: Void an express authorization with an invalid id
	Given I have the express authorization provider client
	And I have entered a invalid authorization id
	When I make the void express authorization request to the endpoint
	Then the result should be not found
