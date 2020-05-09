using DataAccess.Entities;
using Warehouse_Manage_WPF.UserInterface.Helpers;

namespace Warehouse_Manage_WPF.UserInterface.Models
{
    public class CustomerModel : ICustomerEntityConversion
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

        public CustomerModel() { }

        public Customer ConvertToCustomerEntity()
        {
            var customer = new Customer()
            {
                Name = this.Name,
                Address = this.Address,
                City = this.City
            };

            return customer;
        }
    }
}
