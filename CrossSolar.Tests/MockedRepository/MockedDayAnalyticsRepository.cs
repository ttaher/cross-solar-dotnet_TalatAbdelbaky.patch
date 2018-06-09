using CrossSolar.Domain;
using CrossSolar.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CrossSolar.Tests.MockedRepository
{
    internal class MockedDayAnalyticsRepository : IDayAnalyticsRepository
    {
        public Task<List<OneDayElectricityModel>> GetHistoricalData(int panelId)
        {
            throw new NotImplementedException();
        }

        public Task<List<OneDayElectricityModel>> GetHistoricalDataAsync(int panelId)
        {
            List<OneDayElectricityModel> list = new List<OneDayElectricityModel>()
            {
                new OneDayElectricityModel()
                {
                    Average  =10,
                    DateTime = new DateTime(2018,10,10,8,0,0),
                    Maximum = 100,
                    Minimum = 1,
                    Sum = 12312
                },
                new OneDayElectricityModel()
                {
                    Average  = 11,
                    DateTime = new DateTime(2018,10,10,9,0,0),
                    Maximum = 200,
                    Minimum = 4,
                    Sum = 123123
                }
            };


            return Task.Run(delegate () { return list; });
        }
    }
}
