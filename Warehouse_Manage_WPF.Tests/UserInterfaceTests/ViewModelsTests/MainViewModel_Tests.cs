using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Warehouse_Manage_WPF.UserInterface;
using Caliburn.Micro;
using Warehouse_Manage_WPF.UserInterface.ViewModels;

namespace Warehouse_Manage_WPF.Tests.UserInterfaceTests.ViewModelsTests
{
    public class MainViewModel_Tests
    {
        private SimpleContainer _container { get; set; }

        private IWindowManager windowManager { get; set; }


        public MainViewModel_Tests()
        {
            _container = new SimpleContainer();
            _container.RegisterPerRequest(typeof(WarehouseViewModel), typeof(WarehouseViewModel).ToString(), typeof(WarehouseViewModel));
            _container.RegisterPerRequest(typeof(NewDeviceViewModel), typeof(NewDeviceViewModel).ToString(), typeof(NewDeviceViewModel));
            _container.RegisterPerRequest(typeof(ProjectListViewModel), typeof(ProjectListViewModel).ToString(), typeof(ProjectListViewModel));
            _container.RegisterPerRequest(typeof(SettingsViewModel), typeof(SettingsViewModel).ToString(), typeof(SettingsViewModel));

            windowManager = new WindowManager();
        }

        [Fact]
        public void CreateViewModel_Test()
        {
            // Act
            var mainVM = new MainViewModel(_container, windowManager);

            // Assert
            if(mainVM.IsInitialized)
            {
                Assert.IsType<WarehouseViewModel>(mainVM.ActiveItem);
            }
        }

        [Fact]
        public void WarehouseViewOpen_Test()
        {
            // Arrange
            var mainVM = new MainViewModel(_container, windowManager);

            // Act
            if (mainVM.IsInitialized)
            {
                mainVM.WarehouseViewOpen();
            }

            // Assert
            Assert.IsType<WarehouseViewModel>(mainVM.ActiveItem);
        }

        [Fact]
        public void NewDeviceViewOpen_Test()
        {
            // Arrange
            var mainVM = new MainViewModel(_container, windowManager);

            // Act
            if(mainVM.IsInitialized)
            {
                mainVM.NewDeviceViewOpen();
            }

            // Assert
            Assert.IsType<NewDeviceViewModel>(mainVM.ActiveItem);
        }

        [Fact]
        public void ProjectListViewOpen_Test()
        {
            // Arrange
            var mainVM = new MainViewModel(_container, windowManager);

            // Act
            if (mainVM.IsInitialized)
            {
                mainVM.ProjectListViewOpen();
            }

            // Assert
            Assert.IsType<ProjectListViewModel>(mainVM.ActiveItem);
        }
    }
}
