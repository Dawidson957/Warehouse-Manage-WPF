using Caliburn.Micro;
using DataAccess.DataAcc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Manage_WPF.UserInterface.EventModels;
using Warehouse_Manage_WPF.UserInterface.Models;

namespace Warehouse_Manage_WPF.UserInterface.ViewModels
{
    public class ProjectViewModel : Screen, IHandle<AddedNewDeviceToProjectEvent>
    {
		
		private ProjectModel _project;
		private ProjectAccess _projectAccess;
		private DeviceAccess _deviceAccess;
		private IWindowManager _windowManager;
		private SimpleContainer _container;

		public ProjectModel projectModel
		{
			get { return _project; }
			set
			{
				_project = value;
				NotifyOfPropertyChange(() => projectModel);
			}
		}

		public ProjectViewModel(IEventAggregator eventAggregator, IWindowManager windowManager, SimpleContainer simpleContainer)
		{
			_projectAccess = new ProjectAccess();
			_deviceAccess = new DeviceAccess();
			_windowManager = windowManager;
			_container = simpleContainer;
			eventAggregator.Subscribe(this);
		}

		protected override async void OnViewLoaded(object view)
		{
			base.OnViewLoaded(view);
			await LoadDevices();
		}

		public void LoadProject(ProjectModel projectModel)
		{
			_project = projectModel;
		}

		private async Task LoadDevices()
		{
			var devices = await _deviceAccess.GetDevicesAll(this._project.Id);
			ProjectDevices = new BindableCollection<DeviceModel>();

			foreach (var device in devices)
				ProjectDevices.Add(new DeviceModel(device));
		}


        #region PopUp Menu

		public void AddNewDevice()
		{
			var ProjectNewDeviceVM = _container.GetInstance<ProjectNewDeviceViewModel>();
			ProjectNewDeviceVM.SetProjectId(this._project.Id);
			_windowManager.ShowDialog(ProjectNewDeviceVM);
		}

		#endregion



		#region Project Information Card



		#endregion

		#region Devices List Card

		private BindableCollection<DeviceModel> _projectDevices;

		public BindableCollection<DeviceModel> ProjectDevices
		{
			get { return _projectDevices; }
			set
			{
				_projectDevices = value;
				NotifyOfPropertyChange(() => ProjectDevices);
			}
		}

		private DeviceModel _selectedDevice;

		public DeviceModel SelectedDevice
		{
			get { return _selectedDevice; }
			set 
			{
				_selectedDevice = value;
				NotifyOfPropertyChange(() => SelectedDevice);
			}
		}

		public void MouseDoubleClick_DataGrid()
		{
			if(SelectedDevice != null)
			{
				var deviceDetailsVM = _container.GetInstance<DeviceDetailsViewModel>();
				deviceDetailsVM.LoadDevice(SelectedDevice);
				_windowManager.ShowDialog(deviceDetailsVM);
			}
		}

		#endregion

		#region Events

		public async void Handle(AddedNewDeviceToProjectEvent newDeviceAdded)
		{
			await LoadDevices();
		}

		#endregion

	}
}

