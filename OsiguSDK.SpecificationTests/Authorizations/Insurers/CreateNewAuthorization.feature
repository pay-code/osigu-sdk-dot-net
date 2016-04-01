Feature: Create a new authorization as an Insurer
	In order to create an authorization
	As an Insurer 
	I need to send all the neccesary data of the authorization

Scenario: Create a new authorization with an invalid token
	Given I have the insurer authorizations client with an invalid token
	And I have the request data for a new authorization
	When I make the new authorization request to the endpoint
	Then the result should be forbidden for that request

Scenario: Create a new authorization with an invalid slug
	Given I have the insurer authorizations client with an invalid slug
	And I have the request data for a new authorization
	When I make the new authorization request to the endpoint
	Then the result should be unauthorized for that request

Scenario: Create a new authorization with an invalid reference_id
	Given I have the insurer authorizations client
	And I have the request data for a new authorization
	When I make the new authorization request to the endpoint
	Then I have valid response for creating the authorization
	When I make the new authorization request to the endpoint
	Then the result should be unprocessable fot that request

Scenario: Create a new authorization with an unreferenced product
	Given I have the insurer authorizations client
	And I have the request data for a new authorization with an unreferenced product
	When I make the new authorization request to the endpoint
	Then the result should be unprocessable fot that request

Scenario: Create a new authorization with a duplicate product
	Given I have the insurer authorizations client
	And I have the request data for a new authorization with a duplicate product
	When I make the new authorization request to the endpoint
	Then the result should be unprocessable fot that request

Scenario: Create a new valid authorization
	Given I have the insurer authorizations client
	And I have the request data for a new authorization
	When I make the new authorization request to the endpoint
	Then I have valid response for creating the authorization