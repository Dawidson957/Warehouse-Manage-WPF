using DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.DataAcc
{
    public interface IDeviceAccess
    {
        Task<bool> AddDevice(Device device);
        Task<bool> AddDeviceToProject(int projectId, Device device);
        Task<bool> DeleteDevice(int Id);
        Task<List<Device>> GetDevicesAll(int projectId);
        Task<bool> UpdateDevice(Device device);
    }
}