Feature: Update an authorization as an Insurer
	In order to update an authorization
	As an Insurer 
	I need to send the updated data of the authorization

Scenario: Update an authorization with an invalid token
	Given I have the insurer authorizations client with an invalid token
	And I have the request data for a new authorization
	When I make the update authorization request to the endpoint
	Then the result should be forbidden for updating the authorization

Scenario: Update an authorization with an invalid slug
	Given I have the insurer authorizations client with an invalid slug
	And I have the request data for a new authorization
	When I make the update authorization request to the endpoint
	Then the result should be not found for updating the authorization

Scenario: Update an authorization with an invalid authorization id
	Given I have the insurer authorizations client
	And I have the request data for a new authorization
	When I make the update authorization request to the endpoint
	Then the result should be not found for updating the authorization

Scenario: Update an authorization with a valid authorization id
	Given I have the insurer authorizations client
	And I have the request data for a new authorization
	When I make the new authorization request to the endpoint
	Then I have valid response for creating the authorization
	And I make the update authorization request to the endpoint
	Then the result should be not found for updating the authorization