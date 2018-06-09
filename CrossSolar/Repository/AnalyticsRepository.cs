using System.Collections.Generic;
using System.Threading.Tasks;
using CrossSolar.Domain;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CrossSolar.Repository
{
    public class AnalyticsRepository : GenericRepository<OneHourElectricity>, IAnalyticsRepository
    {
        public AnalyticsRepository(CrossSolarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<OneHourElectricity>> GetByPanelId(int panelId)
        {
            return _dbContext.OneHourElectricitys.Where(e => e.PanelId == panelId).OrderBy(s => s.DateTime).ToListAsync();
        }
    }
}