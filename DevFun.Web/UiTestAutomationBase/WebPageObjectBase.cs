using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace UiTestAutomationBase
{
    public class WebPageObjectBase : PageObjectBase
    {
        protected WebPageObjectBase(ISearchContext searchContext) : base(searchContext)
        {
        }

        protected WebPageObjectBase(PageObjectBase parent) : base(parent)
        {

        }

        protected WebPageObjectBase(PageObjectBase parent, ISearchContext searchContext) : base(parent, searchContext)
        {
        }

        public string CurrenUrl => RootPageObject?.Browser.Url;
        protected BrowserPageObject RootPageObject => GetBrowserPageObject(this);
        protected virtual IWebDriver Browser => RootPageObject.Browser;
        protected IJavaScriptExecutor JsExecutor => Browser as IJavaScriptExecutor;

        #region public methods
        public override TControl FindById<TControl>(string id, bool doWait = true)
        {
            var by = By.Id(id);
            if (doWait)
            {
                var wait = new WebDriverWait(GetBrowserPageObject(this).Browser, TimeSpan.FromMilliseconds(10000));
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(by));
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(by));
            }
            return this.SearchContext.FindElement(by) as TControl;
        }

        public IWebElement FindByName(string name, bool doWait = true)
        {
            var by = By.Name(name);
            if (doWait)
            {
                var wait = new WebDriverWait(GetBrowserPageObject(this).Browser, TimeSpan.FromMilliseconds(10000));
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(by));
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(by));
            }
            return this.SearchContext.FindElement(by);
        }

        public IEnumerable<IWebElement> FindManyByName(string name)
        {
            return this.SearchContext.FindElements(By.Name(name));
        }

        public IWebElement FindByCssSelector(string cssSelector, bool doWait = true)
        {
            var by = By.CssSelector(cssSelector);
            if (doWait)
            {
                var wait = new WebDriverWait(GetBrowserPageObject(this).Browser, TimeSpan.FromMilliseconds(10000));
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(by));
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(by));
            }
            return this.SearchContext.FindElement(by);
        }

        public IEnumerable<IWebElement> FindManyByCssSelector(string cssSelector)
        {
            return this.SearchContext.FindElements(By.CssSelector(cssSelector));
        }

        public IWebElement FindByXPath(string xPath, bool doWait = true)
        {
            var by = By.XPath(xPath);
            if (doWait)
            {
                var wait = new WebDriverWait(GetBrowserPageObject(this).Browser, TimeSpan.FromMilliseconds(10000));
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(by));
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(by));
            }
            return this.SearchContext.FindElement(by);
        }

        public IEnumerable<IWebElement> FindManyByXPath(string xPath)
        {
            return this.SearchContext.FindElements(By.XPath(xPath));
        }

        public override Screenshot TakeScreenshot()
        {
            return ((ITakesScreenshot)Browser).GetScreenshot();
        }

        #endregion

        #region private methods

        private BrowserPageObject GetBrowserPageObject(PageObjectBase pageObject)
        {
            if (pageObject == null)
            {
                return null;
            }

            var browserPageObject = pageObject as BrowserPageObject;
            if (browserPageObject != null)
            {
                return browserPageObject;
            }
            else
            {
                return GetBrowserPageObject(pageObject.Parent);
            }
        }

        #endregion

    }
}
