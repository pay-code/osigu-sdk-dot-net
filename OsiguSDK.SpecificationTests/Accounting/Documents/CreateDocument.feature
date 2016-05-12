Feature: Create Document
	In order to create a new document 
	As Internal Service
	I want to be able to create a new document

Scenario: Successfully Create A New Document
	Given I have the accounting client
	And I have the create a new document request
	When I request the create document endpoint
	Then the result should be ok
	And the document should be created successfully


Scenario: Successfully Create A New Payment Document
	Given I have the accounting client
	And I have the create a new document request
	And I have the create a new payment document request
	When I request the create document endpoint
	And I request the create a payment document endpoint
	Then the result should be ok
	And the document should be created successfully

Scenario: Payment Document Type is of the Same Type as the Original Document
	Given I have the accounting client
	And I have the create a new document request
	And I have the create a new payment document request with the same action type as the original
	When I request the create document endpoint
	And I request the create a payment document endpoint
	Then the result should be unprossesable entity

Scenario Outline: Payment Document Amount is not the Same as the Details
	Given I have the accounting client
	And I have the create a new document request
	And I have the create a new payment document request with an amount that doesnt match with the detail
	| TestId   | Difference   |
	| <TestId> | <Difference> |
	When I request the create document endpoint
	And I request the create a payment document endpoint
	Then the result should be unprossesable entity

Scenarios: 
| TestId | Difference |
| 1      | Lower      |
| 2      | Higher     |

Scenario:  Payment Document Amount is higher than the Original Document
	Given I have the accounting client
	And I have the create a new document request
	And I have the create a new payment document request with an amount that is higher than the original document
	When I request the create document endpoint
	And I request the create a payment document endpoint
	Then the result should be unprossesable entity

Scenario: Payment Document Has Invalid Amount
	Given I have the accounting client
	And I have the create a new document request
	And I have the create a new payment document request with an invalid amount
	When I request the create document endpoint
	And I request the create a payment document endpoint
	Then the result should be unprossesable entity

Scenario Outline: Payment Document Does Not Fills Required Fields
	Given I have the accounting client
	And I have the create a new document request
	And I have the create a new payment document request without the required fields
	| TestId   | PaymentType   | MissingElement   |
	| <TestId> | <PaymentType> | <MissingElement> |
	When I request the create document endpoint
	And I request the create a payment document endpoint
	Then the result should be unprossesable entity

Scenarios:
	| TestId   | PaymentType   | MissingElement   |
	| <TestId> | <PaymentType> | <MissingElement> |