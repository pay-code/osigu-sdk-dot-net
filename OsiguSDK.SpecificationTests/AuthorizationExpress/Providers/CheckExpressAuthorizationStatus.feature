Feature: Check Express Authorization Status
	In order to check the express authorization status
	As a Provider
	I want to be able to get the status of an express authorization

Scenario: Get express authorization status successfully 
	Given I have the provider express authorization client
	And I have created an express authorization
	And I have entered a valid queue id
	When I make the request to check express authorization endpoint
	Then the result should be see other
	And the status response should be working
	And the headers should contains the url resource

Scenario: Get express authorization status with invalid queue id
	Given I have the provider express authorization client
	And I have entered an invalid queue id
	When I make the request to check express authorization endpoint
	Then the result should be not found
	