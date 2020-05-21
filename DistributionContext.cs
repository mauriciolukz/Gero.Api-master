using Gero.API.Enumerations;
using Gero.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using Gero.API.Models.AS400;

namespace Gero.API
{
    public class DistributionContext : DbContext
    {
        public DistributionContext(DbContextOptions<DistributionContext> options) : base(options)
        {
        }

        /**
        * Expose models
        */

        // Customers
        public DbSet<Models.Customer> Customers { get; set; }

        // Pending routes to dispatch
        public DbSet<PendingRoutesToDispatch> PendingRoutesToDispatch { get; set; }

        // Exchange rates
        public DbSet<ExchangeRate> ExchangeRates { get; set; }
        public DbSet<ExchangeRateByCustomer> ExchangeRateByCustomers { get; set; }

        // Countries, Departments and Municipalities
        public DbSet<Country> Countries { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Municipality> Municipalities { get; set; }

        // Users
        public DbSet<User> Users { get; set; }
        public DbSet<UserSetting> UserSettings { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<UserTelephone> UserTelephones { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserDevice> UserDevices { get; set; }

        // Devices
        public DbSet<Device> Devices { get; set; }

        // Roles
        public DbSet<Role> Roles { get; set; }

        // Modules
        public DbSet<Module> Modules { get; set; }

        // Role-Modules
        public DbSet<RoleModule> RoleModules { get; set; }

        // Sales channel
        public DbSet<SalesChannel> SalesChannels { get; set; }
        public DbSet<SalesRegion> SalesRegions { get; set; }

        // Route events
        public DbSet<RouteEvent> RouteEvents { get; set; }
        public DbSet<RouteEventItem> RouteEventItems { get; set; }

        // Type of visits
        public DbSet<TypeOfVisit> TypeOfVisits { get; set; }

        // Type of catalogs
        public DbSet<TypeOfCatalog> TypeOfCatalogs { get; set; }

        // Orders
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderType> OrderTypes { get; set; }
        public DbSet<OrderTypeMotive> OrderTypeMotives { get; set; }
        public DbSet<ConsolidatedOrder> ConsolidatedOrders { get; set; }

        // Container returns
        public DbSet<ContainerReturn> ContainerReturns { get; set; }
        public DbSet<ContainerReturnItem> ContainerReturnItems { get; set; }

        // Container loans
        public DbSet<ContainerLoan> ContainerLoans { get; set; }
        public DbSet<ContainerLoanItem> ContainerLoanItems { get; set; }

        // Invoices
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }

        // Synchronization steps
        public DbSet<SynchronizationStep> SynchronizationSteps { get; set; }
        public DbSet<SynchronizationStepByRole> SynchronizationStepByRoles { get; set; }

        // Application setting
        public DbSet<ApplicationSetting> ApplicationSettings { get; set; }

        // Application version
        public DbSet<ApplicationVersion> ApplicationVersions { get; set; }

        // Authorization codes
        public DbSet<AuthorizationCode> AuthorizationCodes { get; set; }
        public DbSet<AuthorizationCodeType> AuthorizationCodeTypes { get; set; }

        // Motives
        public DbSet<Motive> Motives { get; set; }

        // Item images
        public DbSet<ItemImage> ItemImages { get; set; }

        // Warehouses
        public DbSet<Warehouse> Warehouses { get; set; }

        // Routes
        public DbSet<Route> Routes { get; set; }
        public DbSet<RouteSummary> RouteSummaries { get; set; }
        public DbSet<DeliveryRoute> DeliveryRoutes { get; set; }

        // Motives not to sell
        public DbSet<MotivesNotToSell> MotivesNotToSells { get; set; }
        public DbSet<MotivesNotToSellType> MotivesNotToSellTypes { get; set; }

        // Item centralilzation by customers
        public DbSet<ItemCentralizationByCustomer> ItemCentralizationByCustomers { get; set; }

        // Items
        public DbSet<AllowedItem> AllowedItems { get; set; }

        // Target sales
        public DbSet<TargetSale> TargetSales { get; set; }

        // Advance target sales
        public DbSet<AdvanceTargetSale> AdvanceTargetSales { get; set; }

        // Approver types
        public DbSet<ApproverType> ApproverTypes { get; set; }

        // Coupon users
        public DbSet<CouponUser> CouponUsers { get; set; }
        public DbSet<CouponUserWithApprover> CouponUserWithApprovers { get; set; }
        public DbSet<CouponUserByApproverType> CouponUserByApproverTypes { get; set; }

        // Consecutive invoices
        public DbSet<ConsecutiveInvoice> ConsecutiveInvoices { get; set; }

        // Payment receipts
        public DbSet<PaymentReceipt> PaymentReceipts { get; set; }
        public DbSet<PaymentReceiptItem> PaymentReceiptItems { get; set; }

        // Banks with payment types
        public DbSet<BankWithPaymentType> BankWithPaymentTypes { get; set; }

        // Bonus
        public DbSet<Bonus> Bonuses { get; set; }
        public DbSet<BonusByCustomer> BonusByCustomers { get; set; }
        public DbSet<ItemByBonusCustomer> ItemByBonusCustomers { get; set; }
        public DbSet<BonusStatus> BonusStatuses { get; set; }
        public DbSet<BonusStatusLog> BonusStatusLogs { get; set; }

        // Synchronization records
        public DbSet<SynchronizationRecord> SynchronizationRecords { get; set; }

        // Initial inventories
        public DbSet<InitialInventory> InitialInventories { get; set; }
        public DbSet<InitialInventoryItem> InitialInventoryItems { get; set; }

        // Inventory liquidations
        public DbSet<InventoryLiquidation> InventoryLiquidations { get; set; }
        public DbSet<InventoryLiquidationItem> InventoryLiquidationItems { get; set; }

        // Cost center by item families
        public DbSet<CostCenterByItemFamily> CostCenterByItemFamilies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ApplyModelRelationship(modelBuilder);
            //ApplyDataSeed(modelBuilder);

            // Iterate each entity to save Status column as string
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    // FIXME: Use reflection to get converters
                    if (property.Name == "Position")
                    {
                        // Set Position enum converter
                        property.SetValueConverter(
                            new ValueConverter<Position, string>(
                                v => v.ToString(),
                                v => (Position)Enum.Parse(typeof(Position), v)
                            )
                        );
                    }

                    if (property.Name == "Status")
                    {
                        // Set Status enum converter
                        property.SetValueConverter(
                            new ValueConverter<Status, string>(
                                v => v.ToString(),
                                v => (Status)Enum.Parse(typeof(Status), v)
                            )
                        );
                    }

                    if (property.Name == "TelephoneType")
                    {
                        // Set TelephoneType enum converter
                        property.SetValueConverter(
                            new ValueConverter<TelephoneType, string>(
                                v => v.ToString(),
                                v => (TelephoneType)Enum.Parse(typeof(TelephoneType), v)
                            )
                        );
                    }
                }
            }
        }

