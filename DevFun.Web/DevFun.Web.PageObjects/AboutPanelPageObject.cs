using System.Threading.Tasks;
using OpenQA.Selenium;
using UiTestAutomationBase;

namespace DevFun.Web.PageObjects
{
    public class AboutPanelPageObject : WebPageObjectBase
    {
        protected AboutPanelPageObject(PageObjectBase parent) : base(parent)
        {
        }

        public AboutPanelPageObject(PageObjectBase parent, ISearchContext searchContext)
            : base(parent, searchContext)
        {
        }

        private IWebElement homeLink;
        private IWebElement aboutLink;

        private IWebElement deploymentEnvironment;
        private IWebElement machineName;

        public IWebElement HomeLink
        {
            get
            {
                if (homeLink == null)
                {
                    homeLink = FindById<IWebElement>("homeLink");
                }
                return homeLink;
            }
        }

        public IWebElement AboutLink
        {
            get
            {
                if (aboutLink == null)
                {
                    aboutLink = FindById<IWebElement>("aboutLink");
                }
                return aboutLink;
            }
        }

        

        public IWebElement DeploymentEnvironment
        {
            get
            {
                if (deploymentEnvironment == null)
                {
                    deploymentEnvironment = FindById<IWebElement>("deploymentEnvironment");
                }
                return deploymentEnvironment;
            }
        }

        public IWebElement MachineName
        {
            get
            {
                if (machineName == null)
                {
                    machineName = FindById<IWebElement>("machineName");
                }
                return machineName;
            }
        }

        public RandomJokePanelPageObject GotoHome()
        {
            this.HomeLink.Click();
            Task.Delay(100).Wait();
            return new RandomJokePanelPageObject(this.Parent, this.Parent.SearchContext);
        }

        public AboutPanelPageObject GotoAbout()
        {
            this.AboutLink.Click();
            Task.Delay(100).Wait();
            return new AboutPanelPageObject(this.Parent, this.SearchContext);
        }

        public string GetAboutText()
        {
            var a1 = this.DeploymentEnvironment?.Text;
            var a2 = this.MachineName?.Text;
            return $"{a1} {a2}";
        }
    }
}