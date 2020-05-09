using System.Threading.Tasks;
using DataAccess.Entities;

namespace Warehouse_Manage_WPF.UserInterface.Helpers
{
    interface IProjectEntityConversion
    {
        Task<Project> ConvertToProjectEntity();

        Task<int> GetCustomerId(string customerName);
    }
}
