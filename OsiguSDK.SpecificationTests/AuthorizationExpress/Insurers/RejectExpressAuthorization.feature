Feature: Reject an express authorization as an Insurer
	In order to reject an express authorization
	As an Insurer 
	I need to send the valid id of the express authorization

Scenario: Reject an express authorization with an invalid token as an insurer
	Given I have the insurer express authorizations client with an invalid token
	When I make the reject express authorization request to the endpoint as an insurer
	Then the result should be forbidden for rejecting the express authorization

Scenario: Reject an express authorization with an invalid slug as an insurer
	Given I have the insurer express authorizations client with an invalid slug
	When I make the reject express authorization request to the endpoint as an insurer
	Then the result should be not found for rejecting the express authorization

Scenario: Reject an invalid express authorization as an insurer
	Given I have the insurer express authorizations client
	And I have an invalid express authorization id
	When I make the reject express authorization request to the endpoint as an insurer
	Then the result should be not found for rejecting the express authorization

#Scenario: Reject a rejected express authorization as an insurer
##	Steps create express authorization and complete
#	And I have the insurer express authorizations client
#	When I make the reject express authorization request to the endpoint as an insurer
#	Then the result should be ok for rejecting the express authorization
#	And I have the insurer express authorizations client
#	When I make the reject express authorization request to the endpoint as an insurer
#	Then the result should be unprocesable for rejecting the express authorization
#
Scenario: Reject a valid express authorization as an insurer
#	Steps create express authorization and complete
	And I have the insurer express authorizations client
	When I make the reject express authorization request to the endpoint as an insurer
	Then the result should be ok for rejecting the express authorization