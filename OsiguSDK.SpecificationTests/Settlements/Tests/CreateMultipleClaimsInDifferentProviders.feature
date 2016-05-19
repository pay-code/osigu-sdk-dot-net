Feature: Create Multiple Claims In Different Providers
	In order to create multiple claims for a settlement 
	As Provider
	I want to be able to create multiple claims


Scenario Outline: Create Claim Of A Provider
	Given I have the provider selected claims client
	| TestId   | Slug   | Token   | ClaimQuantity   | ProductIds   |
	| <TestId> | <Slug> | <Token> | <ClaimQuantity> | <ProductIds> |
	And I have the insurer authorizations client
	And I have the queue client 
	When I complete the claim creation process

Scenarios: 
	| TestId | Slug | Token | ClaimQuantity | ProductIds |
	| 1      | dev1 | dev1  | 15            | 1,2,3      |
	| 2      | dev2 | dev2  | 3             | 1,2,3      |
	| 3      | dev3 | dev3  | 4             | 1,2,3      |