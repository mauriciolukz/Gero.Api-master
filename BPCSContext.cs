using Microsoft.EntityFrameworkCore;
using Gero.API.Models.AS400;
using IBM.EntityFrameworkCore;

namespace Gero.API
{
    public class BPCSContext : DbContext
    {
        public BPCSContext(DbContextOptions<BPCSContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<PriceList> PriceLists { get; set; }
        public DbSet<Receivable> Receivables { get; set; }
        public DbSet<InitialInventoryLoad> InitialInventoryLoads { get; set; }
        public DbSet<InitialInventoryLoadWithLot> InitialInventoryLoadWithLots { get; set; }
        public DbSet<PurchaseHistory> PurchaseHistories { get; set; }
        public DbSet<Item> Items { get; set; }
    }
}