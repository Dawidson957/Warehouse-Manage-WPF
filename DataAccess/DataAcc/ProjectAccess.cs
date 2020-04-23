using DataAccess.Entities;
using DataAccess.EntityModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataAcc
{
    
    public class ProjectAccess
    {
        
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

        public async Task<Project> GetProjectById(int Id)
        {
            Project project = null;

            try
            {
                using (var context = new WarehouseModel())
                {
                    project = await context.Projects.Include(x => x.Customer).Include(x => x.Devices).FirstOrDefaultAsync(x => x.Id == Id);
                }
            }
            catch(Exception)
            {

            }

            return project;
        }

        public async Task<bool> AddProject(Project project)
        {
            try
            {
                using (var context = new WarehouseModel())
                {
                    var existingProject = context.Projects.FirstOrDefault(x => x.Name == project.Name);

                    if(existingProject == null)
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

        public async Task<bool> AddDeviceToProject(int Id, Device device)
        {
            try
            {
                using (var context = new WarehouseModel())
                {
                    var existingProject = context.Projects.FirstOrDefault(x => x.Id == Id);

                    if(existingProject != null)
                    {
                        var existingDevice = existingProject.Devices.FirstOrDefault(x => x.ArticleNumber == device.ArticleNumber);

                        if(existingDevice != null)
                        {
                            existingDevice.Quantity += device.Quantity;
                        }
                        else
                        {
                            existingProject.Devices.Add(device);
                        }

                        await context.SaveChangesAsync();
                    }
                    else
                    {
                        // If project is not found
                        return false;
                    }

                }
            }
            catch(Exception ex)
            {
                return false;
                throw new Exception(ex.Message);
            }

            // If everything worked good
            return true;
        }
        
    }

}
