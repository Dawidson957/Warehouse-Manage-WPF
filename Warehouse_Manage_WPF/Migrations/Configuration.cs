namespace Warehouse_Manage_WPF.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Warehouse_Manage_WPF.EntityModel.WarehouseModel>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Warehouse_Manage_WPF.EntityModel.WarehouseModel context)
        {
        }
    }
}
