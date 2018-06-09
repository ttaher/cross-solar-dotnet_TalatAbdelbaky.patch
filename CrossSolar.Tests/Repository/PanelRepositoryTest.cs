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
    public class PanelRepositoryTest
    {
        PanelRepository _repository;

        public PanelRepositoryTest()
        {
            CrossSolarInMemoryDbContextProvider contextProvider = new CrossSolarInMemoryDbContextProvider();
            
            _repository = new PanelRepository(contextProvider.GetInMemoryContext());
        }

        

        private void FullCompare(Panel expected, Panel current)
        {
            Assert.NotNull(current);
            Assert.Equal(expected.Brand, current.Brand);
            Assert.Equal(expected.Latitude, current.Latitude);
            Assert.Equal(expected.Longitude, current.Longitude);
            Assert.Equal(expected.Serial, current.Serial);
        }


        [Fact]
        public void InsertAsync()
        {
            var panel = CreateRandom();

            int res = _repository.InsertAsync(panel).Result;

            Assert.NotEqual(default(int), panel.Id);

            var r = _repository.Query().Where(p => p.Brand == panel.Brand).FirstOrDefault();

            FullCompare(panel, r);
        }

        [Fact]
        public void GetAsyncTest()
        {
            var panel = CreateRandom();

            int rest = _repository.InsertAsync(panel).Result;

            var result = _repository.GetAsync(panel.Id).Result;

            FullCompare(panel, result);
        }
        

        [Fact]
        public void UpdateAsync()
        {
            var panel = CreateRandom();

            int res = _repository.InsertAsync(panel).Result;

            var result = _repository.GetAsync(panel.Id).Result;


            result.Brand = "ax2";
            result.Latitude = 12;
            result.Longitude = 21;
            result.Serial = "x2";

            _repository.UpdateAsync(result).Wait();

            var result2 = _repository.GetAsync(result.Id).Result;

            FullCompare(result, result2);

        }



        private Panel CreateRandom()
        {
            Random rnd = new Random();

            return new Panel
            {
                Brand = "Brand_" + DateTime.Now.Ticks,
                Latitude = 12.345678 + rnd.NextDouble(),
                Longitude = 98.7655432 + rnd.NextDouble(),
                Serial = new Guid().ToString(),
            };
        }

    }
}
