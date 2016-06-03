Feature: Get an express authorization as an Insurer
	In order to obtain an express authorization
	As an Insurer 
	I need to send the valid id of the authorization

Scenario: Get an express authorization with an invalid token as an insurer
	Given I have the insurer express authorizations client with an invalid token
	When I make the get express authorization request to the endpoint as an insurer
	Then the result should be forbidden for getting the express authorization

Scenario: Get an express authorization with an invalid slug as an insurer
	Given I have the insurer express authorizations client with an invalid slug
	When I make the get express authorization request to the endpoint as an insurer
	Then the result should be not found for getting the express authorization

Scenario: Get an express authorization with an invalid express authorization id
	Given I have the insurer express authorizations client
	And I have an invalid express authorization id
	When I make the get express authorization request to the endpoint as an insurer
	Then the result should be not found for getting the express authorization

Scenario: Get an express authorization with a valid express authorization id
	Given I have the insurer express authorizations client
	And I create a valid express authorization
	When I make the get express authorization request to the endpoint as an insurer
	Then I have a valid response for getting the express authorization as an insurer