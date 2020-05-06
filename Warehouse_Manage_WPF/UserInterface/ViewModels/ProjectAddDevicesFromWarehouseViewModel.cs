using Caliburn.Micro;
using DataAccess.DataAcc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Warehouse_Manage_WPF.UserInterface.Models;

namespace Warehouse_Manage_WPF.UserInterface.ViewModels
{
    public class ProjectAddDevicesFromWarehouseViewModel : Screen
    {
        private SimpleContainer _container { get; set; }

        private IDeviceAccess _deviceAccess { get; set; }

        private IProducerAccess _producerAccess { get; set; }

        private IWindowManager _windowManager { get; set; }

        private int _projectId { get; set; }

        private const int _warehouseProjectId = 5;


        public ProjectAddDevicesFromWarehouseViewModel(SimpleContainer container, IWindowManager windowManager, IDeviceAccess deviceAccess, IProducerAccess producerAccess)
        {
            _container = container;
            _windowManager = windowManager;
            _deviceAccess = deviceAccess;
            _producerAccess = producerAccess;
            InitializeComboBox();
        }

        
        #region Window Operations

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadWarehouseDevices(_warehouseProjectId);
        }

        private async Task LoadWarehouseDevices(int projectId)
        {
            var devices = await _deviceAccess.GetDevicesAll(projectId);
            WarehouseDevices = new BindableCollection<DeviceModel>();

            foreach (var device in devices)
                WarehouseDevices.Add(new DeviceModel(device, _producerAccess));
        }

        public async Task LoadProjectDevices(int projectId)
        {
            var devices = await _deviceAccess.GetDevicesAll(projectId);
            ProjectDevices = new BindableCollection<DeviceModel>();
            _projectId = projectId;

            foreach (var device in devices)
                ProjectDevices.Add(new DeviceModel(device, _producerAccess));
        }

        #endregion


        #region Top Panel

        public BindableCollection<KeyValuePair<int, string>> comboSource { get; set; }

        private BindableCollection<int> _quantity;


        private int _selectedQuantity;

        public int SelectedQuantity
        {
            get { return _selectedQuantity; }
            set 
            { 
                _selectedQuantity = value;
                NotifyOfPropertyChange(() => SelectedQuantity);
                NotifyOfPropertyChange(() => CanMoveDevice);
            }
        }


        public BindableCollection<int> Quantity
        {
            get { return _quantity; }
            set 
            { 
                _quantity = value;
                NotifyOfPropertyChange(() => Quantity);
            }
        }

        private void setQuantity()
        {
            int maxQuantity = 1;

            if(ComboSelectedItem.Key == 1)
            {
                // Set quantity on warehouse selected item
                try
                {
                    maxQuantity = WarehouseSelectedDevice.Quantity;
                }
                catch(NullReferenceException) { }
                
            }
            else if(ComboSelectedItem.Key == 2)
            {
                // Set quantity to project selected item
                try
                {
                    maxQuantity = ProjectSelectedDevice.Quantity;
                }
                catch (NullReferenceException) { }
            }
            else
            {
                Quantity = new BindableCollection<int>();
                return;
            }

            Quantity = new BindableCollection<int>();

            for (int i = 1; i <= maxQuantity; i++)
                Quantity.Add(i);
        }

        private void InitializeComboBox()
        {
            comboSource = new BindableCollection<KeyValuePair<int, string>>();
            comboSource.Add(new KeyValuePair<int, string>(1, "Magazyn -> Projekt"));
            comboSource.Add(new KeyValuePair<int, string>(2, "Projekt -> Magazyn"));
        }

        private KeyValuePair<int, string> _comboSelectedItem;

        public KeyValuePair<int, string> ComboSelectedItem
        {
            get 
            { 
                return _comboSelectedItem; 
            }
            set 
            { 
                _comboSelectedItem = value;
                NotifyOfPropertyChange(() => ComboSelectedItem);
                NotifyOfPropertyChange(() => CanMoveDevice);
            }
        }

        public bool CanMoveDevice
        {
            get
            {
                bool output = false;

                if((!ComboSelectedItem.Key.Equals(default(ComboBoxItem)) || !ComboSelectedItem.Value.Equals(default(ComboBoxItem))) && 
                    SelectedQuantity > 0 &&
                    (ProjectSelectedDevice != null || WarehouseSelectedDevice != null))
                {
                    output = true;
                }

                return output;
            }
        }

