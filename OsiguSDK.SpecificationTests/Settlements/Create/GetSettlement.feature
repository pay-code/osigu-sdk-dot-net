Feature: Get Settlements
	In order to get the data of the settlements created
	I want to invoke the endpoints for getting settlemnts
	And validate the result of each one

Scenario Outline: Validate List All Settlements
	Given I have the settlement client
	And I created one '<SettlementType>' settlment with <NumberOfClaims> claims with amount '<ClaimAmount>' 
	When I make the request to the list all settlements endpoint
	Then The result should be the list of all settlments
	And The settlement created should be the expected

Scenarios:
| TestId | NumberOfClaims | ClaimAmount            | SettlementType |
| 1      | 1              | LESS_THAN_2800         | Cashout        |
| 2      | 1              | BETWEEN_2800_AND_33600 | Normal         |