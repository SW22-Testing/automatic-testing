using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Interactions;

namespace automatic_testing.Helpers.SpecialActions
{
    public static class MouseActionsHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        public static void DoubleClick(AppiumWebElement e)
        {
            new Actions(e.WrappedDriver)
                .MoveToElement(e)
                //? Možná nebude potřeba
                .Click()
                .DoubleClick()
                .Build()
                .Perform();
        }
    }
}
