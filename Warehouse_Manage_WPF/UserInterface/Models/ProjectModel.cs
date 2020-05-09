using DataAccess.DataAcc;
using System.Threading.Tasks;
using DataAccess.Entities;
using Warehouse_Manage_WPF.UserInterface.Helpers;

namespace Warehouse_Manage_WPF.UserInterface.Models
{
    public class ProjectModel : IProjectEntityConversion
    {
        private ICustomerAccess _customerAccess { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        public string Comment { get; set; }

        public string CustomerName { get; set; }


        public ProjectModel(Project project, ICustomerAccess customerAccess)
        {
            Id = project.Id;
            Name = project.Name;
            Status = project.Status;
            Comment = project.Comment;
            CustomerName = project.Customer.Name;
            _customerAccess = customerAccess;
        }

        public ProjectModel(ICustomerAccess customerAccess)
        {
            _customerAccess = customerAccess;
        }

        public async Task<Project> ConvertToProjectEntity()
        {
            var projectEntity = new Project
            {
                Id = this.Id,
                Name = this.Name,
                Status = this.Status,
                Comment = this.Comment,
                CustomerID = await GetCustomerId(this.CustomerName)
            };

            return projectEntity;
        }

        public async Task<int> GetCustomerId(string customerName)
        {
            int customerId = await _customerAccess.GetCustomerId(customerName);

            return customerId;
        }
    }
}
