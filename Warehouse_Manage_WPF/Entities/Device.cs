using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse_Manage_WPF.Entities
{
    public class Device
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ArticleNumber { get; set; }

        public string Location { get; set; }

        public int Quantity { get; set; }

        public int ProducerID { get; set; }

        public Producer Producer { get; set; }

        public virtual ICollection<Project> Projects { get; set; }

    }
}
