using System.Collections.Generic;

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
