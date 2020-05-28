using Caliburn.Micro;

namespace Warehouse_Manage_WPF.UserInterface.ViewModels
{
    public class SettingsViewModel : Conductor<object>
    {
        private SimpleContainer _container;

        public SettingsViewModel(SimpleContainer simpleContainer)
        {
            _container = simpleContainer;
            var customersVM = _container.GetInstance<CustomersViewModel>();
            ActivateItem(customersVM);
        }

    }
}