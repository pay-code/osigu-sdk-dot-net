Feature: Get List of All Accounting Pending Settlements
	In order to get all the accounting pending settlements
	As settlement client
	I want to be able to ask for all settlements

Scenario: Successfully Get All Acounting Pending Settlements
	Given I have the settlement client
	When I request the get list of accounting pending settlements endpoint
	Then the result should be ok
	And the accounting pending list should not be empty

Scenario: Invalid token
	Given I have the settlement client with an invalid token
	When I request the get list of accounting pending settlements endpoint
	Then the result should be unauthorized

Scenario: Invalid slug
	Given I have the settlement client with an invalid slug
	When I request the get list of accounting pending settlements endpoint
	Then the result should be unauthorized
