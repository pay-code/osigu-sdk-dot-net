Feature: Submit a Product by a Provider
	In to submit a product
	As Provider
	I want to be able to send a product to be saved

Scenario: Authentication Error
	Given I have the provider products client
	And an unauthorized provider user
	And the submit a product request
	When I request the submit a product endpoint
	Then the result should be unauthorized
