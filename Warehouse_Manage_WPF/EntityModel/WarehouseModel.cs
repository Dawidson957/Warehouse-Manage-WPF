namespace Warehouse_Manage_WPF.EntityModel
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Warehouse_Manage_WPF.Entities;
    using Warehouse_Manage_WPF.EntitiesConfiguration;

    public class WarehouseModel : DbContext
    {
        public WarehouseModel()
            : base("name=WarehouseModel")
        {
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Device> Devices { get; set; }

        public DbSet<Producer> Producers { get; set; }

        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CustomerPropertiesConfiguration());
            modelBuilder.Configurations.Add(new DevicePropertiesConfiguration());
            modelBuilder.Configurations.Add(new ProducerPropertiesConfiguration());
            modelBuilder.Configurations.Add(new ProjectPropertiesConfiguration());
        }
    }
}