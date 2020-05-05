using DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.DataAcc
{
    public interface IProjectAccess
    {
        Task<bool> AddProject(Project project);
        Task<List<Project>> GetProjectsInfo();
    }
}