        public async void MoveDevice()
        {
            bool updateResult = false;
            bool addResult = false;

            if(ComboSelectedItem.Key == 1)
            {
                // Move device from warehouse to project
                if(WarehouseSelectedDevice != null)
                {
                    var warehouseDevice = new DeviceModel(WarehouseSelectedDevice);
                    var warehouseDeviceEntity = await warehouseDevice.ConvertToDeviceEntity();

                    var projectDevice = new DeviceModel(WarehouseSelectedDevice);
                    var projectDeviceEntity = await warehouseDevice.ConvertToDeviceEntity(true);

                    warehouseDeviceEntity.Quantity -= SelectedQuantity;
                    projectDeviceEntity.Quantity = SelectedQuantity;

                    updateResult = await _deviceAccess.UpdateDevice(warehouseDeviceEntity);
                    addResult = await _deviceAccess.AddDeviceToProject(_projectId, projectDeviceEntity);

                }
            }
            else if (ComboSelectedItem.Key == 2)
            {
                // Mode device from project to warehouse
                if(ProjectSelectedDevice != null)
                {
                    var projectDevice = new DeviceModel(ProjectSelectedDevice);
                    var projectDeviceEntity = await projectDevice.ConvertToDeviceEntity();

                    var warehouseDevice = new DeviceModel(ProjectSelectedDevice);
                    var warehouseDeviceEntity = await warehouseDevice.ConvertToDeviceEntity(true);

                    warehouseDeviceEntity.Quantity += SelectedQuantity;
                    projectDeviceEntity.Quantity -= SelectedQuantity;

                    updateResult = await _deviceAccess.UpdateDevice(projectDeviceEntity);
                    addResult = await _deviceAccess.AddDeviceToProject(_warehouseProjectId, warehouseDeviceEntity);
                }
            }
            
            // Check the result of exchange
            if(updateResult && addResult)
            {
                await LoadProjectDevices(_projectId);
                await LoadWarehouseDevices(_warehouseProjectId);
            }
            else
            {
                MessageBox.Show("An error occured during exchanging devices");
            }
        }

        #endregion


        #region Warehouse DataGrid

        private BindableCollection<DeviceModel> _warehouseDevices;
        private DeviceModel _warehouseSelectedDevice;

        
        public BindableCollection<DeviceModel> WarehouseDevices
        {
            get { return _warehouseDevices; }
            set 
            { 
                _warehouseDevices = value;
                NotifyOfPropertyChange(() => WarehouseDevices);
            }
        }

        public DeviceModel WarehouseSelectedDevice
        {
            get { return _warehouseSelectedDevice; }
            set 
            {
                _warehouseSelectedDevice = value;
                NotifyOfPropertyChange(() => WarehouseSelectedDevice);
                setQuantity();
            }
        }

        public void WarehouseDevice_DoubleClick()
        {
            if(WarehouseSelectedDevice != null)
            {
                var DeviceDetailsVM = _container.GetInstance<DeviceDetailsViewModel>();
                DeviceDetailsVM.LoadDevice(WarehouseSelectedDevice);
                _windowManager.ShowDialog(DeviceDetailsVM);
            }
        }

        #endregion


        #region Project DataGrid

        private BindableCollection<DeviceModel> _projectDevices;
        private DeviceModel _projectSelectedDevice;


        public BindableCollection<DeviceModel> ProjectDevices
        {
            get { return _projectDevices; }
            set 
            { 
                _projectDevices = value;
                NotifyOfPropertyChange(() => ProjectDevices);
            }
        }

        public DeviceModel ProjectSelectedDevice
        {
            get { return _projectSelectedDevice; }
            set 
            { 
                _projectSelectedDevice = value;
                NotifyOfPropertyChange(() => ProjectSelectedDevice);
                setQuantity();
            }
        }

        public void ProjectDevice_DoubleClick()
        {
            if(ProjectSelectedDevice != null)
            {
                var DeviceDetailsVM = _container.GetInstance<DeviceDetailsViewModel>();
                DeviceDetailsVM.LoadDevice(ProjectSelectedDevice);
                _windowManager.ShowDialog(DeviceDetailsVM);
            }
        }

        #endregion
    }
}
