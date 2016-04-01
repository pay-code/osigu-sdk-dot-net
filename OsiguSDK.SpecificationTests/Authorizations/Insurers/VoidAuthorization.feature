Feature: Void an authorization as an Insurer
	In order to void an authorization
	As an Insurer 
	I need to send the id of the authorization

Scenario: Void an authorization with an invalid token
	Given I have the insurer authorizations client with an invalid token
	And I have the request data for a new authorization
	When I make the void authorization request to the endpoint
	Then the result should be forbidden for voiding the authorization