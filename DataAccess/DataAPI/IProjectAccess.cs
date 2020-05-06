using DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.DataAcc
{
    public interface IProjectAccess
    {
        Task<bool> AddProject(Project project);
        Task<List<Project>> GetProjectsInfo();
        Task<bool> UpdateProject(Project project);
        Task<Project> GetProjectById(int Id);
    }
}