using DataAccess.Entities;
using DataAccess.EntityModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.DataAcc
{
    public class ProjectAccess : IProjectAccess
    {

        public async Task<Project> GetProjectById(int Id)
        {
            Project project = null;

            try
            {
                using (var context = new WarehouseModel())
                {
                    project = await context.Projects.Include(x => x.Customer).FirstOrDefaultAsync(x => x.Id == Id);
                }
            }
            catch(Exception)
            {
                return null;
            }

            return project;
        }

        public async Task<List<Project>> GetProjectsInfo()
        {
            List<Project> projects = null;

            try
            {
                using (var context = new WarehouseModel())
                {
                    projects = await context.Projects.Include(x => x.Customer).ToListAsync<Project>();
                }
            }
            catch { }

            return projects;
        }

        public async Task<bool> AddProject(Project project)
        {
            try
            {
                using (var context = new WarehouseModel())
                {
                    var existingProject = context.Projects.FirstOrDefault(x => x.Name == project.Name);

                    if (existingProject == null)
                    {
                        context.Projects.Add(project);

                        await context.SaveChangesAsync();
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch { }

            return true;
        }

        public async Task<bool> UpdateProject(Project project)
        {
            try
            {
                using (var context = new WarehouseModel())
                {
                    var existingProject = context.Projects.FirstOrDefault(x => x.Id == project.Id);

                    if(existingProject != null)
                    {
                        existingProject.Name = project.Name;
                        existingProject.Status = project.Status;
                        existingProject.CustomerID = project.CustomerID;
                        existingProject.Comment = project.Comment;

                        await context.SaveChangesAsync();
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch(Exception)
            {
                return false;
            }

            return true;
        }
    }
}
