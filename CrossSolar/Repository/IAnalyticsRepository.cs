using System.Collections.Generic;
using System.Threading.Tasks;
using CrossSolar.Domain;
using CrossSolar.Models;

namespace CrossSolar.Repository
{
    public interface IAnalyticsRepository : IGenericRepository<OneHourElectricity>
    {
        Task<List<OneHourElectricity>> GetByPanelId(int panelId);
    }
}