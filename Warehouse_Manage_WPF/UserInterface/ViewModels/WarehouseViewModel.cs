using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using Warehouse_Manage_WPF.DataAccess;
using Warehouse_Manage_WPF.UserInterface.Models;

namespace Warehouse_Manage_WPF.UserInterface.ViewModels
{
    public class WarehouseViewModel : Screen
    {
        private DataAPI _dataAPI { get; set; }

        private SimpleContainer _simpleContainer { get; set; }

        private IWindowManager _windowManager { get; set; }

        
        public BindableCollection<Device> Devices { get; set; }

        public Device SelectedDevice { get; set; }


        public WarehouseViewModel(SimpleContainer simpleContainer, IWindowManager windowManager)
        {
            _simpleContainer = simpleContainer;
            _windowManager = windowManager;
            _dataAPI = new DataAPI();
            LoadDevices();
        }

        private void LoadDevices()
        {
            Devices = new BindableCollection<Device>();
            var devicesFromAPI = _dataAPI.GetAllDevices();

            foreach (var device in devicesFromAPI)
                Devices.Add(new Device(device));
        }

        public void MouseDoubleClick_DataGrid()
        {
            if (SelectedDevice != null)
            {
                DeviceDetailsViewModel deviceDetailsVM = _simpleContainer.GetInstance<DeviceDetailsViewModel>();
                deviceDetailsVM.LoadDevice(SelectedDevice);
                _windowManager.ShowDialog(deviceDetailsVM);
            }
            else
                MessageBox.Show("No device found.");
        }
    }
}
