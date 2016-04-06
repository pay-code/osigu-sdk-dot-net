Feature: Complete a Claim Transaction
	In order to complete the transaction of a claim
	As Provider
	I want to be able to send a claim to be completed


Scenario: Authentication Error
	Given I have the provider claims client
	And I have the insurer authorizations client
	And I have the queue client 
	And I have the request data for a new authorization
	When I make the new authorization request to the endpoint
	And the create a claim request with repeated products
	And I request the create a claim endpoint
	And I request the check claim status endpoint
	And I request the get a claim endpoint
	Given I have the provider claims client without authorization
	When I request the complete transaction request
	Then the result should be unauthorized

Scenario: Slug Does Not Exists
	Given I have the provider claims client
	And I have the insurer authorizations client
	And I have the queue client 
	And I have the request data for a new authorization
	When I make the new authorization request to the endpoint
	And the create a claim request with repeated products
	And I request the create a claim endpoint
	And I request the check claim status endpoint
	And I request the get a claim endpoint
	Given I have the provider claims client without valid slug
	When I request the complete transaction request
	Then the result should be no permission

Scenario: Invalid Claim Id
	Given I have the provider claims client
	And I have the insurer authorizations client
	And I have the queue client 
	And I have the request data for a new authorization
	When I make the new authorization request to the endpoint
	And the create a claim request with repeated products
	And I request the create a claim endpoint
	And I request the check claim status endpoint
	And I request the get a claim endpoint
	When I request the complete transaction request
	Then the result should be not existing 

Scenario Outline: Missing fields
	Given I have the provider claims client
	And I have the insurer authorizations client
	And I have the queue client 
	And I have the request data for a new authorization
	When I make the new authorization request to the endpoint
	And the create a claim request with repeated products
	And I request the create a claim endpoint
	And I request the check claim status endpoint
	And I request the get a claim endpoint
	When I request the complete transaction request with missing fields
	| TestId   | MissingField   |
	| <TestId> | <MissingField> |
	Then the result should be unprossesable entity

Scenarios: 
	| TestId | MissingField   |
	| 1      | ClaimId        |
	| 2      | Invoice        |
	| 3      | Amount         |
	| 4      | Currency       |
	| 5      | DocumentNumber |

Scenario: Successfully Complete a Transaction
	Given I have the provider claims client
	And I have the insurer authorizations client
	And I have the queue client 
	And I have the request data for a new authorization
	When I make the new authorization request to the endpoint
	And the create a claim request with repeated products
	And I request the create a claim endpoint
	And I request the check claim status endpoint
	And I request the get a claim endpoint
	When I request the complete transaction request
	Then the result should be ok

