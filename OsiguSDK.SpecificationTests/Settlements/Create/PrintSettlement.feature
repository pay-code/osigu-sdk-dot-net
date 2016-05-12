Feature: Print Settlement
	In order to print a settlement
	I want to validate the creation file
	And the settlement status

Scenario Outline: Print a Insurer Settlement
	Given I have the settlement client
	And I have '<SettlementType>' settlement with <NumberOfClaims> claims with amount '<ClaimAmount>' 
	And I specify a valid format '<Format>' for to print
	And I have the request data for print a settlement
	When I make the request to print the insurer settlement
	Then result should be ok

Scenarios:
| TestId | NumberOfClaims | ClaimAmount            | SettlementType | Format | 
| 1      | 1              | LESS_THAN_2800         | Cashout        | pdf    | 
| 2      | 1              | BETWEEN_2800_AND_33600 | Normal         | xls    | 