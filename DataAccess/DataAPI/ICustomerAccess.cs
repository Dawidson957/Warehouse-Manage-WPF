using DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.DataAcc
{
    public interface ICustomerAccess
    {
        Task<bool> AddCustomer(Customer customer);

        Task<int> GetCustomerId(string name);

        Task<List<Customer>> GetCustomers();

        Task<List<string>> GetCustomersName();

        Task<bool> UpdateCustomer(Customer customer);
    }
}