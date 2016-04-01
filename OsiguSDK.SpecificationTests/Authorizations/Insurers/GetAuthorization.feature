Feature: Get an authorization as an Insurer
	In order to obtain an authorization
	As an Insurer 
	I need to send the id of the authorization

Scenario: Get an authorization with an invalid token
	Given I have the insurer authorizations client with an invalid token
	And I have an invalid authorization id
	When I make the get authorization request to the endpoint
	Then the result should be forbidden for getting the authorization

Scenario: Get an authorization with an invalid slug
	Given I have the insurer authorizations client with an invalid slug
	And I have an invalid authorization id
	When I make the get authorization request to the endpoint
	Then the result should be not found for getting the authorization

Scenario: Get an authorization with an invalid authorization id
	Given I have the insurer authorizations client
	And I have an invalid authorization id
	When I make the get authorization request to the endpoint
	Then the result should be not found for getting the authorization

Scenario: Get an authorization with a valid authorization id
	Given I have the insurer authorizations client
	And I have the request data for a new authorization
	When I make the new authorization request to the endpoint
	Then I have valid response for creating the authorization
	And I make the get authorization request to the endpoint
	Then I have a valid response for getting the authorization