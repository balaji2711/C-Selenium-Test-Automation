using CoreFramework.CommonUtils;

namespace CoreFramework.Interfaces
{
    public interface IWebActions
    {
        IWebActions AlertCancel();
        IWebActions AlertOk();
        IWebActions AlertText(string text);
        IWebActions Clear();
        IWebActions Click();
        IWebActions ClickElementUsingJavaScript();
        IWebActions DoubleClick();
        IWebActions Find(Locator locator);
        string GetAttribute(string type);
        string GetCurrentURL();
        string GetInnerText();
        int GetOpenWindowCount();

        int GetListOfItems(Locator locator);

        string GetText();
        IWebActions GoToURL(string url);
        IWebActions MouseHover();
        IWebActions MouseHoverClick();
        bool OpenNewTab(string url);
        IWebActions ScrollToBottomofThePage();
        IWebActions ScrollToElement();
        IWebActions ScrollToMiddleOfThePage();
        IWebActions ScrollToTopofThePage();
        IWebActions SelectDropDown(string type, string? text = null, int index = 0);
        IWebActions SwitchToDefaultContent();
        IWebActions SwitchToFrame();
        IWebActions SwitchToFrame(string frameName);
        IWebActions SwitchToNewWindow();
        IWebActions SwitchToParentFrame();
        IWebActions SwitchToWindowByTitle(string titleOfCurrentWindow);
        IWebActions Type(string value);
        void WaitForPageLoad();
        IWebActions WaitFor(int timeInSeconds);
        IWebActions Refresh();
        void CloseBrowser();
        IWebActions OpenBrowser(string browserName, string webBaseUrl, string headlessExecution);
    }
}
