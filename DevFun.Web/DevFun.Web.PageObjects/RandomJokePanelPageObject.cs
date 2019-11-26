using System.Threading.Tasks;
using OpenQA.Selenium;
using UiTestAutomationBase;

namespace DevFun.Web.PageObjects
{
    public class RandomJokePanelPageObject : WebPageObjectBase
    {
        protected RandomJokePanelPageObject(PageObjectBase parent) : base(parent)
        {
        }

        public RandomJokePanelPageObject(PageObjectBase parent, ISearchContext searchContext)
            : base(parent, searchContext)
        {
        }

        private IWebElement homeLink;
        private IWebElement aboutLink;

        private IWebElement jokeText;

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

        public IWebElement JokeText
        {
            get
            {
                if (jokeText == null)
                {
                    jokeText = FindById<IWebElement>("jokeText");
                }
                return jokeText;
            }
        }


        public RandomJokePanelPageObject GotoHome()
        {
            this.HomeLink.Click();
            Task.Delay(100).Wait();
            return new RandomJokePanelPageObject(this.Parent, this.SearchContext);
        }

        public AboutPanelPageObject GotoAbout()
        {
            this.AboutLink.Click();
            Task.Delay(100).Wait();
            return new AboutPanelPageObject(this.Parent, this.Parent.SearchContext);
        }

        public string GetJokeText()
        {
            return this.JokeText?.Text;
        }

    }
}