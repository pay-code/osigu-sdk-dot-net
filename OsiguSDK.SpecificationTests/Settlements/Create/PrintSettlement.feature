Feature: Print Settlement
	In order to print a settlement
	I want to validate the file created
	And the settlement status

Scenario: Print a Settlement
	Given I have entered 50 into the calculator
	And I have entered 70 into the calculator
	When I press add
	Then the result should be 120 on the screen
