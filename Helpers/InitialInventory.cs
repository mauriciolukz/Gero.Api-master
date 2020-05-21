using Gero.API.Models;
using Gero.API.Models.AS400;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gero.API.Helpers
{
    public class InitialInventory
    {
        private readonly DistributionContext _context;

        public InitialInventory(DistributionContext context)
        {
            _context = context;
        }

        public Boolean IsThereInventoryForToday(string route)
        {
            return _context
                .InitialInventories
                .Where(
                    x =>
                        x.SynchronizationDate.Year == DateTime.Now.Year &&
                        x.SynchronizationDate.Month == DateTime.Now.Month &&
                        x.SynchronizationDate.Day == DateTime.Now.Day
                )
                .Where(x => x.Route == route)
                .Count() > 0;
        }

        public void LoadInventoryForToday(string route, List<InitialInventoryLoadWithLot> initialInventoryLoadWithLots)
        {
            // Create new initial inventory header
            Models.InitialInventory initialInventory = new Models.InitialInventory
            {
                Route = route,
                SynchronizationDate = DateTime.Now,
                SynchronizationId = CreateSynchronizationRecord(route, initialInventoryLoadWithLots.Count())
            };

            // Add new initial inventory to database context
            _context.InitialInventories.Add(initialInventory);

            // Save initial inventory to database
            _context.SaveChangesAsync();

            // Create initial inventory items
            CreateInitialInventoryItems(initialInventory, initialInventoryLoadWithLots);
        }

        private void CreateInitialInventoryItems(Models.InitialInventory initialInventory, List<InitialInventoryLoadWithLot> initialInventoryLoadWithLots)
        {
            // Iterate through all initial inventory load with lots
            foreach (var initialInventoryLoadWithLot in initialInventoryLoadWithLots)
            {
                // Create new initial inventory item instance
                Models.InitialInventoryItem initialInventoryItem = new Models.InitialInventoryItem
                {
                    ItemCode = initialInventoryLoadWithLot.ItemCode,
                    Lot = initialInventoryLoadWithLot.Lot,
                    UnitsPerBox = Int32.Parse(initialInventoryLoadWithLot.UnitsPerBox),
                    UnitsQuantity = 1,
                    InitialInventoryId = initialInventory.Id
                };

                // Add new initial inventory item to database context
                _context.InitialInventoryItems.Add(initialInventoryItem);

                // Save initial inventory item to databases
                _context.SaveChangesAsync();
            }
        }

        private int CreateSynchronizationRecord(string route, int numberOfRecords)
        {
            // Create a new synchronization record
            SynchronizationRecord synchronizationRecord = new SynchronizationRecord
            {
                Route = route,
                TypeOfCatalogId = "15", // Initial load by route
                TypeOfVisitId = "2", // Delivery
                FileName = $"api.v1.routes.{route}.initial_load",
                SynchronizationDate = DateTime.Now,
                RecordsRead = numberOfRecords,
                RecordsSynchronized1 = numberOfRecords,
                RecordsSynchronized2 = numberOfRecords,
                ProcessType = Enumerations.ProcessType.Download
            };

            // Add synchronization record to the context
            _context.SynchronizationRecords.Add(synchronizationRecord);

            // Save synchronization record to the database
            _context.SaveChangesAsync();

            // Return Id saved
            return synchronizationRecord.Id;
        }
    }
}
