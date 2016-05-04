Feature: Get One Document Type
	In order to get an specific document type
	As Internal service
	I want to be able to ask for one document type and get it

Scenario: Successfully get document type
	Given I have the accounting client
	When I request the get a document type endpoint
	Then the result should be ok
	And the document type received should be the expected
