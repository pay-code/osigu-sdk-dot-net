Feature: Submit a Product by a Provider
	In to submit a product
	As Provider
	I want to be able to send a product to be saved

Scenario: Authentication Error
	Given I have the provider products client without authorization
	And the submit a product request
	When I request the submit a product endpoint
	Then the result should be unauthorized

Scenario: Slug Does Not Exists
	Given I have the provider products client without valid slug
	And the submit a product request
	When I request the submit a product endpoint
	Then the result should be no permission

Scenario: Product Already Exists
	Given I have the provider products client
	And the submit a product request
	When I request the submit a product endpoint twice
	Then the result should be ok on the first
	And the result should be ignored on the second

Scenario: Product with Id Longer than Expected
	Given I have the provider products client
	And the submit a product request with longer id
	When I request the submit a product endpoint
	Then the result should be a validation error

Scenario Outline: Required Fields missing
	Given I have the provider products client
	And the submit a product request with missing fields
	| TestId   | MissingField   |
	| <TestId> | <MissingField> |
	When I request the submit a product endpoint
	Then the result should be missing field

Scenarios:
	| TestId | MissingField |
	| 1      | ProductId    |
	| 2      | Name         |
	| 3      | Full Name    |
	| 4      | Manufacturer |

Scenario: Submit new product successfully
	Given I have the provider products client
	And the submit a product request
	When I request the submit a product endpoint
	Then the result should be ok
	When the product is removed
	And I request the submit a product endpoint
	Then the result should be ok


