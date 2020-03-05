using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Manage_WPF.Entities;
using Warehouse_Manage_WPF.EntityModel;

namespace Warehouse_Manage_WPF.DataAccess
{
    public class DataAPI
    {
        private WarehouseModel context { get; set; }

        public DataAPI()
        {
            this.context = new WarehouseModel();
        }
    }
}
