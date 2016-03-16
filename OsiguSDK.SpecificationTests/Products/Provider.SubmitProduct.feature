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
	And the result should be error on the second

Scenario: Product with the Same Name Already Exists
	Given I have the provider products client
	And the submit a product request
	When I request the submit a product endpoint twice with the same name
	Then the result should be ok on the first
	And the result should be error because of repeated name on the second



