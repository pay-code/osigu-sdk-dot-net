Feature: Check Express Authorization Status
	In order to check the express authorization status
	As a Provider
	I want to be able to get the status of an express authorization

Scenario: Get express authorization status successfully 
	Given I have the express authorization provider client
	And I have created an express authorization
	And I have entered a valid queue id
	When I check the queue status
	Then the resource should be created successfully
	

Scenario: Get express authorization status with invalid queue id
	Given I have entered an invalid queue id
	When I check the queue status
	Then the result should be not found
	