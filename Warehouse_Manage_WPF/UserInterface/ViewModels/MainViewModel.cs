using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Warehouse_Manage_WPF.UserInterface.Models;
using Warehouse_Manage_WPF.DataAccess;

namespace Warehouse_Manage_WPF.UserInterface.ViewModels
{
    public class MainViewModel : Conductor<object>
    {
        public BindableCollection<Device> DevicesDataGrid { get; set; }

        private DataAPI _dataAPI { get; set; }

        public MainViewModel()
        {
            _dataAPI = new DataAPI();
            LoadDevices();

        }

        private void LoadDevices()
        {
            DevicesDataGrid = new BindableCollection<Device>();
            var devicesFromAPI = _dataAPI.GetAllDevices();

            foreach (var device in devicesFromAPI)
                DevicesDataGrid.Add(new Device(device));
        }
    }
}
