using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using DataAccess.DataAcc;
using DataAccess.Entities;
using Moq;
using Warehouse_Manage_WPF.UserInterface.ViewModels;
using Xunit;

namespace Warehouse_Manage_WPF.Tests.UserInterfaceTests.ViewModelsTests
{
    public class ProjectNewDeviceViewModel_Tests
    {
        private IProducerAccess _producerAccess { get; set; }

        private IDeviceAccess _deviceAccess { get; set; }

        private IEventAggregator _events { get; set; }

        public ProjectNewDeviceViewModel_Tests()
        {
            var producerNames = GenerateProducersNameList();

            var producerAccessMock = new Mock<IProducerAccess>();
            producerAccessMock
                .Setup(x => x.GetProducerNamesAll())
                .ReturnsAsync(producerNames);
            producerAccessMock
                .Setup(x => x.GetProducerId(It.IsAny<string>()))
                .ReturnsAsync(2);

            var deviceAccessMock = new Mock<IDeviceAccess>();
            deviceAccessMock
                .Setup(x => x.AddDevice(It.IsAny<Device>()))
                .ReturnsAsync(true);

            var eventAggregatorMock = new Mock<IEventAggregator>();

            _events = eventAggregatorMock.Object;
            _producerAccess = producerAccessMock.Object;
            _deviceAccess = deviceAccessMock.Object;
        }

        [Fact]
        public void LoadProducers_Test()
        {
            // Arrange 
            var producerNames = GenerateProducersNameList();

            // Act
            var newDeviceVM = new ProjectNewDeviceViewModel(_events, _deviceAccess, _producerAccess);
            newDeviceVM.LoadProducers_Run();

            // Assert
            Assert.IsType<BindableCollection<string>>(newDeviceVM.Producers);
            Assert.True(producerNames.Count == newDeviceVM.Producers.Count);

        }

        [Fact]
        public void SaveButton_Test()
        {
            // Arrange
            string validDeviceName = "Panel KTP900 Basic";
            string validArticleNumber = "DSF98-DS9F8-DS98F";
            string validSelectedProducerName = "SIEMENS";
            string validLocation = "A2";
            int validQuantity = 13;

            var newDeviceVM = new ProjectNewDeviceViewModel(_events, _deviceAccess, _producerAccess);
            newDeviceVM.DeviceName = validDeviceName;
            newDeviceVM.ArticleNumber = validArticleNumber;
            newDeviceVM.SelectedProducer = validSelectedProducerName;
            newDeviceVM.Location = validLocation;
            newDeviceVM.Quantity = validQuantity;

            // Act
            newDeviceVM.SaveButton();

            // Assert
            Assert.True(newDeviceVM.DeviceValidationResult);
            Assert.True(newDeviceVM.DeviceAddResult);
        }

        [Theory]
        [InlineData("", "SD98F98-DS98", "FESTO", "A2", 14)]
        [InlineData("Panel KTP900", "", "FESTO", "A2", 14)]
        [InlineData("", "", "FESTO", "A2", 14)]
        public void SaveButton_InvalidData_Test(string deviceName, string articleNumber,
            string producerName, string location, int quantity)
        {
            // Arrange
            var newDeviceVM = new ProjectNewDeviceViewModel(_events, _deviceAccess, _producerAccess);
            newDeviceVM.DeviceName = deviceName;
            newDeviceVM.ArticleNumber = articleNumber;
            newDeviceVM.SelectedProducer = producerName;
            newDeviceVM.Location = location;
            newDeviceVM.Quantity = quantity;

            // Act
            newDeviceVM.SaveButton();

            // Assert
            Assert.False(newDeviceVM.DeviceValidationResult);
            Assert.False(newDeviceVM.DeviceAddResult);
        }

        [Fact]
        public void ClearButton_Test()
        {
            // Arrange
            string validDeviceName = "Panel KTP900 Basic";
            string validArticleNumber = "DSF98-DS9F8-DS98F";
            string validSelectedProducerName = "SIEMENS";
            string validLocation = "A2";
            int validQuantity = 13;

            var newDeviceVM = new ProjectNewDeviceViewModel(_events, _deviceAccess, _producerAccess);
            newDeviceVM.DeviceName = validDeviceName;
            newDeviceVM.ArticleNumber = validArticleNumber;
            newDeviceVM.SelectedProducer = validSelectedProducerName;
            newDeviceVM.Location = validLocation;
            newDeviceVM.Quantity = validQuantity;

            // Act
            newDeviceVM.ClearButton();

            // Assert
            Assert.Equal("", newDeviceVM.DeviceName);
            Assert.Equal("", newDeviceVM.ArticleNumber);
            Assert.Equal("", newDeviceVM.Location);
            Assert.Equal(0, newDeviceVM.Quantity);
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
