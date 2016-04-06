Feature: Get a List of Claims
	In order to get a list of a claims
	As Provider
	I want to be able to get every claim


Scenario: Authentication Error
	Given I have the provider claims client without authorization
	When I request the get a list of claims endpoint
	Then the result should be unauthorized

Scenario: Slug Does Not Exists
	Given I have the provider claims client without valid slug
	When I request the get a list of claims endpoint
	Then the result should be no permission

Scenario: Successfully Get Claims
	Given I have the provider claims client
	When I request the get a list of claims endpoint
	Then the result should be ok
	And the list should have the same amount of claims as expected