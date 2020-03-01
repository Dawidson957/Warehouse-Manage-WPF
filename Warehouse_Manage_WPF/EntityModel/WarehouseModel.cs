namespace Warehouse_Manage_WPF.EntityModel
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Warehouse_Manage_WPF.Entities;

    public class WarehouseModel : DbContext
    {
        public WarehouseModel()
            : base("name=WarehouseModel")
        {
        }

    }
}