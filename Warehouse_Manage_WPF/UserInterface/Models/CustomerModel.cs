using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace Warehouse_Manage_WPF.UserInterface.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }


        public CustomerModel(Customer customer)
        {
            Id = customer.Id;
            Name = customer.Name;
            Address = customer.Address;
            City = customer.City;
        }

        public CustomerModel()
        {

        }
    }
}
