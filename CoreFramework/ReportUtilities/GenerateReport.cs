using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using CoreFramework.Action;
using OpenQA.Selenium;
namespace CoreFramework.ReportUtilities
{
    public class GenerateReport
    {
        public static int totalCount = 0;
        public static int passCount = 0;
        public static int failCount = 0;
        [ThreadStatic]
        private static ExtentTest? featureName;
        [ThreadStatic]
        private static ExtentTest? scenario;
        private static ExtentReports? extent;
        public static string CurrentRunFolderName = string.Format("{0}-{1}-{2}-{3}-{4}-{5}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        public static string filePath = @"C:/" + Environment.MachineName + "/" + "AutomationReport_Timestamp_" + CurrentRunFolderName;
        static HashSet<string> failedFeatures = new HashSet<string>();

        [Obsolete]
        private static ExtentHtmlReporter? ExtentHtmlReporter;
        [ThreadStatic]
        public static string? logStep;

        [Obsolete]
        public static void InitializeTestSuite(string browserName, string productUrl, string author, string reportName)
        {
            ExtentHtmlReporter = new ExtentHtmlReporter(filePath + "\\index.html");
            ExtentHtmlReporter.Config.Theme = Theme.Dark;
            ExtentHtmlReporter.Config.DocumentTitle = "Automation Test Run Report";
            ExtentHtmlReporter.Config.ReportName = reportName;
            extent = new ExtentReports();
            extent.AttachReporter(ExtentHtmlReporter);
            extent.AddSystemInfo("Machine", Environment.MachineName);
            extent.AddSystemInfo("OS", Environment.OSVersion.VersionString);
            extent.AddSystemInfo("Browser Type", browserName.ToUpper());
            extent.AddSystemInfo("Web Application URL", productUrl);
            extent.AddSystemInfo("Author", author);
        }

        public static void EndTestSuite()
        {
            extent.Flush();          
        }

        public static void Before(ScenarioContext scenarioContext)
        {
            scenario = featureName.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);
            scenario.AssignCategory(scenarioContext.ScenarioInfo.Tags);
        }

        public static void BeforeFeature(FeatureContext featureContext)
        {
            featureName = extent.CreateTest<Feature>(featureContext.FeatureInfo.Title);
        }

        public static void AddFailedFeature(FeatureContext featureContext)
        {
            failedFeatures.Add("TestCategory=" + featureContext.FeatureInfo.Tags[0]);
        }

        [Obsolete]
        public static void AfterStep(ScenarioContext scenarioContext)
        {
            var stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            if (scenarioContext.TestError == null)
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text.ToString());
                else if (stepType == "When")
                    scenario.CreateNode<When>(scenarioContext.StepContext.StepInfo.Text.ToString());
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text + Environment.NewLine + logStep);
                else if (stepType == "And")
                    scenario.CreateNode<And>(scenarioContext.StepContext.StepInfo.Text.ToString());
            }
            else if (scenarioContext.TestError != null)
            {
                var screenshot = SeleniumActions.CaptureScreenShot(scenarioContext.ScenarioInfo.Title.Trim());
                if (stepType == "Given")
                    scenario.CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text.ToString()).Fail(scenarioContext.TestError.Message, screenshot);
                else if (stepType == "When")
                    scenario.CreateNode<When>(scenarioContext.StepContext.StepInfo.Text.ToString()).Fail(scenarioContext.TestError.Message, screenshot);
                else if (stepType == "And")
                    scenario.CreateNode<When>(scenarioContext.StepContext.StepInfo.Text.ToString()).Fail(scenarioContext.TestError.Message, screenshot);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text.ToString()).Fail(scenarioContext.TestError.Message, screenshot);
            }
        }

        public static MediaEntityModelProvider CaptureScreenShot(string name, IWebDriver driver)
        {
            var screenshot = ((ITakesScreenshot)driver).GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, name).Build();
        }

        public static void LogReport(Status status, string message)
        {
            scenario.Log(status, message);
        }
    }
}
