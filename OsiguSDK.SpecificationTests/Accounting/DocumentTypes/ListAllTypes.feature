Feature: List All Document Types
	In order to get all document types
	As Internal Service
	I want to be able to get all the document types


Scenario: Successfully get all document types
	Given I have the accounting client
	When I request the get all document types endpoint
	Then the result should be ok
	And the amount of document types should be the expected
