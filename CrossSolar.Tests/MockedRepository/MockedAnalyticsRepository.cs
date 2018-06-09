using CrossSolar.Domain;
using CrossSolar.Models;
using CrossSolar.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossSolar.Tests.MockedRepository
{
    internal class MockedAnalyticsRepository : IAnalyticsRepository
    {
        List<OneHourElectricity> _data;

        public MockedAnalyticsRepository()
        {
            _data = new List<OneHourElectricity>()
            {
                new OneHourElectricity()
                {
                    DateTime = DateTime.Now,
                    Id = 1,
                    KiloWatt = 10,
                    PanelId = 1
                },

                new OneHourElectricity()
                {
                    DateTime = DateTime.Now,
                    Id = 1,
                    KiloWatt = 12,
                    PanelId = 1
                },

                new OneHourElectricity()
                {
                    DateTime = DateTime.Now,
                    Id = 1,
                    KiloWatt = 12,
                    PanelId = 2
                }
            };
        }


        public bool Exist(int id)
        {
            return _data.Where(p => p.Id == id).Any();
        }

        public Task<OneHourElectricity> GetAsync(int id)
        {
            return _data.AsQueryable().Where(k => k.Id == id).SingleOrDefaultAsync();
        }

        public Task<List<OneHourElectricity>> GetByPanelId(int panelId)
        {
            return Task.Run(delegate() { return _data.AsQueryable().Where(k => k.PanelId == panelId).ToList(); });
        }
        
        public Task<int> InsertAsync(OneHourElectricity entity)
        {
            _data.Add(entity);
            
            return Task.Run(delegate(){ return 1; });
        }

        public IQueryable<OneHourElectricity> Query()
        {
            return _data.AsQueryable();
        }

        public Task UpdateAsync(OneHourElectricity entity)
        {
            int i = _data.FindIndex(delegate (OneHourElectricity check) { return check.Id == entity.Id; });
            _data[i] = entity;
            return null;
        }
    }
}
