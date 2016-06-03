Feature: Create Express Authorization
	In order to create an express authorization
	As a Provider
	I want to validate the create express authorization process

Scenario: Create a new express authorization successfully
	Given I have the express authorization provider client
	And I have entered a valid insurer
	And I have entered a valid policy holder
	And I have the request data for a new express authorization
	When I make the new express authorization request to the endpoint
	Then the result should be Accepted
	And the queue id is not null or empty

Scenario: Create a new express authorization with an invalid insurer
	Given I have the express authorization provider client
	And I have entered an invalid insurer
	And I have entered a valid policy holder
	And I have the request data for a new express authorization
	When I make the new express authorization request to the endpoint
	Then the result should be bad request
	
Scenario Outline: Create a new express authorization with an invalid policy holder
	Given I have the express authorization provider client
	And I have entered a valid insurer
	And I have entered an invalid policy holder '<PolicyHolderField>'
	And I have the request data for a new express authorization
	When I make the new express authorization request to the endpoint
	Then the result should be bad request

	Scenarios: 
	| PolicyHolderField |
	| Id                |
	| DateOfBirth       |

Scenario: Create a new express authorization without insurer_id in the request
	Given I have the express authorization provider client
	And I have not included a insurer
	And I have entered a valid policy holder
	And I have the request data for a new express authorization
	When I make the new express authorization request to the endpoint
	Then the result should be bad request

Scenario: Create a new express authorization without policy_holder in the request
	Given I have the express authorization provider client
	And I have entered a valid insurer
	And I have not included a policy holder
	And I have the request data for a new express authorization
	When I make the new express authorization request to the endpoint
	Then the result should be bad request

Scenario: Create a new express authorization with invalid provider slug
	Given I have the express authorization provider client with invalid slug
	And I have entered a valid insurer
	And I have entered a valid policy holder
	And I have the request data for a new express authorization
	When I make the new express authorization request to the endpoint
	Then the result should be not found