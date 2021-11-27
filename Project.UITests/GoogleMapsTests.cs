using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using Project.Core.Configurations;
using Project.Core.Enums;
using Project.Core.PageObjects;
using System;
using System.Linq;
using Xunit;

namespace Project.UITests
{
    public class GoogleMapsTests : BaseTests
    {
        private readonly GoogleMapsPage _googleMapsPage;
        private const string _expectedTime = "01:50";
        private const int _expectedDistance = 10;
        private const string _firstPoint = "Plac Defilad 1, Warszawa";
        private const string _secondPoint = "Bemowo Lotnisko, Warszawa";

        public GoogleMapsTests(IWebDriver driver, IOptions<AppSettings> appSettingsOptions, GoogleMapsPage googleMapsPag)
            : base(driver, appSettingsOptions)
        {
            _googleMapsPage = googleMapsPag;
        }

        [Theory]
        [Trait ("Category","Check Time Distance Walk")]
        [InlineData(_firstPoint, _secondPoint, _expectedTime, _expectedDistance)]
        [InlineData(_secondPoint, _firstPoint, _expectedTime, _expectedDistance)]
        public void CheckTimeAndDistanceWalk_ReturnsIsRange(string startPoint, string endPoint, string expectedTime, int expectedDistance)
        {
            _googleMapsPage.GoToUrl($"{settings.HttpSchema}://{settings.Url}");
            _googleMapsPage.AcceptCookies()
                            .SearchEndPoint(endPoint)
                            .SetRoute()
                            .ChoiceTransportType(TransportType.Walk)
                            .SearchStartPoint(startPoint);

            var teamTripList = _googleMapsPage.GetTripTime();
            var teamDistanceList = _googleMapsPage.GetTripDisctance();
            var expectedTimeSpan = TimeSpan.ParseExact(expectedTime, @"hh\:mm", null);

            Assert.True(teamTripList.All(x => x < expectedTimeSpan));
            Assert.True(teamDistanceList.All(x => x < expectedDistance));
        }

        [Theory]
        [Trait("Category", "Check Time Distance Bike")]
        [InlineData(_firstPoint, _secondPoint, _expectedTime, _expectedDistance)]
        [InlineData(_secondPoint, _firstPoint, _expectedTime, _expectedDistance)]
        public void CheckTimeAndDistanceBike_ReturnsIsRange(string startPoint, string endPoint, string expectedTime, int expectedDistance)
        {
            _googleMapsPage.GoToUrl($"{settings.HttpSchema}://{settings.Url}");
            _googleMapsPage.AcceptCookies()
                            .SearchEndPoint(endPoint)
                            .SetRoute()
                            .ChoiceTransportType(TransportType.Bike)
                            .SearchStartPoint(startPoint);

            var teamTripList = _googleMapsPage.GetTripTime();
            var teamDistanceList = _googleMapsPage.GetTripDisctance();
            var expectedTimeSpan = TimeSpan.ParseExact(expectedTime, @"hh\:mm", null);

            Assert.True(teamTripList.All(x => x < expectedTimeSpan));
            Assert.True(teamDistanceList.All(x => x < expectedDistance));
        }
    }
}
