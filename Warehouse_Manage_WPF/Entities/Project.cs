using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse_Manage_WPF.Entities
{
    public class Project
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CustomerID { get; set; }

        public Customer Customer { get; set; }

        public virtual ICollection<Device> Devices { get; set; }
    }
}
