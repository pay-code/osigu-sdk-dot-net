Feature: Change Claim Items
	In order to change the items of a claim
	As Provider
	I want to be able to send a claim to be updated


Scenario: Authentication Error
	Given I have the provider claims client
	And I have the insurer authorizations client
	And I have the queue client 
	And I have the request data for a new authorization
	When I make the new authorization request to the endpoint
	And the create a claim request
	And I request the create a claim endpoint
	And I delay the check status request
	And I request the check claim status endpoint
	And I request the get a claim endpoint
	Given I have the provider claims client without authorization
	When I request the change items request
	Then the result should be unauthorized

Scenario: Slug Does Not Exists
	Given I have the provider claims client
	And I have the insurer authorizations client
	And I have the queue client 
	And I have the request data for a new authorization
	When I make the new authorization request to the endpoint
	And the create a claim request
	And I request the create a claim endpoint
	And I delay the check status request
	And I request the check claim status endpoint
	And I request the get a claim endpoint
	Given I have the provider claims client without valid slug
	When I request the change items request
	Then the result should be no permission

Scenario: Invalid Claim Id
	Given I have the provider claims client
	And I have the insurer authorizations client
	And I have the queue client
	And I have the request data for a new authorization
	When I make the new authorization request to the endpoint
	And the create a claim request with repeated products
	And I request the create a claim endpoint
	And I delay the check status request
	And I request the check claim status endpoint
	And I request the get a claim endpoint
	Given I have the provider claims client
	When I request the change items request with an invalid claim id
	Then the result should be not existing 

Scenario: Invalid PIN 
	Given I have the provider claims client
	And I have the insurer authorizations client
	And I have the queue client
	And I have the request data for a new authorization
	When I make the new authorization request to the endpoint
	And the create a claim request with repeated products
	And I request the create a claim endpoint
	And I request the check claim status endpoint
	And I delay the check status request
	And I request the get a claim endpoint
	Given I have the provider claims client
	When I request the change items request with an invalid PIN
	Then the result should be not existing 

Scenario Outline: Missing Fields
	Given I have the provider claims client
	And I have the insurer authorizations client
	And I have the queue client 
	And I have the request data for a new authorization
	When I make the new authorization request to the endpoint
	And the create a claim request
	And I request the create a claim endpoint
	And I delay the check status request
	And I request the check claim status endpoint
	And I request the get a claim endpoint
	And I request the change items request with missing fields
	|TestId|MissingField|
	|<TestId>|<MissingField>|
	Then the result should be unprocessable fot that request

Scenarios: 
| TestId | MissingField  |
| 1      | ClaimId       |
| 2      | PIN           |
| 2      | Empty Items   |
| 3      | Empty Items 2 |
| 3      | Product Id    |
| 4      | Price         |
| 5      | Quantity      |

Scenario: Product Provider Not Existing
	Given I have the provider claims client
	And I have the insurer authorizations client
	And I have the queue client 
	And I have the request data for a new authorization
	When I make the new authorization request to the endpoint
	And the create a claim request
	And I request the create a claim endpoint
	And I delay the check status request
	And I request the check claim status endpoint
	And I request the get a claim endpoint
	And I request the change items request with unexisting osigu product
	Then the result should be unprocessable fot that request

Scenario: Successfully change items
	Given I have the provider claims client
	And I have the insurer authorizations client
	And I have the queue client 
	And I have the request data for a new authorization
	When I make the new authorization request to the endpoint
	And the create a claim request
	And I request the create a claim endpoint
	And I delay the check status request
	And I request the check claim status endpoint
	And I request the get a claim endpoint
	And I request the change items request
	Then the result should be ok




