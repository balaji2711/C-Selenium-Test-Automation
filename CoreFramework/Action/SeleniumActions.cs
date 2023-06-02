using AventStack.ExtentReports;
using CoreFramework.ClientFactory;
using CoreFramework.CommonUtils;
using CoreFramework.Interfaces;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;
using System.Drawing;
using System.Net;

namespace CoreFramework.Action
{
    public enum LocatorType
    {
        Id,
        Name,
        XPath,
        LinkText,
        TagName,
        CssSelector,
        ClassName,
        PartialLinkText
    }

    public class SeleniumActions : DriverFactory, IWebActions
    {
        private IWebElement _element;
        IJavaScriptExecutor? javaScriptExecutor;

        public IWebActions Find(Locator locator)
        {
            _element = null;
            _element = SwitchLocator(locator);
            if (_element == null)
                Assert.Fail("Can't find locator - " + locator.Value);
            return this;
        }

        private IWebElement SwitchLocator(Locator locator)
        {
            IWebElement element = null;
            switch (locator.Type)
            {
                case LocatorType.Id:
                    element = FindElement(By.Id(locator.Value));
                    break;

                case LocatorType.Name:
                    element = FindElement(By.Name(locator.Value));
                    break;

                case LocatorType.XPath:
                    element = FindElement(By.XPath(locator.Value));
                    break;

                case LocatorType.LinkText:
                    element = FindElement(By.LinkText(locator.Value));
                    break;

                case LocatorType.TagName:
                    element = FindElement(By.TagName(locator.Value));
                    break;

                case LocatorType.CssSelector:
                    element = FindElement(By.CssSelector(locator.Value));
                    break;

                case LocatorType.ClassName:
                    element = FindElement(By.ClassName(locator.Value));
                    break;

                case LocatorType.PartialLinkText:
                    element = FindElement(By.PartialLinkText(locator.Value));
                    break;

                default:
                    Assert.Fail($"Locator Type not yet implemented - {locator.Type}");
                    break;
            }
            return element;
        }

        private IWebElement FindElement(By by)
        {
            IWebElement element = null;
            try
            {
                element = driver.FindElement(by);
            }
            catch (Exception) { }
            return element;
        }

        public IWebActions Clear()
        {
            try
            {
                _element.Clear();
            }
            catch (Exception ex)
            {
                Assert.Fail("Failed: Unable to clear a text box - " + ex.Message);
            }
            return this;
        }

        public IWebActions Type(string value)
        {
            try
            {
                _element.SendKeys(value);
            }
            catch (Exception ex)
            {
                Assert.Fail("Failed: Unable to enter text in the text box - " + ex.Message);
            }
            return this;
        }

        public IWebActions Click()
        {
            try
            {
                _element.Click();
            }
            catch (Exception ex)
            {
                Assert.Fail("Failed: Cannot click on the specified element - " + ex.Message);
            }
            return this;
        }

        public IWebActions SelectDropDown(string type, string? text = null, int index = 0)
        {
            try
            {
                SelectElement selectElement = new SelectElement(_element);
                if (type == "text")
                    selectElement.SelectByText(text);
                else if (type == "index")
                    selectElement.SelectByIndex(index);
                else
                    selectElement.SelectByValue(text);
            }
            catch (Exception ex)
            {
                Assert.Fail("Failed: Dropdown value selection - " + ex.Message);
            }
            return this;
        }

        public IWebActions AlertOk()
        {
            try
            {
                IAlert alertOk = driver.SwitchTo().Alert();
                alertOk.Accept();
            }
            catch (Exception ex)
            {
                Assert.Fail("Failed: Unable to click Ok on alert - " + ex.Message);
            }
            return this;
        }

        public IWebActions AlertCancel()
        {
            try
            {
                IAlert alertCancel = driver.SwitchTo().Alert();
                alertCancel.Dismiss();
            }
            catch (Exception ex)
            {
                Assert.Fail("Failed: Unable to click Ok on alert - " + ex.Message);
            }
            return this;
        }

        public IWebActions AlertText(string text)
        {
            try
            {
                IAlert alertText = driver.SwitchTo().Alert();
                alertText.SendKeys(text);
                alertText.Accept();
            }
            catch (Exception ex)
            {
                Assert.Fail("Failed: Unable to click Ok on alert - " + ex.Message);
            }
            return this;
        }

