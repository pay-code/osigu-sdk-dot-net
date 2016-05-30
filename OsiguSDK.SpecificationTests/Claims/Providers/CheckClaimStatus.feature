Feature: Check Claim Status
	In order to check the status of a Claim
	As Provider
	I want to be able to view the status of a claim

Scenario: Authentication Error
	Given I have the provider claims client
	And I have the insurer authorizations client
	And I have the queue client without authorization
	And I have the request data for a new authorization
	When I make the new authorization request to the endpoint
	And the create a claim request with repeated products
	And I request the create a claim endpoint
	And I request the check claim status endpoint
	Then the result should be unauthorized

Scenario: Invalid Queue Id
	Given I have the provider claims client
	And I have the insurer authorizations client
	And I have the queue client
	And I have the request data for a new authorization
	When I make the new authorization request to the endpoint
	And the create a claim request with repeated products
	And I request the create a claim endpoint
	And I request the check claim status endpoint with an invalid queue id
	Then the result should be not existing

Scenario: Success With Finalized Queue Status
	Given I have the provider claims client
	And I have the insurer authorizations client
	And I have the queue client
	And I have the request data for a new authorization
	When I make the new authorization request to the endpoint
	And the create a claim request with repeated products
	And I request the create a claim endpoint
	And I request the check claim status endpoint
	Then the result should be ok