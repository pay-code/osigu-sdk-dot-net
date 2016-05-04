Feature: Create a new settlement
	In order to validate the creation of a new settlement
	I want to create settlements
	And validate each one

# Scenarios for settlement creation successfully are validated in "Validate settlement calculation"

Scenario Outline: Validate settlement creation with insurer_id invalid
	Given I have the settlement client
	And I have <NumberOfClaims> claims with amount '<ClaimAmount>'
	And I have entered a valid provider
	And I have entered an invalid insurer
	And I have entered valid dates
	And I have the request data for a new settlement
	When I make the request to the endpoint to create a new '<SettlementType>'
	Then the result should be 422
	And the message should be '<ErrorMessage>'

Scenarios:
| TestId | NumberOfClaims | ClaimAmount            | SettlementType | ErrorMessage                                 |
| 1      | 1              | LESS_THAN_2800         | Cashout        | Insurer {insurerId} is invalid or not exists |
| 2      | 1              | BETWEEN_2800_AND_33600 | Normal         | Insurer {insurerId} is invalid or not exists |

Scenario Outline: Validate settlement creation with provider_id invalid
	Given I have the settlement client
	And I have <NumberOfClaims> claims with amount '<ClaimAmount>'
	And I have entered an invalid provider
	And I have entered a valid insurer
	And I have entered valid dates
	And I have the request data for a new settlement
	When I make the request to the endpoint to create a new '<SettlementType>'
	Then the result should be 422
	And the message should be '<ErrorMessage>'

Scenarios:
| TestId | NumberOfClaims | ClaimAmount            | SettlementType | ErrorMessage                                |
| 1      | 1              | LESS_THAN_2800         | Cashout        | Provider {providerId} is invalid or not exists |
| 2      | 1              | BETWEEN_2800_AND_33600 | Normal         | Provider {providerId} is invalid or not exists |

Scenario Outline: Validate settlement creation with an empty list of claims
	Given I have the settlement client
	And I have entered a empty list of claims
	And I have entered a valid provider
	And I have entered a valid insurer
	And I have entered valid dates
	And I have the request data for a new settlement
	When I make the request to the endpoint to create a new '<SettlementType>'
	Then the result should be 422
	And the error list should be '<ErrorList>'

Scenarios:
| TestId | NumberOfClaims | ClaimAmount            | SettlementType | ErrorList                                       |
| 1      | 1              | LESS_THAN_2800         | Cashout        | [{"Path":"items","Message":"may not be empty"}] |
| 2      | 1              | BETWEEN_2800_AND_33600 | Normal         | [{"Path":"items","Message":"may not be empty"}] |


Scenario Outline: Validate settlement creation with a list of claims invalid
	Given I have the settlement client
	And I have entered a list of claims invalid
	And I have entered a valid provider
	And I have entered a valid insurer
	And I have entered valid dates
	And I have the request data for a new settlement
	When I make the request to the endpoint to create a new '<SettlementType>'
	Then the result should be 422
	And the error list should be '<ErrorList>'

Scenarios:
| TestId | NumberOfClaims | ClaimAmount            | SettlementType | ErrorList                                       |
| 1      | 1              | LESS_THAN_2800         | Cashout        | [{"Path":"invalid_claim","Message":"Claim (99999999) is invalid or not exists"}] |
| 2      | 1              | BETWEEN_2800_AND_33600 | Normal         | [{"Path":"invalid_claim","Message":"Claim (99999999) is invalid or not exists"}] |


Scenario Outline: Validate settlement creation with date from greater than date to
	Given I have the settlement client
	And I have <NumberOfClaims> claims with amount '<ClaimAmount>'
	And I have entered a valid provider
	And I have entered a valid insurer
	And I have entered invalid dates
	And I have the request data for a new settlement
	When I make the request to the endpoint to create a new '<SettlementType>'
	Then the result should be 422
	And the error list should be '<ErrorList>'

Scenarios:
| TestId | NumberOfClaims | ClaimAmount            | SettlementType | ErrorList                                       |
| 1      | 1              | LESS_THAN_2800         | Cashout        | [{"Path":"invalid_dates","Message":"Date FROM must be after date TO"}] |
| 2      | 1              | BETWEEN_2800_AND_33600 | Normal         | [{"Path":"invalid_dates","Message":"Date FROM must be after date TO"}] |


Scenario Outline: Validate settlement creation with required fields
	Given I have the settlement client
	And I have <NumberOfClaims> claims with amount '<ClaimAmount>'
	And I have entered a valid provider
	And I have entered a valid insurer
	And I have entered valid dates
	And I have an empty request data for a new settlement
	When I make the request to the endpoint to create a new '<SettlementType>'
	Then the result should be 422
	And the error list should be '<ErrorList>'

Scenarios:
| TestId | NumberOfClaims | ClaimAmount            | SettlementType | ErrorList                                                                                                                                                                                                                                  |
| 1      | 1              | LESS_THAN_2800         | Cashout        | [{"Path":"items","Message":"may not be empty"},{"Path":"from","Message":"may not be null"},{"Path":"provider_id","Message":"may not be null"},{"Path":"insurer_id","Message":"may not be null"},{"Path":"to","Message":"may not be null"}] |
| 2      | 1              | BETWEEN_2800_AND_33600 | Normal         | [{"Path":"items","Message":"may not be empty"},{"Path":"from","Message":"may not be null"},{"Path":"provider_id","Message":"may not be null"},{"Path":"insurer_id","Message":"may not be null"},{"Path":"to","Message":"may not be null"}] |


Scenario Outline: Validate settlement creation with json invalid
	Given I have the settlement client
	And I have <NumberOfClaims> claims with amount '<ClaimAmount>'
	And I have entered a valid provider
	And I have entered a valid insurer
	And I have entered valid dates
	And I have an invalid request data for a new settlement
	When I make the request to the endpoint to create a new '<SettlementType>'
	Then the result should be 400
	And the message should be '<ErrorMessage>'

Scenarios:
| TestId | NumberOfClaims | ClaimAmount            | SettlementType | ErrorMessage                                                |
| 1      | 1              | LESS_THAN_2800         | Cashout        | Bad request, the request body should be a valid JSON object |
| 2      | 1              | BETWEEN_2800_AND_33600 | Normal         | Bad request, the request body should be a valid JSON object |