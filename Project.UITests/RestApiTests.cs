using Microsoft.Extensions.Options;
using Project.Core.Configurations;
using Project.Core.RestApiServices;
using System.Threading.Tasks;
using Xunit;

namespace Project.UITests
{
    public class RestApiTests
    {
        private const string _correctNamePlanet = "Tatooine";
        private const string _incorrectNamePlanet = "xyz";
        private readonly IRestApiService _restApiService;
        private readonly ReaderTestData _readerDataConfig;

        public RestApiTests(IRestApiService restApiService, IOptions<ReaderTestData> readerTestDataOptions)
        {
            _restApiService = restApiService;
            _readerDataConfig = readerTestDataOptions.Value;
        }

        [Fact]
        public async Task CheckNamePlanet_ReturnsIsNamePlanetCorrect()
        {
            var personalData = await _restApiService.GetResponse<PersonalData>($"{_readerDataConfig.UrlPersonalData}{_readerDataConfig.FormatJson}");
            var planetData = await _restApiService.GetResponse<PlanetlData>(personalData.Homeworld);

            Assert.Equal(_correctNamePlanet, planetData.Name);
        }

        [Fact]
        public async Task CheckNamePlanet_ReturnsIsNamePlanetIncorrect()
        {
            var personalData = await _restApiService.GetResponse<PersonalData>($"{_readerDataConfig.UrlPersonalData}{_readerDataConfig.FormatJson}");
            var planetData = await _restApiService.GetResponse<PlanetlData>(personalData.Homeworld);

            Assert.NotEqual(_incorrectNamePlanet, planetData.Name);
        }
    }
}
