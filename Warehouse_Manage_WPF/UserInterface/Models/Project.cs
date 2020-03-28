using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse_Manage_WPF.UserInterface.Models
{
    public class Project
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CustomerID { get; set; }

        public Customer Customer { get; set; }

        public ICollection<Device> Devices { get; set; }

        public Project(Entities.Project project)
        {
            Id = project.Id;
            Name = project.Name;
            CustomerID = project.CustomerID;

            if(project.Customer != null)
                Customer = new Customer(project.Customer);
        }

    }
}
