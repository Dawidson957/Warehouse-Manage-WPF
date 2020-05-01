using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace Warehouse_Manage_WPF.UserInterface.Helpers
{
    interface IDeviceEntityConversion
    {
        Task<Device> ConvertToDeviceEntity(bool clearIDs = false);

        Task<int> GetProducerId(string Name);
    }
}
