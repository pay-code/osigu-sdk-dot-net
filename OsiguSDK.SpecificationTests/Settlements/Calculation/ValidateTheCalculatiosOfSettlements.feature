Feature: Validate the calculatios of Settements
	In order to validate the calculation of Settlements
	I want to create settlements 
	And validate the correct calculation for each one

Scenario Outline: Validate settlement calculation
	Given I have the settlement client
	And I have <NumberOfClaims> claims with amount '<ClaimAmount>'
	And I have entered a '<ProviderType>' 
	And I have entered a valid insurer
	And I have the request data for a new settlement
	When I make the request to the endpoint to create a new '<SettlementType>'
	Then the result should be created
	When I get the settlement created
	Then The calculation should be the expected
Scenarios:
| TestId | NumberOfClaims | ClaimAmount            | ProviderType        | SettlementType |
#| 1      | 1              | LESS_THAN_2800         | IsNotRetainingAgent | Cashout        |
#| 2      | 1              | BETWEEN_2800_AND_33600 | IsNotRetainingAgent | Cashout        |
#| 3      | 1              | GREATER_THAN_33600     | IsNotRetainingAgent | Cashout        |
#| 4      | 3              | LESS_THAN_2800         | IsNotRetainingAgent | Cashout        |
#| 5      | 3              | BETWEEN_2800_AND_33600 | IsNotRetainingAgent | Cashout        |
| 6      | 3              | GREATER_THAN_33600     | IsNotRetainingAgent | Cashout        |
#| 7      | 1              | LESS_THAN_2800         | IsNotRetainingAgent | Normal         |
#| 8      | 1              | BETWEEN_2800_AND_33600 | IsNotRetainingAgent | Normal         |
#| 9      | 1              | GREATER_THAN_33600     | IsNotRetainingAgent | Normal         |
#| 10     | 3              | LESS_THAN_2800         | IsNotRetainingAgent | Normal         |
#| 11     | 3              | BETWEEN_2800_AND_33600 | IsNotRetainingAgent | Normal         |
#| 12     | 3              | GREATER_THAN_33600     | IsNotRetainingAgent | Normal         |
#| 13     | 1              | LESS_THAN_2800         | IsRetainingAgent    | Cashout        |
#| 14     | 1              | BETWEEN_2800_AND_33600 | IsRetainingAgent    | Cashout        |
#| 15     | 1              | GREATER_THAN_33600     | IsRetainingAgent    | Cashout        |
#| 16     | 3              | LESS_THAN_2800         | IsRetainingAgent    | Cashout        |
#| 17     | 3              | BETWEEN_2800_AND_33600 | IsRetainingAgent    | Cashout        |
#| 18     | 3              | GREATER_THAN_33600     | IsRetainingAgent    | Cashout        |
#| 19     | 1              | LESS_THAN_2800         | IsRetainingAgent    | Normal         |
#| 20     | 1              | BETWEEN_2800_AND_33600 | IsRetainingAgent    | Normal         |
#| 21     | 1              | GREATER_THAN_33600     | IsRetainingAgent    | Normal         |
#| 22     | 3              | LESS_THAN_2800         | IsRetainingAgent    | Normal         |
#| 23     | 3              | BETWEEN_2800_AND_33600 | IsRetainingAgent    | Normal         |
#| 24     | 3              | GREATER_THAN_33600     | IsRetainingAgent    | Normal         |
