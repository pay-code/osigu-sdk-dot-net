Feature: Get list of express authorizations as an Insurer
	In order to obtain the list of express authorizations
	As an Insurer 
	I need to send a valid request and a valid slug

Scenario: Get list of express authorizations with an invalid token as an insurer
	Given I have the insurer express authorizations client with an invalid token
	When I make the get list of express authorizations request to the endpoint as an insurer
	Then the result should be forbidden for getting the list of express authorizations as an insurer

Scenario: Get list of express authorizations with an invalid slug as an insurer
	Given I have the insurer express authorizations client with an invalid slug
	When I make the get list of express authorizations request to the endpoint as an insurer
	Then the result should be not found for getting the list of express authorizations as an insurer

#Scenario: Get list of express authorizations with an invalid parameters as an insurer
#	Given I have the insurer express authorizations client with an invalid slug
#	And I have an invalid authorization status
#	When I make the get express authorization request to the endpoint as an insurer
#	Then the result should be not found for getting the express authorization

Scenario Outline: Get list of express authorizations with valid status
	Given I have the insurer express authorizations client
	And I request the authorizations status '<AuthorizationStatus>' and id <TestId>
	When I make the get list of express authorizations request to the endpoint as an insurer
	Then the result should be the list of express authorizations

Scenarios:
	| TestId | AuthorizationStatus    |
	| 1      | insurer_pending_review |
	| 2      | insurer_approved       |
	| 3      | insurer_rejected       |
	| 4      | pending_paid           |
	| 5      | paid                   |