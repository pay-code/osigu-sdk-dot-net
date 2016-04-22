Feature: Create a new settlement
	In order to create a new normal settlement
	I need to send all the neccesary data of the settlement configuration

	Scenario: Create a new normal settlement
	Given I have the settlements client
	And I have the request data for a new normal settlement
	When I make the create normal settlement authorization request to the endpoint
	Then the result should be 204 