﻿using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium;

namespace CoreFramework.ClientFactory
{
    public class DriverFactory
    {
        [ThreadStatic]
        protected static IWebDriver? driver;

        public void OpenBrowser(string browserName, string webBaseUrl, string headlessExecution)
        {
            switch (browserName)
            {
                case "chrome":
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument("--start-maximized");
                    chromeOptions.AddArgument("--disable-notifications");
                    chromeOptions.AddArguments("--ignore-certificate-errors");
                    if (headlessExecution == "Yes")
                        chromeOptions.AddArguments("--headless");
                    driver = new ChromeDriver(chromeOptions);
                    driver.Manage().Cookies.DeleteAllCookies();
                    break;

                case "firefox":
                    var firefoxOptions = new FirefoxOptions();
                    if (headlessExecution == "Yes")
                        firefoxOptions.AddArguments("--headless");
                    driver = new FirefoxDriver(firefoxOptions);
                    driver.Manage().Cookies.DeleteAllCookies();
                    driver.Manage().Window.Maximize();
                    break;

                case "edge":
                    driver = new EdgeDriver();
                    driver.Manage().Cookies.DeleteAllCookies();
                    driver.Manage().Window.Maximize();
                    break;

                case "ie":
                    var ieOptions = new InternetExplorerOptions();
                    ieOptions.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                    ieOptions.IgnoreZoomLevel = true;
                    driver = new InternetExplorerDriver(ieOptions);
                    driver.Manage().Cookies.DeleteAllCookies();
                    driver.Manage().Window.Maximize();
                    break;

                default:
                    throw new ArgumentException($"Browser not yet implemented - {browserName}");
            }
            driver.Navigate().GoToUrl(webBaseUrl);
        }

        public void QuitBrowser()
        {
            if (driver != null)
                driver.Quit();
        }
    }
}
