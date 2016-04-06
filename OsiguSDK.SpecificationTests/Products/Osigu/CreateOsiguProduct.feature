Feature: CreateOsiguProduct
	In order to create a new standard product
	As osigu internal application
	I want to be able to create a new osigu product

Scenario: Successfully Create Osigu Product
	Given I have configured the rest client
	And I have the request to create an osigu product
	When I request the create osigu product endpoint
	Then the result should be the osigu product created with the respective id
