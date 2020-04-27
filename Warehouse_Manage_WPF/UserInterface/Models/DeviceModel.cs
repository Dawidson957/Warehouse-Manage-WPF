using DataAccess.DataAcc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;
using Warehouse_Manage_WPF.UserInterface.Helpers;

namespace Warehouse_Manage_WPF.UserInterface.Models
{
    public class DeviceModel : IDeviceEntityConversion
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ArticleNumber { get; set; }

        public string ProducerName { get; set; }

        public string Location { get; set; }

        public int Quantity { get; set; }

        public int ProjectId { get; set; }

        public async Task<Device> ConvertToDeviceEntity()
        {
            Device DeviceEntity = new Device
            {
                Id = this.Id,
                Name = this.Name,
                ArticleNumber = this.ArticleNumber,
                ProducerID = await this.GetProducerId(this.ProducerName),
                Location = this.Location,
                Quantity = this.Quantity,
                ProjectID = this.ProjectId
            };

            return DeviceEntity;
        }

        public async Task<int> GetProducerId(string Name)
        {
            var producerAcces = new ProducerAccess();
            int Id = await producerAcces.GetProducerId(this.ProducerName);

            return Id;
        }

        public DeviceModel(Device device)
        {
            Id = device.Id;
            Name = device.Name;
            ArticleNumber = device.ArticleNumber;
            ProducerName = device.Producer.Name;
            Location = device.Location;
            Quantity = device.Quantity;
            ProjectId = device.ProjectID;
        }

        public DeviceModel()
        {

        }
    }
}
