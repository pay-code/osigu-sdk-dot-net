Feature: Void An Express Authorization
	In order to void an express authorization
	As a Provider
	I want to be able to void an express authorization

Scenario: Void an express authorization successfully when status is pending review or null
	Given I have the provider express authorization client
	And I have entered a valid provider slug
	And I have entered a valid authorization id
	When I make the void express authorization request to the endpoint
	Then the result should be no content

Scenario: Void an express authorization when status is different to pending review or null
	Given I have the provider express authorization client
	And I have entered a valid provider slug
	And I have entered a valid authorization id
	And The authorization status is different to pending review or null
	When I make the void express authorization request to the endpoint
	Then the result should be bad request

Scenario: Void an express authorization with an invalid provider slug
	Given I have the provider express authorization client
	And I have entered an invalid provider slug
	And I have entered a valid authorization id
	When I make the void express authorization request to the endpoint
	Then the result should be bad request

Scenario: Void an express authorization with an invalid id
	Given I have the provider express authorization client
	And I have entered a valid provider slug
	And I have entered a invalid authorization id
	When I make the void express authorization request to the endpoint
	Then the result should be bad request
