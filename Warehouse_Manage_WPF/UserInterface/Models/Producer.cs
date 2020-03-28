using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse_Manage_WPF.UserInterface.Models
{
    public class Producer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string URL { get; set; }

        public ICollection<Device> Devices { get; set; }

        public Producer(Entities.Producer producer)
        {
            Id = producer.Id;
            Name = producer.Name;
            URL = producer.URL;
        }

    }
}
