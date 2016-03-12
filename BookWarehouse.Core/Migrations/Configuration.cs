using System.Collections.Generic;
using BookWarehouse.Core.Data;
using BookWarehouse.Core.Domain;

namespace BookWarehouse.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WarehouseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WarehouseContext context)
        {
            SeedSampleWarehouses(context);
            SeedSampleTitles(context);
            SeedSampleInventoryItems(context);
        }

        private void SeedSampleWarehouses(WarehouseContext context)
        {
            context.Warehouses.AddOrUpdate(warehouse => new {warehouse.Name},
                new Warehouse { Name = "Alpha" },
                new Warehouse { Name = "Beta" },
                new Warehouse { Name = "Charlie" },
                new Warehouse { Name = "Delta" },
                new Warehouse { Name = "Echo" },
                new Warehouse { Name = "Foxtrot" }
                );

            context.SaveChanges();
        }
        private void SeedSampleTitles(WarehouseContext context)
        {
            context.Titles.AddOrUpdate(title => new {title.Isbn, title.YearPublished},
                new Title { Name = "Developing ASP.NET MVC 4 Web Applications", Isbn = 9780735677227, YearPublished = 2014 },
                new Title { Name = "Data Abstraction and Problem Solving with C++: Walls and Mirrors", Isbn = 0201874024, YearPublished = 1998},
                new Title { Name = "Interaction Design: Beyond Human-Computer Interaction", Isbn = 9780470665763, YearPublished = 2011},
                new Title { Name = "Code Complete: A Practical Handbook of Software Construction", Isbn = 0735619670, YearPublished = 2004},
                new Title { Name = "Algorithms", Isbn = 9780073523408, YearPublished = 2008},
                new Title { Name = "The Mythical Man-Month", YearPublished = 1995},
                new Title { Name = "Discrete Mathematics and Its Applications", Isbn = 9780072880083, YearPublished = 2007},
                new Title { Name = "Linux Kernal Development", Isbn = 9780672329463, YearPublished = 2010},
                new Title { Name = "The Linux Programming Interface", Isbn = 9781593272203, YearPublished = 2010},
                new Title { Name = "Linux Pocket Guide", Isbn = 9781449316693, YearPublished = 2012},
                new Title { Name = "America: A Citizen's Guide to Democracy Inaction", Isbn = 0446532681, YearPublished = 2004}
                );

            context.SaveChanges();
        }

        private void SeedSampleInventoryItems(WarehouseContext context)
        {
            // not strictly needed, but useful for demo data
            var titles = context.Titles.ToList();

            for (int i = 0; i < 100; i++)
            {
                var qualityPrice = RandQuality();

                context.InventoryItems.AddOrUpdate(
                    new InventoryItem
                    {
                        TitleId = RandTitle(context),
                        WarehouseId = RandWh(context),
                        Edition = RandEd(),
                        Quality = qualityPrice.Item1,
                        Price = qualityPrice.Item2
                    });
            }

            context.SaveChanges();
        }


        // These methods aren't necessary, but allow us to dump in some test data
        private Guid RandWh(WarehouseContext context)
        {
            var guids = context.Warehouses.Select(x => x.WarehouseId).ToList();

            var rand = new Random();
            return guids[rand.Next(guids.Count - 1)];
        }

        private Guid RandTitle(WarehouseContext context)
        {
            var guids = context.Titles.Select(x => x.TitleId).ToList();

            var rand = new Random();
            return guids[rand.Next(guids.Count - 1)];
        }

        private string RandEd()
        {
            var editions = new List<string> {"1st", "2nd", "3rd", "4th", "5th", "6th"};

            var rand = new Random();
            return editions[rand.Next(editions.Count - 1)];
        }

        private Tuple<int, decimal> RandQuality()
        {
            var rand = new Random();
            var index = rand.Next(1, 5);

            var prices = new List<decimal> { 9.99m, 14.99m, 19.99m, 24.99m, 29.99m };
            return new Tuple<int, decimal>(index, prices[index - 1]);
        }
    }
}
