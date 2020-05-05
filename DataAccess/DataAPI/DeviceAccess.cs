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
    public class DeviceAccess : IDeviceAccess
    {

        public async Task<bool> AddDevice(Device device)
        {
            try
            {
                using (var context = new WarehouseModel())
                {
                    var deviceExists = context.Devices.FirstOrDefault(x => x.ArticleNumber == device.ArticleNumber);

                    if (deviceExists != null)
                    {
                        deviceExists.Quantity += device.Quantity;
                    }
                    else
                    {
                        context.Devices.Add(device);
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<List<Device>> GetDevicesAll(int projectId)
        {
            List<Device> devices = null;

            try
            {
                using (var context = new WarehouseModel())
                {
                    devices = await context.Devices
                        .Include(x => x.Producer)
                        .Where(x => x.ProjectID == projectId).ToListAsync<Device>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return devices;
        }

        public async Task<bool> UpdateDevice(Device device)
        {
            try
            {
                using (var context = new WarehouseModel())
                {
                    var existingDevice = await context.Devices.FirstOrDefaultAsync(x => x.Id == device.Id);

                    if (existingDevice != null)
                    {
                        existingDevice.Name = device.Name;
                        existingDevice.ArticleNumber = device.ArticleNumber;
                        existingDevice.Location = device.Location;
                        existingDevice.Quantity = device.Quantity;
                        existingDevice.ProjectID = device.ProjectID;

                        if (existingDevice.ProducerID != device.ProducerID)
                        {
                            existingDevice.ProducerID = device.ProducerID;
                        }

                        await context.SaveChangesAsync();
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteDevice(int Id)
        {
            try
            {
                using (var context = new WarehouseModel())
                {
                    var existingDevice = context.Devices.FirstOrDefault(x => x.Id == Id);

                    if (existingDevice != null)
                    {
                        context.Devices.Remove(existingDevice);

                        await context.SaveChangesAsync();
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> AddDeviceToProject(int projectId, Device device)
        {
            try
            {
                using (var context = new WarehouseModel())
                {

                    var project = context.Projects.Include(x => x.Devices).FirstOrDefault(x => x.Id == projectId);

                    if (project != null)
                    {
                        var deviceFromProject = project.Devices.FirstOrDefault(x => x.ArticleNumber == device.ArticleNumber);

                        if (deviceFromProject != null)
                        {
                            deviceFromProject.Quantity += device.Quantity;
                        }
                        else
                        {
                            project.Devices.Add(device);
                        }

                        await context.SaveChangesAsync();
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

    }
}