        private void ApplyModelRelationship(ModelBuilder modelBuilder)
        {
            // Add one-to-one relationship in user-usersetting, user-info
            modelBuilder.Entity<User>().HasOne(a => a.Setting).WithOne(b => b.User);
            modelBuilder.Entity<User>().HasOne(a => a.Info).WithOne(b => b.User);

            // Add many-to-many relationship in order type-motive
            modelBuilder.Entity<OrderTypeMotive>().HasKey(c => new { c.OrderTypeId, c.MotiveId });

            // Add many-to-many relationship in role-module
            modelBuilder.Entity<RoleModule>().HasKey(c => new { c.RoleId, c.ModuleId });

            modelBuilder.Entity<RoleModule>()
                .HasOne(c => c.Role)
                .WithMany(c => c.Modules)
                .HasForeignKey(c => c.RoleId);

            modelBuilder.Entity<RoleModule>()
                .HasOne(c => c.Module)
                .WithMany(c => c.Roles)
                .HasForeignKey(c => c.ModuleId);

            // Add many-to-many relationship in user-role
            modelBuilder.Entity<UserRole>().HasKey(c => new { c.UserId, c.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(c => c.User)
                .WithMany(c => c.Roles)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(c => c.Role)
                .WithMany(c => c.Users)
                .HasForeignKey(c => c.RoleId);

            // Add many-to-many relationship in synchronization step by role
            modelBuilder.Entity<SynchronizationStepByRole>().HasKey(c => new { c.RoleId, c.SynchronizationStepId });

            // Add many-to-many relationship in user-device
            modelBuilder.Entity<UserDevice>().HasKey(c => new { c.UserId, c.DeviceId });

            modelBuilder.Entity<UserDevice>()
                .HasOne(c => c.User)
                .WithMany(c => c.Devices)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<UserDevice>()
                .HasOne(c => c.Device)
                .WithMany(c => c.Users)
                .HasForeignKey(c => c.DeviceId);

            // Add self relationship in module
            modelBuilder.Entity<Module>(entity =>
                entity
                    .HasMany(x => x.Children)
                    .WithOne(x => x.Parent)
                    .HasForeignKey(x => x.ParentId)
            );

            // Allowed item
            modelBuilder.Entity<AllowedItem>(entity =>
                entity
                    .HasIndex(x => x.ItemCode)
                    .IsUnique()
            );
        }

        private void ApplyDataSeed(ModelBuilder modelBuilder)
        {
            // Approver types
            modelBuilder.Entity<ApproverType>().HasData(
                new ApproverType { Id = 1, Name = "Solicitante Móvil", Code = "mobile_applicant" },
                new ApproverType { Id = 2, Name = "Solicitante Web", Code = "web_applicant" },
                new ApproverType { Id = 3, Name = "Verificador", Code = "checker" },
                new ApproverType { Id = 4, Name = "Aprobador", Code = "approver" },
                new ApproverType { Id = 5, Name = "Aprobador Superior", Code = "master_approver" }
            );
        }
    }
}