using OpenQA.Selenium;

namespace UiTestAutomationBase
{
    public abstract class PageObjectBase
    {
        protected PageObjectBase(ISearchContext searchContext)
        {
            this.SearchContext = searchContext;
        }

        protected PageObjectBase(PageObjectBase parent)
        {
            this.Parent = parent;
            this.SearchContext = parent.SearchContext;
        }

        protected PageObjectBase(PageObjectBase parent, ISearchContext searchContext)
        {
            this.Parent = parent;
            this.SearchContext = searchContext;
        }

        public PageObjectBase Parent { get; }
        public ISearchContext SearchContext { get; set; }

        public virtual TControl FindById<TControl>(string id, bool doWait = true) where TControl : class, IWebElement
        {
            var by = By.Id(id);
            var element = SearchContext.FindElement(by) as TControl;
            return element;
        }

        public abstract Screenshot TakeScreenshot();
    }
}
