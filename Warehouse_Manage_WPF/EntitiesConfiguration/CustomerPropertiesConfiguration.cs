using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Warehouse_Manage_WPF.Entities;

namespace Warehouse_Manage_WPF.EntitiesConfiguration
{
    public class CustomerPropertiesConfiguration : EntityTypeConfiguration<Customer>
    {
        public CustomerPropertiesConfiguration()
        {

        }
    }
}
