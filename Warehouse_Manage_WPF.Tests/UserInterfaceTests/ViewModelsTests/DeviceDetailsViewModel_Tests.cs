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
    public class DeviceDetailsViewModel_Tests
    {
        private IProducerAccess _producerAccess { get; set; }

        private IDeviceAccess _deviceAccess { get; set; }

        private IEventAggregator _eventAggregator { get; set; }


        public DeviceDetailsViewModel_Tests()
        {
            var producerNames = GenerateProducersNameList();

            var producerAccessMock = new Mock<IProducerAccess>();
            producerAccessMock
                .Setup(x => x.GetProducerNamesAll())
                .ReturnsAsync(producerNames);
            producerAccessMock
                .Setup(x => x.GetProducerId(It.IsAny<string>()))
                .ReturnsAsync(2);

            _producerAccess = producerAccessMock.Object;

            var deviceAccessMock = new Mock<IDeviceAccess>();
            deviceAccessMock
                .Setup(x => x.UpdateDevice(It.IsAny<Device>()))
                .ReturnsAsync(true);
            deviceAccessMock
                .Setup(x => x.DeleteDevice(It.IsAny<int>()))
                .ReturnsAsync(true);

            _deviceAccess = deviceAccessMock.Object;

            _eventAggregator = new EventAggregator();
        }


        [Fact]
        public void LoadDevice_Test()
        {
            // Arrange
            DeviceModel deviceModel = new DeviceModel(_producerAccess)
            {
                Id = 76,
                Name = "Panel KTP900 Basic",
                ArticleNumber = "DSF98-DS9F8-DS98F",
                ProducerName = "SIEMENS",
                Location = "A2",
                Quantity = 13,
                ProjectId = 5
            };

            var deviceDetailsVM = new DeviceDetailsViewModel(_eventAggregator, _producerAccess, _deviceAccess);

            // Act
            deviceDetailsVM.LoadDevice(deviceModel);

            // Assert
            Assert.Equal(deviceModel.Id, deviceDetailsVM.Device.Id);
            Assert.Equal(deviceModel.Name, deviceDetailsVM.Device.Name);
            Assert.Equal(deviceModel.ArticleNumber, deviceDetailsVM.Device.ArticleNumber);
            Assert.Equal(deviceModel.ProducerName, deviceDetailsVM.Device.ProducerName);
            Assert.Equal(deviceModel.Location, deviceDetailsVM.Device.Location);
            Assert.Equal(deviceModel.Quantity, deviceDetailsVM.Device.Quantity);
            Assert.Equal(deviceModel.ProjectId, deviceDetailsVM.Device.ProjectId);
        }

        [Fact]
        public void CreateViewModel_Test()
        {
            // Arrange
            List<string> producersNameList = GenerateProducersNameList();

            // Act
            var deviceDetailsVM = new DeviceDetailsViewModel(_eventAggregator, _producerAccess, _deviceAccess);
            deviceDetailsVM.LoadProducersRun();

            // Assert
            Assert.IsType<BindableCollection<string>>(deviceDetailsVM.Producers);
            Assert.Equal(producersNameList.Count, deviceDetailsVM.Producers.Count);
            Assert.Equal(producersNameList[0], deviceDetailsVM.Producers[0]);
            Assert.Equal(producersNameList[3], deviceDetailsVM.Producers[3]);
            
        }

        [Fact]
        public async void SubmitButton_Test()
        {
            // Arrange
            DeviceModel deviceModel = new DeviceModel(_producerAccess)
            {
                Id = 76,
                Name = "Panel KTP900 Basic",
                ArticleNumber = "DSF98-DS9F8-DS98F",
                ProducerName = "SIEMENS",
                Location = "A2",
                Quantity = 13,
                ProjectId = 5
            };

            var deviceDetailsVM = new DeviceDetailsViewModel(_eventAggregator, _producerAccess, _deviceAccess);
            deviceDetailsVM.Device = deviceModel;
            deviceDetailsVM.LoadProducersRun();

            // Act
            await deviceDetailsVM.SubmitButton();
            
            // Assert
            Assert.True(deviceDetailsVM.DeviceValidationResult);
            Assert.True(deviceDetailsVM.DeviceUpdateResult);
        }

        [Fact]
        public void DeleteButton_Test()
        {
            // Arrange
            DeviceModel deviceModel = new DeviceModel(_producerAccess)
            {
                Id = 76,
                Name = "Panel KTP900 Basic",
                ArticleNumber = "DSF98-DS9F8-DS98F",
                ProducerName = "SIEMENS",
                Location = "A2",
                Quantity = 13,
                ProjectId = 5
            };

            var deviceDetailsVM = new DeviceDetailsViewModel(_eventAggregator, _producerAccess, _deviceAccess);
            deviceDetailsVM.Device = deviceModel;
            deviceDetailsVM.LoadProducersRun();

            // Act
            deviceDetailsVM.DeleteButton();
            
            // Assert
            Assert.True(deviceDetailsVM.DeviceDeleteResult);
        }


        private List<string> GenerateProducersNameList()
        {
            List<string> producersName = new List<string>()
            {
                "FESTO",
                "SIEMENS",
                "PILZ",
                "ABB",
                "WEIDMULLER",
                "SICK",
                "ALLEN-BRADLEY"
            };

            return producersName;
        }
    }

}
