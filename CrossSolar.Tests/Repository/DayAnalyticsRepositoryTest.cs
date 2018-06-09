using CrossSolar.Repository;
using CrossSolar.Tests.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;
using CrossSolar.Domain;

namespace CrossSolar.Tests.Repository
{
    public class DayAnalyticsRepositoryTest
    {
        DayAnalyticsRepository _repository;
        PanelRepository _panelRepository;

        public DayAnalyticsRepositoryTest()
        {
            CrossSolarInMemoryDbContextProvider contextProvider = new CrossSolarInMemoryDbContextProvider();
            
            _repository = new DayAnalyticsRepository(contextProvider.GetInMemoryContext());

            _panelRepository = new PanelRepository(contextProvider.GetInMemoryContext());
        }

        [Fact]
        public void GetHistoricalDataAsyncTest()
        {
            Panel p = _panelRepository.Query().FirstOrDefault();

            var result = _repository.GetHistoricalData(p.Id).Result;

            Assert.NotNull(result);
        }



    }
}
