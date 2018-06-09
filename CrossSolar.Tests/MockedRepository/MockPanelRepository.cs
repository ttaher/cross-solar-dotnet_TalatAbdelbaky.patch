using CrossSolar.Domain;
using CrossSolar.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CrossSolar.Tests.MockedRepository
{
    internal class MockPanelRepository : IPanelRepository
    {
        List<Panel> _data;

        public MockPanelRepository()
        {
            _data = new List<Panel>()
            {
                new Panel()
                {
                    Id = 1,
                    Brand = "x",
                    Latitude = 10,
                    Longitude = 11,
                    Serial = "aa"
                },
                new Panel()
                {
                    Id = 2,
                    Brand = "y",
                    Latitude = 11,
                    Longitude = 12,
                    Serial = "bb"
                }
            };


        }

        public bool Exist(int id)
        {
            return _data.Exists(delegate (Panel e) { return e.Id == id; });
        }

        public Task<Panel> GetAsync(int id)
        {
            return _data.AsQueryable().Where(e=>e.Id == id).SingleOrDefaultAsync();
        }

        public Task<int> InsertAsync(Panel entity)
        {
            _data.Add(entity);

            return Task.Run(delegate () { return 1; });
        }

        public IQueryable<Panel> Query()
        {
            return _data.AsQueryable();
        }

        public Task UpdateAsync(Panel entity)
        {
            int i = _data.FindIndex(delegate (Panel check) { return check.Id == entity.Id; });
            _data[i] = entity;
            return null;
        }
    }
}
