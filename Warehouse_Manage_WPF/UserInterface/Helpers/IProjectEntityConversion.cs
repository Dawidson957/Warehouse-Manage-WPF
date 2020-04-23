using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Manage_WPF.Entities;
using Warehouse_Manage_WPF.UserInterface.Models;

namespace Warehouse_Manage_WPF.UserInterface.Helpers
{
    interface IProjectEntityConversion
    {
        Task<Project> ConvertToProjectEntity();

        Task<int> GetCustomerId(string customerName);
    }
}
