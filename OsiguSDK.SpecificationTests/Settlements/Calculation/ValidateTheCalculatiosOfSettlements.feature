Feature: Validate the calculatios of Settements
	In order to validate the calculation of Settlements
	I want to create Normal and Cashout settlements 
	And validate the correct calculation for each one

Scenario: Validate cashout when is not retention agent and amount is less than 2500
	Given I have the settlement client
	And I have claims with amount less than 2500
	And I have entered a non retention provider
	And I have entered a valid insurer
	And I have the request data for a new cashout settlement
	When I make the request to the endpoint to create a new cashout
	Then the result should be 202
	When I get the settlement created
	Then The calculation should be the expected

