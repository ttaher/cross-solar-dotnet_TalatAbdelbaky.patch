using System;
using System.Threading.Tasks;
using CrossSolar.Controllers;
using CrossSolar.Domain;
using CrossSolar.Models;
using CrossSolar.Repository;
using CrossSolar.Tests.MockedRepository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;


namespace CrossSolar.Tests.Controller
{
    public class AnalyticsControllerTest
    {
        private AnalyticsController _analyticsController;

        private IPanelRepository _panelRepositoryMock;
        private IAnalyticsRepository _analyticsRepository;
        private IDayAnalyticsRepository _dayAnalyticsRepository;


        public AnalyticsControllerTest()
        {

            _analyticsRepository = new MockedAnalyticsRepository();

            _panelRepositoryMock = new MockPanelRepository();

            _dayAnalyticsRepository = new MockedDayAnalyticsRepository();

            _analyticsController = new AnalyticsController(_analyticsRepository, _panelRepositoryMock, _dayAnalyticsRepository);
        }



        [Fact]
        public async Task Get()
        {
            int panelId = 1;
            var result = await _analyticsController.Get(panelId);
            Assert.NotNull(result);

            var createdResult = result as CreatedResult;
            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult.StatusCode);

        }

        [Fact]
        public async Task DayResults()
        {
            int panelId = 1;
            var result = await _analyticsController.DayResults(panelId);
            Assert.NotNull(result);

            var objectResult = result as ObjectResult;
            Assert.NotNull(objectResult);
            Assert.Equal(200, objectResult.StatusCode);

        }

        [Fact]
        public async Task Post()
        {
            int panelId = 1;
            var model = new OneHourElectricityModel
            {
                DateTime = DateTime.UtcNow,
                KiloWatt = 1000
            };

            var result = await _analyticsController.Post(panelId, model);
            Assert.NotNull(result);
            var createdResult = result as CreatedResult;
            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult.StatusCode);
        }
    }

}
