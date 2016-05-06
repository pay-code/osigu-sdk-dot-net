Feature: Create a Claim
	In order to create a new claim
	As Provider
	I want to be able to send a claim to be saved

Scenario: Authentication Error
	Given I have the provider claims client without authorization
	And I have the insurer authorizations client
	And I have the request data for a new authorization
	When I make the new authorization request to the endpoint
	And the create a claim request
	When I request the create a claim endpoint
	Then the result should be unauthorized

Scenario: Slug Does Not Exists
	Given I have the provider claims client without valid slug
	And the create a claim request
	When I request the create a claim endpoint
	Then the result should be no permission

Scenario: Slug Does Not Match
	Given I have the provider claims client two
	And I have the insurer authorizations client
	And I have the request data for a new authorization
	When I make the new authorization request to the endpoint
	And the create a claim request
	And I request the create a claim endpoint with the second client
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
	And I have the insurer authorizations client
	And I have the request data for a new authorization
	When I make the new authorization request to the endpoint
	And I request the create a claim endpoint
	Then the result should be unprossesable entity

Scenario Outline: Required Fields missing
	Given I have the provider claims client
	And the create a claim request with missing fields
	| TestId   | MissingField   |
	| <TestId> | <MissingField> |
	And I have the insurer authorizations client
	And I have the request data for a new authorization
	When I make the new authorization request to the endpoint
	And I request the create a claim endpoint
	Then the result should be unprossesable entity

Scenarios:
	| TestId | MissingField  |
	| 1      | PIN           |
	| 2      | Empty Items   |
	| 3      | Empty Items 2 |
	| 4      | Product Id    |
	| 5      | Price         |
	| 6      | Quantity      |

Scenario: Provider Product Does Not Exists as Osigu Product
	Given I have the provider claims client
	And I have the insurer authorizations client
	And I have the request data for a new authorization
	When I make the new authorization request to the endpoint
	And the create a claim request with a product that does not exists in osigu products
	When I request the create a claim endpoint
	Then the result should be unprossesable entity

Scenario: Create Claim Successfully With Only Different Products
	Given I have the provider claims client
	And I have the insurer authorizations client
	And I have the request data for a new authorization
	When I make the new authorization request to the endpoint
	And the create a claim request with different products
	And I request the create a claim endpoint
	Then the result should be ok

Scenario: Create Claim Successfully With Repeated Products
	Given I have the provider claims client
	And I have the insurer authorizations client
	And I have the request data for a new authorization
	When I make the new authorization request to the endpoint
	And the create a claim request with repeated products
	And I request the create a claim endpoint
	Then the result should be ok

Scenario Outline: Create Claim Successfully With Substitute Products
	Given I have the provider claims client
	And I have the insurer authorizations client
	And I have the request data for a new authorization
	When I make the new authorization request to the endpoint
	And the create a claim request with substitute products
	| TestId   | ItemId   | FixQuantity   | ExpectedResult   |
	| <TestId> | <ItemId> | <FixQuantity> | <ExpectedResult> |
	And I request the create a claim endpoint
	Then the result should be the expected 

Scenarios: 
	| TestId | ItemId | FixQuantity | ExpectedResult |
	| 1      | 0      | Same        | 422            |
	| 2      | 1      | Same        | 0              |
	| 3      | 2      | Same        | 422            |
	| 4      | 3      | Same        | 422            |
	| 5      | 4      | Higher      | 422            |
	| 6      | 4      | Lower       | 0              |
	| 7      | 5      | Same        | 422            |
	| 8      | 6      | Same        | 422            |

Scenario: Create Claim Successfully With Substitute Products With Different Ingredients
	Given I have the provider claims client
	And I have the insurer authorizations client
	And I have the request data for a new authorization
	When I make the new authorization request to the endpoint
	And the create a claim request with substitute products with differente ingredients
	And I request the create a claim endpoint
	Then the result should be unprossesable entity
