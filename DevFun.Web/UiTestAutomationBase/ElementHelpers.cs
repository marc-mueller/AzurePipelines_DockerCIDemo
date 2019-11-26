using OpenQA.Selenium.Appium.Windows;

namespace UiTestAutomationBase
{
    public static class ElementHelpers
    {
        public static WindowsElement SetText(this WindowsElement element, string text)
        {
            element.Clear();
            element.SendKeys(text);
            return element;
        }

        public static string GetText(this WindowsElement element)
        {
            return element.Text;
        }
    }
}
