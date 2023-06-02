using CoreFramework.Action;
using CoreFramework.CommonUtils;
using CoreFramework.Interfaces;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualiTestAssessment.Pages
{
    public class AddItemsToCartPage
    {
        IWebActions iWebActions;        
        Locator viewCart = new(LocatorType.XPath, "(//a[text()='View cart'])[1]");
        Locator totalNumberOfItems = new(LocatorType.XPath, "//table[contains(@class,'cart')]/tbody/tr[contains(@class,'woo')]");
        Locator image = new(LocatorType.XPath, "//div/img");
        List<String> priceList = new List<string>();

        public AddItemsToCartPage(IWebActions iWebActions)
        {
            this.iWebActions = iWebActions;
        }

        public void AddRandomFourItemsToCart()
        {
            Random random = new Random();
            HashSet<int> randomNumber = new HashSet<int>();
            int maxLen = 4;
            for (int i = 1; i <= maxLen; i++)
            {
                int item = random.Next(1, 10);
                if(!randomNumber.Contains(item))
                {
                    Locator listOfProducts = new(LocatorType.XPath, "(//ul[@class='products columns-3']/li/div/a[text()='Add to cart'])[" + item + "]");
                    iWebActions.Find(listOfProducts).MouseHover();
                    iWebActions.Find(listOfProducts).ClickElementUsingJavaScript();
                    Thread.Sleep(2000);
                }
                else
                    maxLen++;
                randomNumber.Add(item);
            }
        }

        public void ViewCart()
        {
            iWebActions.Find(viewCart).ScrollToTopofThePage();
            iWebActions.Find(viewCart).ClickElementUsingJavaScript();
        }

        public void FindTotalNumberOfItemsInCart(int expectedCount)
        {
            int actualCount = 0;            
            actualCount = iWebActions.GetListOfItems(totalNumberOfItems);
            Assert.Multiple(() =>
            {
                Assert.NotZero(actualCount, "No items in the cart\n");
                Assert.AreEqual(expectedCount, actualCount, "Number of items in the cart is not mataching");
            });
        }

        public void FindLowestPriceItemfromCart()
        {
            for (int i = 1; i <= 4; i++)
            {
                Locator itemPrice = new(LocatorType.XPath, "//table[contains(@class,'cart')]/tbody/tr[" + i + "]/td[4]");
                priceList.Add(iWebActions.Find(itemPrice).GetText());
                priceList = priceList.Select(item => item.Replace("$", "")).OrderBy(item => item).ToList();
            }
        }

        public void RemoveLowestPriceItemfromCart()
        {
            Locator removeitem = new(LocatorType.XPath, "(//span[text()='"+ priceList[0] + "']/parent::td/preceding-sibling::td[3]/a)[1]");
            iWebActions.Find(removeitem).MouseHover();
            iWebActions.Find(removeitem).ClickElementUsingJavaScript();
            Thread.Sleep(5000);
        }
    }
}
