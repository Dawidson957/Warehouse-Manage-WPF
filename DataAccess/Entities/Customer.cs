using System.Collections.Generic;

namespace DataAccess.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}
