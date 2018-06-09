using CrossSolar.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrossSolar.Repository
{
    public interface IDayAnalyticsRepository
    {
        Task<List<OneDayElectricityModel>> GetHistoricalData(int panelId);

    }
}