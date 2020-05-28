using Caliburn.Micro;
using DataAccess.DataAcc;
using System.Threading.Tasks;
using System.Windows;
using Warehouse_Manage_WPF.UserInterface.EventModels;
using Warehouse_Manage_WPF.UserInterface.Models;

namespace Warehouse_Manage_WPF.UserInterface.ViewModels
{
	public class ProjectViewModel : Screen, IHandle<AddedNewDeviceToProjectEvent>, IHandle<DeviceCredentialsChangedEvent>, IHandle<ChangedProjectCredentialsEvent>
	{
		private SimpleContainer _container { get; set; }

		private IProjectAccess _projectAccess { get; set; }

		private IDeviceAccess _deviceAccess { get; set; }

		private ICustomerAccess _customerAccess { get; set; }

		private IProducerAccess _producerAccess { get; set; }

		private IWindowManager _windowManager { get; set; }


		public ProjectViewModel(SimpleContainer simpleContainer, IEventAggregator eventAggregator, IWindowManager windowManager,
			IProjectAccess projectAccess, IDeviceAccess deviceAccess, ICustomerAccess customerAccess, IProducerAccess producerAccess)
		{
			_container = simpleContainer;
			_projectAccess = projectAccess;
			_deviceAccess = deviceAccess;
			_customerAccess = customerAccess;
			_producerAccess = producerAccess;

			_windowManager = windowManager;
			eventAggregator.Subscribe(this);
		}


		#region Window Operations

		public async Task LoadProject(int projectId)
		{
			var project = await _projectAccess.GetProjectById(projectId);

			if (project != null)
			{
				projectModel = new ProjectModel(project, _customerAccess);

				CustomerName = projectModel.CustomerName;
				ProjectStatus = projectModel.Status;
				Comment = projectModel.Comment;

				await LoadDevices();
			}
			else
			{
				MessageBox.Show("Podany projekt nie istnieje");
			}
		}

		private async Task LoadDevices()
		{
			var devices = await _deviceAccess.GetDevicesAll(projectModel.Id);
			ProjectDevices = new BindableCollection<DeviceModel>();

			foreach (var device in devices)
				ProjectDevices.Add(new DeviceModel(device, _producerAccess));
		}

		#endregion


		#region Project

		private ProjectModel _project { get; set; }

		public ProjectModel projectModel
		{
			get { return _project; }
			set
			{
				_project = value;
				NotifyOfPropertyChange(() => projectModel);
			}
		}

		#endregion


		#region PopUp Menu

		public void AddNewDevice()
		{
			var ProjectNewDeviceVM = _container.GetInstance<ProjectNewDeviceViewModel>();
			ProjectNewDeviceVM.SetProjectId(projectModel.Id);
			_windowManager.ShowDialog(ProjectNewDeviceVM);
		}

		public async void ExchangeDevices()
		{
			var ProjectAddDeviesFromWarehouseVM = _container.GetInstance<ProjectAddDevicesFromWarehouseViewModel>();
			await ProjectAddDeviesFromWarehouseVM.LoadProjectDevices(projectModel.Id);
			_windowManager.ShowDialog(ProjectAddDeviesFromWarehouseVM);
		}

		public void EditProject()
		{
			var projectDetailsVM = _container.GetInstance<ProjectDetailsViewModel>();
			projectDetailsVM.LoadProject(projectModel);
			_windowManager.ShowDialog(projectDetailsVM);
		}

		public void CloseProject()
		{
			var mainVM = (MainViewModel)this.Parent;
			var projectListVM = _container.GetInstance<ProjectListViewModel>();
			mainVM.ActivateItem(projectListVM);
		}

		#endregion


		#region Project Information Card

		private string _customerName;
		private string _projectStatus;
		private string _comment;

		public string CustomerName
		{
			get { return _customerName; }
			set
			{
				_customerName = value;
				NotifyOfPropertyChange(() => CustomerName);
			}
		}

		public string ProjectStatus
		{
			get { return _projectStatus; }
			set
			{
				_projectStatus = value;
				NotifyOfPropertyChange(() => ProjectStatus);
			}
		}

		public string Comment
		{
			get { return _comment; }
			set
			{
				_comment = value;
				NotifyOfPropertyChange(() => Comment);
			}
		}

		#endregion


		#region Devices List Card

		private BindableCollection<DeviceModel> _projectDevices;
		private DeviceModel _selectedDevice;


		public BindableCollection<DeviceModel> ProjectDevices
		{
			get { return _projectDevices; }
			set
			{
				_projectDevices = value;
				NotifyOfPropertyChange(() => ProjectDevices);
			}
		}

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
			if (SelectedDevice != null)
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

		public async void Handle(DeviceCredentialsChangedEvent deviceCredentialsChanged)
		{
			await LoadDevices();
		}

		public async void Handle(ChangedProjectCredentialsEvent changedProjectCredentialsEvent)
		{
			await LoadProject(changedProjectCredentialsEvent.ProjectId);
		}

		#endregion


		#region Only For Testing

		public async void LoadDevices_Run() { await LoadDevices(); }

		#endregion

	}
}

