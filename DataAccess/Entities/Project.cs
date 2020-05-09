using System.Collections.Generic;

namespace DataAccess.Entities
{
    public class Project
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        public string Comment { get; set; }

        public int CustomerID { get; set; }

        public Customer Customer { get; set; }

        public virtual ICollection<Device> Devices { get; set; }
    }
}
