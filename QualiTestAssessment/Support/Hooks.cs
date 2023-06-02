using CoreFramework.Action;
using CoreFramework.CommonUtils;
using CoreFramework.Interfaces;
using CoreFramework.ReportUtilities;

namespace QualiTestAssessment.Support
{
    [Binding]
    public class Hooks
    {
        IWebActions? iWebActions;
        static string? browserName = null;
        static string? webBaseUrl = null;
        static string? headlessMode = null;
        static string? author = null;
        static string? reportName = null;

        [BeforeTestRun]
        [Obsolete]
        public static void InitializeTestSuite()
        {
            ConfigManager.InitializeEnvConfig();
            browserName = Environment.GetEnvironmentVariable("Browser") ?? ConfigManager.config.testConfig.browser;
            webBaseUrl = Environment.GetEnvironmentVariable("WebBaseUrl") ?? ConfigManager.config.appConfig.webBaseURL;
            headlessMode = Environment.GetEnvironmentVariable("HeadlessMode") ?? ConfigManager.config.testConfig.headlessMode;                     
            author = Environment.GetEnvironmentVariable("Author") ?? ConfigManager.config.testConfig.author;
            reportName = Environment.GetEnvironmentVariable("Report") ?? ConfigManager.config.testConfig.reportName;
            GenerateReport.InitializeTestSuite(browserName, webBaseUrl, author, reportName);
        }

        [AfterTestRun]
        public static void EndTestSuite()
        {
            GenerateReport.EndTestSuite();
        }

        //If you choose to have a single browser instance for all tests,
        //you can replace these attributes with[BeforeTestRun] and[AfterTestRun]

        [Before]
        [Obsolete]
        public void InitializeDrivers(ScenarioContext scenarioContext)
        {
            GenerateReport.logStep = null;
            GenerateReport.totalCount++;
            iWebActions = new SeleniumActions();
            iWebActions.OpenBrowser(browserName, webBaseUrl, headlessMode);
            GenerateReport.Before(scenarioContext);
            scenarioContext["iWebActions"] = iWebActions;
        }

        [After]
        [Obsolete]
        public void CloseDrivers(ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            if (scenarioContext.TestError == null)
                GenerateReport.passCount++;
            else if (scenarioContext.TestError != null)
            {
                GenerateReport.failCount++;
                GenerateReport.AddFailedFeature(featureContext);
            }
            iWebActions.CloseBrowser();
        }

        [BeforeFeature]
        [Obsolete]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            GenerateReport.BeforeFeature(featureContext);
        }

        [AfterStep]
        [Obsolete]
        public void CreateReportingSteps(ScenarioContext scenarioContext)
        {
            GenerateReport.AfterStep(scenarioContext);
        }
    }
}