        public IWebActions SwitchToFrame()
        {
            try
            {
                driver.SwitchTo().Frame(_element);
            }
            catch (Exception ex)
            {
                Assert.Fail("Failed: Unable to switch to the frame - " + ex.Message);
            }
            return this;
        }

        public IWebActions SwitchToFrame(string frameName)
        {
            try
            {
                driver.SwitchTo().Frame(frameName);
            }
            catch (Exception ex)
            {
                Assert.Fail("Failed: Unable to switch to the frame by framename - " + ex.Message);
            }
            return this;
        }

        public IWebActions SwitchToParentFrame()
        {
            try
            {
                driver.SwitchTo().ParentFrame();
            }
            catch (Exception ex)
            {
                Assert.Fail("Failed: Unable to switch to parent frame - " + ex.Message);
            }
            return this;
        }

        public IWebActions SwitchToDefaultContent()
        {
            try
            {
                driver.SwitchTo().DefaultContent();
            }
            catch (Exception ex)
            {
                Assert.Fail("Failed: Unable to switch to main content of page - " + ex.Message);
            }
            return this;
        }

        public string GetText()
        {
            string text = null;
            try
            {
                text = _element.Text;
            }
            catch (Exception ex)
            {
                Assert.Fail("Failed: Unable to get the text from web element - " + ex.Message.ToString());
            }
            return text;
        }        

        public string GetInnerText()
        {
            string text = null;
            try
            {
                text = (string)((IJavaScriptExecutor)driver).ExecuteScript("return arguments[0].innerHTML;", _element);
                text = text.Replace(System.Environment.NewLine, string.Empty);
            }
            catch (Exception ex)
            {
                Assert.Fail("Failed: Unable to get the inner HTML from web element - " + ex.Message.ToString());
            }
            return text;
        }

        public int GetOpenWindowCount()
        {
            int count = 0;
            try
            {
                count = driver.WindowHandles.Count;
            }
            catch (Exception ex)
            {
                Assert.Fail("Failed: Unable to get a count of opened window - " + ex.Message.ToString());
            }
            return count;
        }

        public bool OpenNewTab(string url)
        {
            bool flag = false;
            try
            {
                javaScriptExecutor = (IJavaScriptExecutor)driver;
                javaScriptExecutor.ExecuteScript("window.open();");
                int count = GetOpenWindowCount();
                if (count >= 1)
                    this.SwitchToNewWindow();
                string script = "window.location =\'" + url + "\'";
                javaScriptExecutor.ExecuteScript(script);
                flag = true;
            }
            catch (Exception ex)
            {
                Assert.Fail("Failed: Unable to open a new tab - " + ex.Message.ToString());
            }
            return flag;
        }

        public IWebActions GoToURL(string url)
        {
            try
            {
                driver.Navigate().GoToUrl(url);
                WaitFor(2);
            }
            catch (Exception ex)
            {
                Assert.Fail("Failed: Unable to navigate to URL - " + ex.Message.ToString());
            }
            return this;
        }

        public string GetCurrentURL()
        {
            string url = null;
            try
            {
                url = driver.Url;
            }
            catch (Exception ex)
            {
                Assert.Fail("Failed: Unable to navigate to URL - " + ex.Message.ToString());
            }
            return url;
        }

