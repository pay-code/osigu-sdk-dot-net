Feature: Submit a Removal of a Product by a Provider
	In to remove a product
	As Provider
	I want to be able to remove a product

Scenario: Authentication Error
	Given I have the provider products client without authorization
	And a product created
	When I request the submit a removal endpoint
	Then the result should be unauthorized

Scenario: Security Authorization
	Given I have the provider products client without the required permission


Scenario: Slug Does Not Exists
	Given I have the provider products client without valid slug
	And a product created
	When I request the submit a removal endpoint
	Then the result should be no permission

