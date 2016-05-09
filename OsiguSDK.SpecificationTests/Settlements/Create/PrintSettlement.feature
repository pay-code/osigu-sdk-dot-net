Feature: Print Settlement
	In order to print a settlement
	I want to validate the creation file
	And the settlement status

Scenario: Print a Insurer Settlement
	Given I have the settlement client
	And I have '<SettlementType>' settlement with <NumberOfClaims> claims with amount '<ClaimAmount>' 
	When I make the request to the list all settlements endpoint
	Then result should be ok