        public IWebActions SwitchToWindowByTitle(string titleOfCurrentWindow)
        {
            try
            {
                List<string> windows = driver.WindowHandles.ToList();
                foreach (var window in windows)
                {
                    driver.SwitchTo().Window(window);
                    if (driver.Title == titleOfCurrentWindow)
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Assert.Fail("Failed: Cannot switch to window by title - " + ex.Message);
            }
            return this;
        }

        public IWebActions SwitchToNewWindow()
        {
            try
            {
                string currentWindowHandler = driver.CurrentWindowHandle;
                WaitFor(2);
                List<string> windows = driver.WindowHandles.ToList<string>();
                windows.ForEach(t =>
                {
                    if (!t.Equals(currentWindowHandler))
                    {
                        driver.SwitchTo().Window(t);
                        string s = driver.Title;
                        driver.Manage().Window.Maximize();
                    }
                });
            }
            catch (Exception ex)
            {
                Assert.Fail("Failed: Unable to switch to new window - " + ex.Message.ToString());
            }
            return this;
        }

        public IWebActions WaitFor(int timeInSeconds)
        {
            Thread.Sleep(timeInSeconds * 1000);
            return this;
        }

        public IWebActions ClickElementUsingJavaScript()
        {
            try
            {
                javaScriptExecutor = (IJavaScriptExecutor)driver;
                javaScriptExecutor.ExecuteScript("arguments[0].click();", _element);
            }
            catch (Exception ex)
            {
                Assert.Fail("Failed: Cannot click on the specified element - " + ex.Message);
            }
            return this;
        }

        public IWebActions DoubleClick()
        {
            try
            {
                Actions doubleClick = new Actions(driver);
                doubleClick.DoubleClick(_element).Build().Perform();
            }
            catch (Exception ex)
            {
                Assert.Fail("Failed: Cannot do the double click on the specified element - " + ex.Message);
            }
            return this;
        }

        public IWebActions MouseHover()
        {
            try
            {
                Actions mouseHover = new Actions(driver);
                mouseHover.MoveToElement(_element).Perform();
            }
            catch (Exception ex)
            {
                Assert.Fail("Failed: Cannot mouse hover on element - " + ex.Message);
            }
            return this;
        }

        public IWebActions MouseHoverClick()
        {
            try
            {
                Actions moveToElement = new Actions(driver);
                moveToElement.MoveToElement(_element).Click().Build().Perform();
            }
            catch (Exception ex)
            {
                Assert.Fail("Failed: Cannot move to the element - " + ex.Message);
            }
            return this;
        }

        public IWebActions ScrollToTopofThePage()
        {
            try
            {
                javaScriptExecutor = (IJavaScriptExecutor)driver;
                javaScriptExecutor.ExecuteScript("window.scrollTo(0, 0);");
                this.WaitFor(2);
            }
            catch (Exception ex)
            {
                Assert.Fail("Failed: Cannot scroll to top of the page - " + ex.Message);
            }
            return this;
        }

        public IWebActions ScrollToBottomofThePage()
        {
            try
            {
                javaScriptExecutor = (IJavaScriptExecutor)driver;
                javaScriptExecutor.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
            }
            catch (Exception ex)
            {
                Assert.Fail("Failed: Cannot scroll to bottom of the page - " + ex.Message);
            }
            return this;
        }

        public IWebActions ScrollToMiddleOfThePage()
        {
            try
            {
                javaScriptExecutor = (IJavaScriptExecutor)driver;
                javaScriptExecutor.ExecuteScript("window.scrollTo(0, document.body.scrollHeight/2);");
            }
            catch (Exception ex)
            {
                Assert.Fail("Failed: Cannot scroll to middle of the page - " + ex.Message);
            }
            return this;
        }

        public IWebActions ScrollToElement()
        {
            try
            {
                javaScriptExecutor = (IJavaScriptExecutor)driver;
                javaScriptExecutor.ExecuteScript("arguments[0].scrollIntoView();", _element);
            }
            catch (Exception ex)
            {
                Assert.Fail("Failed: Cannot scroll to the element - " + ex.Message);
            }
            return this;
        }


        public void WaitForPageLoad()
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(60)).Until(
            d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        }


        public int GetListOfItems(Locator locator)
        {
            int count = 0;
            count = driver.FindElements(By.XPath(locator.Value)).Count;
            return count;
        }

        public string GetAttribute(string type)
        {
            string? text = null;
            try
            {
                text = _element.GetAttribute(type);
                return text;
            }
            catch (Exception ex)
            {
                Assert.Fail("Unable to get the attribute for " + type + " from element - " + ex.Message.ToString());
            }
            return text;
        }

        public static MediaEntityModelProvider CaptureScreenShot(string name)
        {
            var screenshot = ((ITakesScreenshot)driver).GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, name).Build();
        }

        public IWebActions Refresh()
        {
            driver.Navigate().Refresh();
            return this;
        }

        public void CloseBrowser()
        {
            base.QuitBrowser();
        }

        public IWebActions OpenBrowser(string browserName, string webBaseUrl, string headlessExecution)
        {
            base.OpenBrowser(browserName, webBaseUrl, headlessExecution);
            return this;
        }
    }
}
