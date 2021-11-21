using OpenQA.Selenium;
using Project.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Project.Core.PageObjects
{
    public class GoogleMapsPage : BasePage
    {

        #region Selectors
        private readonly By _buttons = By.XPath(".//button |.//*[@class='button']");
        private readonly By _searchField = By.XPath(".//*[@id='searchboxinput']");
        private readonly By _setRoute = By.XPath(".//*[@id='pane']/*/*[1]/*/*/*[4]/div[1]/button");
        private readonly By _walkOption = By.XPath(".//*[@data-travel_mode]/button/*[1][contains(@src, 'walk')]");
        private readonly By _bikeOption = By.XPath(".//button/*[1][contains(@src, 'bicycle')] | .//button/*[1][contains(@src, 'bike')]");
        private readonly By _startPoint = By.XPath(".//*[@id= 'directions-searchbox-0']/*/*/input");
        private readonly By _tripTime = By.XPath(".//*[contains(@class, 'duration')]");
        private readonly By _tripDistance = By.XPath(".//*[contains(@class, 'duration')]/following-sibling::*[1]");
        #endregion

        public GoogleMapsPage(IWebDriver driver) 
            : base(driver)
        {
        }

        public GoogleMapsPage AcceptCookies()
        {
            var buttonsCookiesPage = Driver.FindElements(_buttons);
            var buttonCookies = buttonsCookiesPage.LastOrDefault();
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView();", buttonCookies);
            buttonCookies.Click();
            return this;
        }

        public GoogleMapsPage SearchEndPoint(string place)
        {
            SearchPlace(_searchField, place);
            return this;
        }

        public GoogleMapsPage SearchStartPoint(string place)
        {
            SearchPlace(_startPoint, place);
            return this;
        }

        public GoogleMapsPage SetRoute()
        {
            WaitAndClick(_setRoute);
            return this;
        }

        public GoogleMapsPage ChoiceTransportType(TransportType typeTransport) 
        {
            switch (typeTransport)
            {
                case(TransportType.Walk):
                    WaitAndClick(_walkOption);
                    break;
                case (TransportType.Bike):
                    WaitAndClick(_bikeOption);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Transport type is not support");
            }
            return this;
        }

        public List<decimal> GetTripTime()
        {
            var regex = new Regex(@"[0-9]+");
            var tripsTimeList = WaitAndFindElements(_tripTime);
            return tripsTimeList.Select(s => decimal.Parse(regex.Match(s.Text.Replace(',', '.')).Value)).ToList();
        }

        public List<double> GetTripDisctance()
        {
            var regex = new Regex(@"[0-9,]+");
            var tripsTimeList = WaitAndFindElements(_tripDistance);
            return tripsTimeList.Select(s => double.Parse(regex.Match(s.Text.Replace(',', '.')).Value)).ToList();
        }

        private void SearchPlace(By xpath, string place)
        {
            WaitAndSendKeyAndAccept(xpath, place);
        }
    }
}
