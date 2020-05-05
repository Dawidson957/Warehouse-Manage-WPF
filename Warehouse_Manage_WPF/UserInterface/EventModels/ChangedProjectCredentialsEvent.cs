using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse_Manage_WPF.UserInterface.EventModels
{
    public class ChangedProjectCredentialsEvent
    {
        public int ProjectId { get; set; }

        public ChangedProjectCredentialsEvent(int id)
        {
            ProjectId = id;
        }
    }
}
