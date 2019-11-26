using System;
using DevFun.Web.PageObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UiTestAutomationBase;

namespace DevFun.Web.Ui.Tests
{
    public class WebUITestBase
    {
        public TestContext TestContext { get; set; }

        public string BaseUrl { get; private set; }

        public DevFunAppPageObject CurrentApplication { get; private set; }

        [TestCleanup]
        public void TeardownTest()
        {
            CloseCurrentApplication();
        }

        public DevFunAppPageObject Launch(string url = null, TargetBrowser targetBrowser = TargetBrowser.Undefined, int width = 0, int height = 0)
        {
            if (url == null)
            {
                url = TestContext?.Properties["baseUrl"]?.ToString();
                if (url == null)
                {
                    throw new ArgumentNullException("Invalid base url parameter detected in the test context");
                }
            }

            if (targetBrowser == TargetBrowser.Undefined)
            {
                if (!Enum.TryParse(TestContext?.Properties["targetBrowser"]?.ToString(), true, out targetBrowser))
                {
                    throw new ArgumentNullException("Invalid target browser parameter detected in the test context");
                }
            }

            if (width <= 0 || height <= 0)
            {
                if (!int.TryParse(TestContext?.Properties["resolutionWidth"].ToString(), out width))
                {
                    throw new ArgumentNullException("Invalid resolution width parameter detected in the test context");
                }

                if (!int.TryParse(TestContext?.Properties["resolutionHeight"].ToString(), out height))
                {
                    throw new ArgumentNullException("Invalid resolution height parameter detected in the test context");
                }
            }

            if (CurrentApplication != null)
            {
                CloseCurrentApplication();
            }

            this.BaseUrl = url;
            this.CurrentApplication = DevFunAppPageObject.Launch(url, targetBrowser, width, height);
            return CurrentApplication;
        }

        public void SaveScreenshot(string name = null)
        {
            TestContext.AddResultFile(name);
        }

        private void CloseCurrentApplication()
        {
            this.CurrentApplication?.Close();
            this.CurrentApplication = null;
        }
    }
}