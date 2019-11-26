using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Safari;

namespace UiTestAutomationBase
{
    public abstract class BrowserPageObject : WebPageObjectBase
    {
        protected BrowserPageObject(ISearchContext parentSearchContext) : base(parentSearchContext)
        {
        }

        protected override IWebDriver Browser => this.SearchContext as IWebDriver;

        public void Close()
        {
            Browser.Quit();
        }

        protected static IWebDriver LaunchWebDriver(string url, TargetBrowser targetBrowser = TargetBrowser.InternetExplorer, int width = 1024, int height = 768)
        {
            IWebDriver browser = null;

            switch (targetBrowser)
            {
                case TargetBrowser.InternetExplorer:
                    var ieOptions = new InternetExplorerOptions
                    {
                        IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                        EnsureCleanSession = true
                    };
                    browser = new InternetExplorerDriver(ieOptions);
                    break;

                case TargetBrowser.Edge:
                    var edgeOptions = new EdgeOptions
                    {
                        PageLoadStrategy = EdgePageLoadStrategy.Eager
                    };
                    browser = new EdgeDriver(edgeOptions);
                    break;

                case TargetBrowser.Chrome:
                    browser = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                    break;

                case TargetBrowser.Firefox:
                    browser = new FirefoxDriver();
                    break;

                case TargetBrowser.Safari:
                    browser = new SafariDriver();
                    break;

                case TargetBrowser.PhantomJSDriver:
                    browser = new PhantomJSDriver();
                    break;

                default:
                    throw new ArgumentException($"Unknown target browser type {targetBrowser}");
            }

            browser.Manage().Window.Size = new Size(width, height);
            browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            browser.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            browser.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(30);

            browser.Navigate().GoToUrl(url);

            return browser;
        }
    }
}