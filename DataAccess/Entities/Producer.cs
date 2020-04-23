using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Producer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string URL { get; set; }

        public virtual ICollection<Device> Devices { get; set; }
    }
}
