using Caliburn.Micro;
using DataAccess.DataAcc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Manage_WPF.UserInterface.Models;

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
