using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using DataAccess.DataAcc;
using DataAccess.Entities;
using Moq;
using Warehouse_Manage_WPF.UserInterface.Models;
using Warehouse_Manage_WPF.UserInterface.ViewModels;
using Xunit;

namespace Warehouse_Manage_WPF.Tests.UserInterfaceTests.ViewModelsTests
{
    public class ProjectViewModel_Tests
    {
		private SimpleContainer _container { get; set; }

		private IProjectAccess _projectAccess { get; set; }

		private IDeviceAccess _deviceAccess { get; set; }

		private ICustomerAccess _customerAccess { get; set; }

		private IProducerAccess _producerAccess { get; set; }

		private IWindowManager _windowManager { get; set; }

		private IEventAggregator _eventAggregator { get; set; }


		public ProjectViewModel_Tests()
		{
			// Container Configuration
			_container = new SimpleContainer();
			_container.RegisterPerRequest(typeof(ProjectNewDeviceViewModel), typeof(ProjectNewDeviceViewModel).ToString(), typeof(ProjectNewDeviceViewModel));
			_container.RegisterPerRequest(typeof(ProjectAddDevicesFromWarehouseViewModel), typeof(ProjectAddDevicesFromWarehouseViewModel).ToString(), typeof(ProjectAddDevicesFromWarehouseViewModel));
			_container.RegisterPerRequest(typeof(ProjectDetailsViewModel), typeof(ProjectDetailsViewModel).ToString(), typeof(ProjectDetailsViewModel));
			_container.RegisterPerRequest(typeof(ProjectListViewModel), typeof(ProjectListViewModel).ToString(), typeof(ProjectListViewModel));
			_container.RegisterPerRequest(typeof(DeviceDetailsViewModel), typeof(DeviceDetailsViewModel).ToString(), typeof(DeviceDetailsViewModel));

			// IProjectAccess Configuration
			var projectAccessMock = new Mock<IProjectAccess>();
			var project = GenerateProject();
			projectAccessMock
				.Setup(x => x.GetProjectById(It.IsAny<int>()))
				.ReturnsAsync(project);

			_projectAccess = projectAccessMock.Object;

			// IDeviceAccess Configuration
			var deviceAccessMock = new Mock<IDeviceAccess>();
			var devices = GenerateDeviceList();
			deviceAccessMock
				.Setup(x => x.GetDevicesAll(It.IsAny<int>()))
				.ReturnsAsync(devices);

			_deviceAccess = deviceAccessMock.Object;

			// IProducerAccess Configuration
			var producerAccessMock = new Mock<IProducerAccess>();
			_producerAccess = producerAccessMock.Object;

			// ICustomerAccess Configuration
			var customerAccessMock = new Mock<ICustomerAccess>();
			_customerAccess = customerAccessMock.Object;

			// IWindowManager Configuration
			var windowManagerMock = new Mock<IWindowManager>();
			_windowManager = windowManagerMock.Object;

			// IEventAggregator Configuration
			var eventAggregatorMock = new Mock<IEventAggregator>();
			_eventAggregator = eventAggregatorMock.Object;
		}

		[Fact]
		public async void LoadProject_Test()
		{
			// Arrange
			var sampleProjectId = 3;
			var project = GenerateProject();
			var devices = GenerateDeviceList();
			var ProjectVM = new ProjectViewModel(_container, _eventAggregator, _windowManager, _projectAccess, _deviceAccess, _customerAccess, _producerAccess);

			// Act
			await ProjectVM.LoadProject(sampleProjectId);

			// Assert

			// Check ProjectModel in ViewModel
			Assert.IsType<ProjectModel>(ProjectVM.projectModel);
			Assert.NotNull(ProjectVM.projectModel);
			Assert.Equal(project.Id, ProjectVM.projectModel.Id);
			Assert.Equal(project.Name, ProjectVM.projectModel.Name);
			Assert.Equal(project.Status, ProjectVM.projectModel.Status);
			Assert.Equal(project.Comment, ProjectVM.projectModel.Comment);
			Assert.Equal(project.Customer.Name, ProjectVM.projectModel.CustomerName);

			// Check Loading Devices
			Assert.IsType<BindableCollection<DeviceModel>>(ProjectVM.ProjectDevices);
			Assert.NotEmpty(ProjectVM.ProjectDevices);
			Assert.Equal(devices.Count, ProjectVM.ProjectDevices.Count);

			// Check Unpacking ProjectModel
			Assert.Equal(project.Customer.Name, ProjectVM.CustomerName);
			Assert.Equal(project.Status, ProjectVM.ProjectStatus);
			Assert.Equal(project.Comment, ProjectVM.Comment);
		}

		[Fact]
		public void LoadDevices_EmptyList_Test()
		{
			/*
			 * Case when project device list is empty (DataAPI returns null)
			 */

			// Arrange
			var deviceAccessMock = new Mock<IDeviceAccess>();
			List<Device> devices = null;
			deviceAccessMock
				.Setup(x => x.GetDevicesAll(It.IsAny<int>()))
				.ReturnsAsync(devices);

			var deviceAccess = deviceAccessMock.Object;
			var ProjectVM = new ProjectViewModel(_container, _eventAggregator, _windowManager, _projectAccess, deviceAccess, _customerAccess, _producerAccess);
			ProjectVM.projectModel = new ProjectModel(_customerAccess) { Id = 3 }; // LoadDevices() needs only ProjectId 

			// Act
			ProjectVM.LoadDevices_Run();

			// Assert
			Assert.IsType<BindableCollection<DeviceModel>>(ProjectVM.ProjectDevices);
			Assert.Empty(ProjectVM.ProjectDevices);
		}

		private Project GenerateProject()
		{
			Project project = new Project
			{
				Id = 2,
				Name = "sampleProject",
				Status = "Open",
				Comment = "sampleComment",
				CustomerID = 13,
				Customer = new Customer
				{
					Id = 2,
					Name = "sampleCustomerName",
					Address = "sampleAddress",
					City = "sampleCity"
				}
			};

			return project;
		}

		private List<Device> GenerateDeviceList()
		{
			List<Device> devices = new List<Device>
			{
				new Device
				{
					Id = 1,
					Name = "sampleName1",
					ArticleNumber = "sampleArticleNumber1",
					Quantity = 4,
					Location = "A1",
					ProducerID = 1,
					Producer = new Producer
					{
						Id = 1,
						Name = "sampleProducerName1",
						URL = "sampleURL1"
					},
					ProjectID = 1
				},
				new Device
				{
					Id = 2,
					Name = "sampleName2",
					ArticleNumber = "sampleArticleNumber2",
					Quantity = 14,
					Location = "A2",
					ProducerID = 2,
					Producer = new Producer
					{
						Id = 2,
						Name = "sampleProducerName2",
						URL = "sampleURL2"
					},
					ProjectID = 1
				},
			};

			return devices;
		}
	}
}
