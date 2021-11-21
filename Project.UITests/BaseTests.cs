using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using Project.Core.Configurations;
using System;

namespace Project.UITests
{
    public abstract class BaseTests : IDisposable
    {
        protected readonly AppSettings settings;
        protected readonly IWebDriver Driver;

        protected BaseTests(IWebDriver driver, IOptions<AppSettings> appSettingsOptions)
        {
            settings = appSettingsOptions.Value;
            Driver = driver;
            Driver.Manage().Window.Maximize();
        }

        public void Dispose()
        {
            Driver.Quit();
        }
    }
}
