Feature: Create Multiple Claims In Different Providers
	In order to create multiple claims for a settlement 
	As Provider
	I want to be able to create multiple claims


Scenario Outline: Create Claim Of A Provider
	Given I have the provider selected claims client
	| TestId   | Provider   | ClaimQuantity   | ProductIds   |
	| <TestId> | <Provider> | <ClaimQuantity> | <ProductIds> |
	And I have the insurer authorizations client
	And I have the queue client 
	When I complete the claim creation process

Scenarios: 
	| TestId | Provider | ClaimQuantity |
	| 1      | 1        | 10            |
	| 2      | 2        | 10            |
	| 3      | 3        | 10            |