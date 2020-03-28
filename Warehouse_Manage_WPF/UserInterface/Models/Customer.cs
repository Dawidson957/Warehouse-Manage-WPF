using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse_Manage_WPF.UserInterface.Models
{
    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public ICollection<Project> Projects { get; set; }

        public Customer(Entities.Customer customer)
        {
            Id = customer.Id;
            Name = customer.Name;
            Address = customer.Address;
            City = customer.City;

        }

    }
}
