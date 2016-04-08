Feature: GetListAllSettlements
	In order to get all settlements
	As settlement client
	I want to be able to ask for all settlements

Scenario: Successfully Get All Settlements
	Given I have the settlement client
	When I request the endpoint
	Then the result should be ok
	And the settlement list should not be empty
