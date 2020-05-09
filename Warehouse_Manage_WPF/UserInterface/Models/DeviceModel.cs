using DataAccess.DataAcc;
using System.Threading.Tasks;
using DataAccess.Entities;
using Warehouse_Manage_WPF.UserInterface.Helpers;

namespace Warehouse_Manage_WPF.UserInterface.Models
{
    public class DeviceModel : IDeviceEntityConversion
    {
        private IProducerAccess prodAcces;

        public int Id { get; set; }

        public string Name { get; set; }

        public string ArticleNumber { get; set; }

        public string ProducerName { get; set; }

        public string Location { get; set; }

        public int Quantity { get; set; }

        public int ProjectId { get; set; }


        public DeviceModel(Device device, IProducerAccess producerAccess)
        {
            Id = device.Id;
            Name = device.Name;
            ArticleNumber = device.ArticleNumber;
            ProducerName = device.Producer.Name;
            Location = device.Location;
            Quantity = device.Quantity;
            ProjectId = device.ProjectID;
            prodAcces = producerAccess;
        }

        public DeviceModel(IProducerAccess producerAccess)
        {
            prodAcces = producerAccess;
        }

        public DeviceModel(DeviceModel deviceModel)
        {
            Id = deviceModel.Id;
            Name = deviceModel.Name;
            ArticleNumber = deviceModel.ArticleNumber;
            ProducerName = deviceModel.ProducerName;
            Location = deviceModel.Location;
            Quantity = deviceModel.Quantity;
            ProjectId = deviceModel.ProjectId;
            prodAcces = deviceModel.prodAcces;
        }

        public async Task<Device> ConvertToDeviceEntity(bool clearIDs = false)
        {
            Device DeviceEntity = null;

            if(clearIDs)
            {
                DeviceEntity = new Device
                {
                    Name = this.Name,
                    ArticleNumber = this.ArticleNumber,
                    ProducerID = await this.GetProducerId(this.ProducerName),
                    Location = this.Location,
                    Quantity = this.Quantity
                };
            }
            else
            {
                DeviceEntity = new Device
                {
                    Id = this.Id,
                    Name = this.Name,
                    ArticleNumber = this.ArticleNumber,
                    ProducerID = await this.GetProducerId(this.ProducerName),
                    Location = this.Location,
                    Quantity = this.Quantity,
                    ProjectID = this.ProjectId
                };
            }

            return DeviceEntity;
        }

        public async Task<int> GetProducerId(string Name)
        {
            int Id = await prodAcces.GetProducerId(this.ProducerName);

            return Id;
        }
    }
}
