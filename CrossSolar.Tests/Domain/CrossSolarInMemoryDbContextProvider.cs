using CrossSolar.Domain;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrossSolar.Tests.Domain
{
    public class CrossSolarInMemoryDbContextProvider
    {
        CrossSolarDbContext _context;

        public CrossSolarInMemoryDbContextProvider()
        {
            var options = new DbContextOptionsBuilder<CrossSolarDbContext>()
               .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
               .Options;
            _context = new CrossSolarDbContext(options);

            Random rnd = new Random(DateTime.Now.Millisecond);

            List<OneHourElectricity> list = new List<OneHourElectricity>();

            for (int i = 0; i < 10; i++)
            {
                Panel p = CreateRandom(rnd);

                list = new List<OneHourElectricity>();

                for (int e=0;e<rnd.Next(10);e++)
                {
                    OneHourElectricity el = CreateRandomOneHourElectricity(p.Id);
                    list.Add(el);
                }

                p.OneHourElectricitys = list;

                _context.Panels.Add(p);
            }

            _context.SaveChanges();
        }

        public CrossSolarDbContext GetInMemoryContext()
        {
            return _context;
        }

        private static Panel CreateRandom(Random rnd)
        {
            return new Panel
            {
                Brand = "Brand_" + DateTime.Now.Ticks,
                Latitude = 12.345678 + rnd.NextDouble(),
                Longitude = 98.7655432 + rnd.NextDouble(),
                Serial = new Guid().ToString(),
            };
        }

        private static OneHourElectricity CreateRandomOneHourElectricity(int panelId)
        {
            Random rnd = new Random(DateTime.Now.Millisecond);

            return new OneHourElectricity()
            {
                DateTime = DateTime.Now.AddHours(rnd.Next(23-DateTime.Now.Hour)),
                KiloWatt = rnd.Next(10000),
                PanelId = panelId
            };
        }
    }
}
