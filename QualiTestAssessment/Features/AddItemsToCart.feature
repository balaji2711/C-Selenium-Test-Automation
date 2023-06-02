Feature: AddItemsToCart

	Add Items to Cart

@Regression
Scenario: Add Items to Cart

	Given I add four random items to my cart
	When I view my cart
	Then I find total four items listed in my cart
	When I search for lowest price item
	And I am able to remove the lowest price item from my cart
	Then I am able to verify three items in my cart