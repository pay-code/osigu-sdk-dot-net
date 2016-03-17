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
	And a product created
	When I request the submit a removal endpoint without permission
	Then the result should be unauthorized

Scenario: Slug Does Not Exists
	Given I have the provider products client without valid slug
	And a product created
	When I request the submit a removal endpoint
	Then the result should be no permission

Scenario: Product Does Not Exists
	Given I have the provider products client 
	When I request the submit a removal endpoint
	Then the result should be product don't exists

Scenario: Product Already Deleted
	Given I have the provider products client 
	And a product created
	When I request the submit a removal endpoint
	And I request the submit a removal endpoint
	Then the result should be product status error

Scenario: Product Deleted Successfully
	Given I have the provider products client 
	And a product created
	When I request the submit a removal endpoint
	Then the result should be product deleted successfully