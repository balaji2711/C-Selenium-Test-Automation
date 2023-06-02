using CoreFramework.Interfaces;
using QualiTestAssessment.Pages;

namespace QualiTestAssessment.StepDefinitions
{
    [Binding]
    public class AddItemsToCartSteps
    {
        AddItemsToCartPage addItemsToCartPage;
        IWebActions iWebActions;

        public AddItemsToCartSteps(ScenarioContext scenarioContext)
        {
            iWebActions = scenarioContext["iWebActions"] as IWebActions;
            addItemsToCartPage = new AddItemsToCartPage(iWebActions);
        }

        [Given(@"I add four random items to my cart")]
        public void GivenIAddFourRandomItemsToMyCart()
        {
            addItemsToCartPage.AddRandomFourItemsToCart();
        }

        [When(@"I view my cart")]
        public void WhenIViewMyCart()
        {
            addItemsToCartPage.ViewCart();
        }

        [Then(@"I find total four items listed in my cart")]
        public void ThenIFindTotalFourItemsListedInMyCart()
        {
            addItemsToCartPage.FindTotalNumberOfItemsInCart(4);
        }

        [When(@"I search for lowest price item")]
        public void WhenISearchForLowestPriceItem()
        {
            addItemsToCartPage.FindLowestPriceItemfromCart();
        }

        [When(@"I am able to remove the lowest price item from my cart")]
        public void WhenIAmAbleToRemoveTheLowestPriceItemFromMyCart()
        {
            addItemsToCartPage.RemoveLowestPriceItemfromCart();
        }

        [Then(@"I am able to verify three items in my cart")]
        public void ThenIAmAbleToVerifyThreeItemsInMyCart()
        {
            addItemsToCartPage.FindTotalNumberOfItemsInCart(3);
        }
    }
}
