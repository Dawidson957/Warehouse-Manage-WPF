using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Manage_WPF.Entities;
using Warehouse_Manage_WPF.EntityModel;

namespace Warehouse_Manage_WPF.DataAccess
{
    public class DataAPI
    {
        private WarehouseModel context { get; set; }

        public DataAPI()
        {
            this.context = new WarehouseModel();
        }

        #region Device Entity

        public Device GetDeviceById(int Id)
        {
            return context.Devices.Include("Producer").AsNoTracking().FirstOrDefault(c => c.Id == Id);
        }

        public List<Device> GetAllDevices()
        {
            List<Device> devices = context.Devices.Include("Producer").AsNoTracking().ToList<Device>();

            return devices.Count == 0 ? null : devices;
        }

        public Device GetDeviceByArticleNumber(string ArticleNumber)
        {
            return context.Devices.Include("Producer").AsNoTracking().FirstOrDefault(c => c.ArticleNumber == ArticleNumber);
        }

        public bool AddDevice(Device device)
        {
            try
            {
                context.Devices.Add(device);
                context.SaveChanges();
            }
            catch(Exception)
            {
                return false;
            }

            return true;
        }

        public bool DeleteDevice(Device device)
        {
            var deviceToDelete = context.Devices.FirstOrDefault(c => c.Id == device.Id);

            if (deviceToDelete == null)
                return false;
            else
            {
                try
                {
                    context.Devices.Remove(deviceToDelete);
                    context.SaveChanges();
                }
                catch(Exception)
                {
                    return false;
                }

                return true;
            }
        }

        public bool UpdateDevice(Device device)
        {
            var existingDevice = context.Devices.Include("Producer").FirstOrDefault(c => c.Id == device.Id);

            if (existingDevice == null)
                return false;
            else
            {
                existingDevice.Name = device.Name;
                existingDevice.ArticleNumber = device.ArticleNumber;
                existingDevice.Location = device.Location;
                existingDevice.Quantity = device.Quantity;
                existingDevice.ProducerID = device.ProducerID;

                try
                {
                    context.SaveChanges();
                }
                catch(Exception)
                {
                    return false;
                }

                return true;
            }
        }

        #endregion
    }
}
