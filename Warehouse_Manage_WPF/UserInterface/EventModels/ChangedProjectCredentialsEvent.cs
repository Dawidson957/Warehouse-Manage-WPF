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
