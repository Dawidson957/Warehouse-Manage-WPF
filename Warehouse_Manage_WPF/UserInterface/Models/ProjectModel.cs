using DataAccess.DataAcc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;
using Warehouse_Manage_WPF.UserInterface.Helpers;

namespace Warehouse_Manage_WPF.UserInterface.Models
{
    public class ProjectModel : IProjectEntityConversion
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Comment { get; set; }

        public string CustomerName { get; set; }


        public ProjectModel(Project project)
        {
            Id = project.Id;
            Name = project.Name;
            CustomerName = project.Customer.Name;
        }

        public async Task<Project> ConvertToProjectEntity()
        {
            var projectEntity = new Project
            {
                Id = this.Id,
                Name = this.Name,
                CustomerID = await GetCustomerId(this.CustomerName)
            };

            return projectEntity;
        }

        public async Task<int> GetCustomerId(string customerName)
        {
            var customerAccess = new CustomerAccess();
            int customerId = await customerAccess.GetCustomerId(customerName);

            return customerId;
        }

        public ProjectModel() { }
    }
}
