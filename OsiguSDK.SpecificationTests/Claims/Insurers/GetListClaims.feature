Feature: Get a List of Claims by an Insurer
	In order to get a list of a claims
	As Insurer
	I want to be able to get every claim


Scenario: Authentication Error
	Given I have the insurer claims client without authorization
	When I request the get a list of claims as insurer endpoint
	Then the result should be unauthorized

Scenario: Slug Does Not Exists
	Given I have the insurer claims client without valid slug
	When I request the get a list of claims as insurer endpoint
	Then the result should be no permission

Scenario: Successfully Get Claims
	Given I have the insurer claims client
	When I request the get a list of claims as insurer endpoint
	Then the result should be ok
	And the list should have the same amount of insurer claims as expected