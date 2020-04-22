using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Manage_WPF.Entities;
using Warehouse_Manage_WPF.EntityModel;

namespace DataAccess.DataAcc
{
    public class DeviceAccess
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
            catch(Exception)
            {
                return false;
            }

            return true;
        }
    }
}
