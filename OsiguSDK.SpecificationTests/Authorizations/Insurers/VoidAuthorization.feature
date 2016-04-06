Feature: Void an authorization as an Insurer
	In order to void an authorization
	As an Insurer 
	I need to send the id of the authorization

Scenario: Void an authorization with an invalid token
	Given I have the insurer authorizations client with an invalid token
	And I have the request data for a new authorization
	When I make the void authorization request to the endpoint
	Then the result should be forbidden for void the authorization

Scenario: Void an authorization with an invalid slug
	Given I have the insurer authorizations client with an invalid slug
	And I have the request data for a new authorization
	When I make the void authorization request to the endpoint
	Then the result should be not found for void the authorization

Scenario: Void an authorization with an invalid reference_id
	Given I have the insurer authorizations client
	And I have the request data for a new authorization
	When I make the void authorization request to the endpoint
	Then the result should be not found for void the authorization

Scenario: Void an authorization with a valid reference_id
	Given I have the insurer authorizations client
	And I have the request data for a new authorization
	When I make the new authorization request to the endpoint
	Then I have valid response for creating the authorization
	When I make the void authorization request to the endpoint
	Then the result should be a valid response for void the authorization