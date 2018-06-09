using System.Collections.Generic;
using CrossSolar.Domain;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

namespace CrossSolar.Repository
{
    public class DayAnalyticsRepository : OneDayElectricityModel, IDayAnalyticsRepository
    {
        CrossSolarDbContext _dbContext;

        public DayAnalyticsRepository(CrossSolarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<OneDayElectricityModel>> GetHistoricalData(int panelId)
        {
            return
                _dbContext.OneHourElectricitys
                .Where(p => p.PanelId == panelId)
                .OrderByDescending(p => p.DateTime)
                .GroupBy(p => new
                {
                    p.DateTime.Year,
                    p.DateTime.Month,
                    p.DateTime.Day
                })
                .Select(s => new OneDayElectricityModel()
                {
                    Average = s.Average(p => p.KiloWatt),
                    Maximum = s.Max(p => p.KiloWatt),
                    Minimum = s.Min(p => p.KiloWatt),
                    Sum = s.Sum(p => p.KiloWatt),
                    DateTime = new DateTime(s.Key.Year, s.Key.Month, s.Key.Day, 0, 0, 0)
                }).ToListAsync();
        }
    }
}