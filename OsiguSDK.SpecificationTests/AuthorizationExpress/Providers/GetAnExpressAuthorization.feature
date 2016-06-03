Feature: Get An Express Authorization
	In order to get an express authorization
	As a Provider
	I want to be able to get an express authorization data

Scenario: Get an express authorization successfully
	Given I have the express authorization provider client
	And I have created an complete express authorization
	When I make the get express authorization request to the endpoint
	Then the result should be ok
	And the authorization express data should be the expected

Scenario: Get an express authorization with an invalid provider slug
	Given I have the express authorization provider client with invalid slug
	And I have created an complete express authorization
	When I make the get express authorization request to the endpoint
	Then the result should be not found

Scenario: Get an express authorization with an invalid id
	Given I have the express authorization provider client
	And I have entered a invalid authorization id
	When I make the get express authorization request to the endpoint
	Then the result should be not found
