using System;
using OpenQA.Selenium;
using UiTestAutomationBase;

namespace DevFun.Web.PageObjects
{
    public class DevFunAppPageObject : BrowserPageObject
    {
        protected Uri BaseUri { get; private set; }
        protected new IWebDriver Browser { get; private set; }

        public DevFunAppPageObject(ISearchContext parentSearchContext) : base(parentSearchContext)
        {
        }

        public RandomJokePanelPageObject NavigateToRandomJokes()
        {
            return new RandomJokePanelPageObject(this, this.SearchContext);
        }

        public static DevFunAppPageObject Launch(string url, TargetBrowser targetBrowser = TargetBrowser.InternetExplorer, int width = 1024, int height = 768)
        {
            var browser = LaunchWebDriver(url, targetBrowser, width, height);
            var app = new DevFunAppPageObject(browser)
            {
                BaseUri = new Uri(url),
                Browser = browser
            };
            return app;
        }
    }
}