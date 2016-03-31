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

Scenario: Create a new valid authorization
	Given I have the insurer products client
	And I have the request data for a new insurer product
	When I make the add a product insurer request to the endpoint
	Then I have ok response of adding that product
	And I have the insurer authorizations client
	And I have the request data for a new authorization
	When I make the new authorization request to the endpoint
	Then I have valid response for creating the authorization