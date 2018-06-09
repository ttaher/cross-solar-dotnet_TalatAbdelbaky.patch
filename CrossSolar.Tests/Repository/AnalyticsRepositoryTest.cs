using CrossSolar.Domain;
using CrossSolar.Repository;
using CrossSolar.Tests.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;


namespace CrossSolar.Tests.Repository
{
    public class AnalyticsRepositoryTest
    {
        AnalyticsRepository _repository;

        public AnalyticsRepositoryTest()
        {
            CrossSolarInMemoryDbContextProvider contextProvider = new CrossSolarInMemoryDbContextProvider();

            _repository = new AnalyticsRepository(contextProvider.GetInMemoryContext());
        }



        private void FullCompare(OneHourElectricity expected, OneHourElectricity current)
        {
            Assert.NotNull(current);
            Assert.Equal(expected.DateTime, current.DateTime);
            Assert.Equal(expected.KiloWatt, current.KiloWatt);
            Assert.Equal(expected.PanelId, current.PanelId);
        }



        [Fact]
        public void InsertAsync()
        {
            var sample = CreateRandomEntity(1);

            int res = _repository.InsertAsync(sample).Result;

            Assert.NotEqual(default(int), sample.Id);

            var r = _repository.Query().Where(p => p.Id == sample.Id).FirstOrDefault();

            FullCompare(sample, r);
        }

        [Fact]
        public void GetAsyncTest()
        {
            var sample = CreateRandomEntity(1);

            int res = _repository.InsertAsync(sample).Result;

            var result = _repository.GetAsync(sample.Id).Result;

            FullCompare(sample, result);
        }
        [Fact]
        public void GetByPanelIdTest()
        {
            var sample = CreateRandomEntity(1);

            var result = _repository.GetByPanelId(sample.Id);
            Assert.NotNull(result);
        }


        [Fact]
        public void UpdateAsync()
        {
            var sample = CreateRandomEntity(1);

            int res = _repository.InsertAsync(sample).Result;

            var result = _repository.GetAsync(sample.Id).Result;

            result.KiloWatt += 5000;

            _repository.UpdateAsync(result).Wait();

            var result2 = _repository.GetAsync(result.Id).Result;

            FullCompare(result, result2);

        }


        private OneHourElectricity CreateRandomEntity(int panelId)
        {
            Random rnd = new Random(DateTime.Now.Millisecond);

            return new OneHourElectricity()
            {
                DateTime = DateTime.Now,
                KiloWatt = rnd.Next(10000),
                PanelId = panelId
            };
        }
    }
}
