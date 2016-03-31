Feature: Create a Claim
	In order to create a new claim
	As Provider
	I want to be able to send a claim to be saved

Scenario: Authentication Error
	Given I have the provider claims client without authorization
	And the create a claim request
	When I request the create a claim endpoint
	Then the result should be unauthorized

Scenario: Slug Does Not Exists
	Given I have the provider claims client without valid slug
	And the create a claim request
	When I request the create a claim endpoint
	Then the result should be no permission

Scenario: Slug Does Not Match
	Given I have the provider claims client
	And I have the provider claims client two
	And the create a claim request
	When I request the create a claim endpoint with the second client
	Then the result should be no permission

Scenario: Authorization Does Not Exists
	Given I have the provider claims client 
	And the create a claim request with an unexisting authorization
	When I request the create a claim endpoint
	Then the result should be not existing 

Scenario: Authorization Exists But Is Not Associated With The Provider
	Given I have the provider claims client
	And the create a claim request with an authorization not associated with the provider
	When I request the create a claim endpoint
	Then the result should be not existing 

Scenario: PIN Is Not Valid
	Given I have the provider claims client
	And the create a claim request with an invalid pin
	When I request the create a claim endpoint
	Then the result should be not existing 

Scenario Outline: Required Fields missing
	Given I have the provider claims client
	And the create a claim request with missing fields
	| TestId   | MissingField   |
	| <TestId> | <MissingField> |
	When I request the create a claim endpoint
	Then the result should be missing field

Scenarios:
	| TestId | MissingField  |
	| 1      | PIN           |
	| 2      | Empty Items   |
	| 3      | Empty Items 2 |
	| 4      | Product Id    |
	| 5      | Price         |
	| 6      | Quantity      |

Scenario: Provider Producto Does Not Exists as Osigu Product
	Given I have the provider claims client
	And the create a claim request with a product that does not exists in osigu products
	When I request the create a claim endpoint
	Then the result should be not existing

Scenario: Create Claim Successfully With Only Different Products
	Given I have the provider claims client
	And the create a claim request with different products
	When I request the create a claim endpoint
	Then the result should be ok

Scenario: Create Claim Successfully With Repeated Products
	Given I have the provider claims client
	And the create a claim request with repeated products
	When I request the create a claim endpoint
	Then the result should be ok