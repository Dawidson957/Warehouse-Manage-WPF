using DataAccess.DataAcc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Manage_WPF.Entities;
using Warehouse_Manage_WPF.UserInterface.Helpers;

namespace Warehouse_Manage_WPF.UserInterface.Models
{
    public class NewDeviceFormModel : IDeviceEntityConversion
    {
        public string Name { get; set; }

        public string ArticleNumber { get; set; }

        public string ProducerName { get; set; }

        public string Location { get; set; }

        public int Quantity { get; set; }

        public async Task<Device> ConvertToDeviceEntity()
        {
            Device DeviceEntity = new Device
            {
                Name = this.Name,
                ArticleNumber = this.ArticleNumber,
                ProducerID = await this.GetProducerId(this.ProducerName),
                Location = this.Location,
                Quantity = this.Quantity
            };

            return DeviceEntity;
        }

        public async Task<int> GetProducerId(string Name)
        {
            var producerAcces = new ProducerAccess();
            int Id = await producerAcces.GetProducerId(this.ProducerName);

            return Id;
        }
    }
}
