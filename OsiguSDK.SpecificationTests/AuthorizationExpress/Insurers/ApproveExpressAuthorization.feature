Feature: Approve an express authorization as an Insurer
	In order to approve an express authorization
	As an Insurer 
	I need to send the valid id of the express authorization

Scenario: Approve an express authorization with an invalid token as an insurer
	Given I have the insurer express authorizations client with an invalid token
	When I make the approve express authorization request to the endpoint as an insurer
	Then the result should be forbidden for approving the express authorization

Scenario: Approve an express authorization with an invalid slug as an insurer
	Given I have the insurer express authorizations client with an invalid slug
	When I make the approve express authorization request to the endpoint as an insurer
	Then the result should be not found for approving the express authorization

Scenario: Approve an invalid express authorization as an insurer
	Given I have the insurer express authorizations client
	And I have an invalid express authorization id
	When I make the approve express authorization request to the endpoint as an insurer
	Then the result should be not found for approving the express authorization
#
#Scenario: Approve a rejected express authorization as an insurer
#	#Scenario for creating an express authorization
#	#Scenario for rejection the express authorization
#	And I have the insurer express authorizations client
#	When I make the approve express authorization request to the endpoint as an insurer
#	Then the result should be not procesable for approving the express authorization

#Scenario: Approve an approved express authorization as an insurer
#	#Scenario for creating an express authorization
#	#Scenario for rejection the express authorization
#	Given I have the insurer express authorizations client
#	When I make the approve express authorization request to the endpoint as an insurer
#	Then the result should be ok for approving the express authorization
#	And I have the insurer express authorizations client
#	When I make the approve express authorization request to the endpoint as an insurer
#	Then the result should be not procesable for approving the express authorization

Scenario: Approve a rejected express authorization as an insurer
	Given I create a valid express authorization
	And I have the insurer express authorizations client
	When I make the approve express authorization request to the endpoint as an insurer
	Then the result should be ok for approving the express authorization