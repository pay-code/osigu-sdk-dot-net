Feature: Create a Claim
	In order to create a new claim
	As Provider
	I want to be able to send a claim to be saved

Scenario: Authentication Error
	Given I have the provider claims client without authorization
	And the create a claim request
	When I request the create a claim endpoint
	Then the result should be unauthorized

Scenario: Slug Does Not Exists
	Given I have the provider claims client without valid slug
	And the create a claim request
	When I request the create a claim endpoint
	Then the result should be no permission

Scenario: Slug Does Not Match
	Given I have the provider claims client
	And I have the provider claims client two
	And the create a claim request
	When I request the create a claim endpoint with the second client
	Then the result should be no permission

Scenario: Authorization Does Not Exists
	Given I have the provider claims client 
	And the create a claim request with an unexisting authorization
	When I request the create a claim endpoint
	Then the result should be not existing 
