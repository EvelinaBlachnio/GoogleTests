using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace Project.Core.PageObjects
{
    public abstract class BasePage
    {
        protected IWebDriver Driver { get; set; }
        protected WebDriverWait Wait { get; set; }

        protected BasePage(IWebDriver driver)
        {
            Driver = driver;
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void GoToUrl(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }

        public void WaitAndClick(By xpath)
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(xpath));
            Wait.Until(x => x.FindElement(xpath)).Click();
        }

        public IList<IWebElement> WaitAndFindElements(By xpath)
        {
            Wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(xpath));
            return Driver.FindElements(xpath);
        }

        public void WaitAndSendKeyAndAccept(By xpath, string key)
        {
            var element = Wait.Until(x => x.FindElement(xpath));
            var action = new Actions(Driver);
            action.Click(element).SendKeys(key).SendKeys(Keys.Enter).Build().Perform();
        }
    }
}
