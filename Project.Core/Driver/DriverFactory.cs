using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using Project.Core.Enums;
using System;

namespace Project.Core.Driver
{
    public static class DriverFactory
    {
        public static IWebDriver GetWebDriver(Browsers browser) =>
            browser switch
            {
                Browsers.Chrome => new ChromeDriver(),
                Browsers.Edge => new EdgeDriver(),
                _ => throw new ArgumentOutOfRangeException("Not supported browser: " + browser),
            };
    }
}
