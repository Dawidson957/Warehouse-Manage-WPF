using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using DataAccess.DataAcc;
using DataAccess.Entities;
using MaterialDesignThemes.Wpf;
using Moq;
using Warehouse_Manage_WPF.UserInterface.ViewModels;
using Xunit;

namespace Warehouse_Manage_WPF.Tests.UserInterfaceTests.ViewModelsTests
{
    public class NewDeviceViewModel_Tests
    {
        private IProducerAccess _producerAccess { get; set; }

        private IDeviceAccess _deviceAccess { get; set; }


        public NewDeviceViewModel_Tests()
        {
            var producerNames = GenerateProducersNameList();

            var producerAccessMock = new Mock<IProducerAccess>();
            producerAccessMock
                .Setup(x => x.GetProducerNamesAll())
                .ReturnsAsync(producerNames);

            producerAccessMock
                .Setup(x => x.AddProducer(It.IsAny<Producer>()))
                .ReturnsAsync(true);

            producerAccessMock
                .Setup(x => x.GetProducerId(It.IsAny<string>()))
                .ReturnsAsync(2);

            _producerAccess = producerAccessMock.Object;

            var deviceAccessMock = new Mock<IDeviceAccess>();
            deviceAccessMock
                .Setup(x => x.AddDevice(It.IsAny<Device>()))
                .ReturnsAsync(true);

            _deviceAccess = deviceAccessMock.Object;
        }


        [Fact]
        public void LoadProducers_Test()
        {
            // Arrange 
            var producerNames = GenerateProducersNameList();

            // Act
            var newDeviceVM = new NewDeviceViewModel(_producerAccess, _deviceAccess);
            newDeviceVM.LoadProducersRun();

            // Assert
            Assert.IsType<BindableCollection<string>>(newDeviceVM.ProducersName);
            Assert.True(producerNames.Count == newDeviceVM.ProducersName.Count);
            
        }

        [Fact]
        public void LoadSnackbar_Test()
        {
            // Act 
            var newDeviceVM = new NewDeviceViewModel(_producerAccess, _deviceAccess);

            // Assert
            Assert.IsType<Snackbar>(newDeviceVM.SnackbarNotification);
            Assert.Null(newDeviceVM.SnackbarNotification.MessageQueue);
        }

        [Fact]
        public void SaveNewProducer_Test()
        {
            // Arrange
            string validProducerName = "FESTO";
            string validProducerURL = "https://festo.com/";

            var newDeviceVM = new NewDeviceViewModel(_producerAccess, _deviceAccess);
            newDeviceVM.ProducerName = validProducerName;
            newDeviceVM.URL = validProducerURL;

            // Act
            newDeviceVM.SaveNewProducer();
            
            // Assert
            Assert.Equal(validProducerName, newDeviceVM.ProducerName);
            Assert.Equal(validProducerURL, newDeviceVM.URL);
            Assert.IsType<SnackbarMessageQueue>(newDeviceVM.SnackbarNotification.MessageQueue);
            Assert.True(newDeviceVM.SaveNewProducerResult);
            Assert.True(newDeviceVM.NewProducerValidationResult);
            
        }

        [Theory]
        [InlineData("", "someURL")]
        public void SaveNewProducerInvalidData_Test(string name, string url)
        {
            // Arrange
            var newDeviceVM = new NewDeviceViewModel(_producerAccess, _deviceAccess);
            newDeviceVM.ProducerName = name;
            newDeviceVM.URL = url;

            // Act
            newDeviceVM.SaveNewProducer();

            // Assert
            Assert.IsType<SnackbarMessageQueue>(newDeviceVM.SnackbarNotification.MessageQueue);
            Assert.False(newDeviceVM.SaveNewProducerResult);
            Assert.False(newDeviceVM.NewProducerValidationResult);
            
        }

        [Fact]
        public void ClearProducerForm_Test()
        {
            // Arrange
            string validProducerName = "FESTO";
            string validProducerURL = "https://festo.com/";

            var newDeviceVM = new NewDeviceViewModel(_producerAccess, _deviceAccess);
            newDeviceVM.ProducerName = validProducerName;
            newDeviceVM.URL = validProducerURL;

            // Act
            newDeviceVM.ClearProducerForm();

            // Assert
            Assert.Equal("", newDeviceVM.ProducerName);
            Assert.Equal("", newDeviceVM.URL);
        }

        [Fact]
        public void SaveNewDevice_Test()
        {
            // Arrange
            string validDeviceName = "Panel KTP900 Basic";
            string validArticleNumber = "DSF98-DS9F8-DS98F";
            string validSelectedProducerName = "SIEMENS";
            string validLocation = "A2";
            int validQuantity = 13;

            var newDeviceVM = new NewDeviceViewModel(_producerAccess, _deviceAccess);
            newDeviceVM.DeviceName = validDeviceName;
            newDeviceVM.ArticleNumber = validArticleNumber;
            newDeviceVM.SelectedProducerName = validSelectedProducerName;
            newDeviceVM.Location = validLocation;
            newDeviceVM.Quantity = validQuantity;

            // Act
            newDeviceVM.SaveButton();
            
            // Assert
            Assert.True(newDeviceVM.NewDeviceValidationResult);
            Assert.True(newDeviceVM.SaveNewDeviceResult);
            Assert.IsType<SnackbarMessageQueue>(newDeviceVM.SnackbarNotification.MessageQueue);
        }

        [Theory]
        [InlineData("", "SD98F98-DS98", "FESTO", "A2", 14)]
        [InlineData("Panel KTP900", "", "FESTO", "A2", 14)]
        [InlineData("", "", "FESTO", "A2", 14)]
        public void SaveNewDeviceInvalidData_Test(string deviceName, string articleNumber, 
            string producerName, string location, int quantity)
        {
            // Arrange
            var newDeviceVM = new NewDeviceViewModel(_producerAccess, _deviceAccess);
            newDeviceVM.DeviceName = deviceName;
            newDeviceVM.ArticleNumber = articleNumber;
            newDeviceVM.SelectedProducerName = producerName;
            newDeviceVM.Location = location;
            newDeviceVM.Quantity = quantity;

            // Act
            newDeviceVM.SaveButton();
            
            // Assert
            Assert.IsType<SnackbarMessageQueue>(newDeviceVM.SnackbarNotification.MessageQueue);
            Assert.False(newDeviceVM.NewDeviceValidationResult);
            Assert.False(newDeviceVM.SaveNewDeviceResult);
        }

        [Fact]
        public void ClearDeviceForm_Test()
        {
            // Arrange
            string validDeviceName = "Panel KTP900 Basic";
            string validArticleNumber = "DSF98-DS9F8-DS98F";
            string validSelectedProducerName = "SIEMENS";
            string validLocation = "A2";
            int validQuantity = 13;

            var newDeviceVM = new NewDeviceViewModel(_producerAccess, _deviceAccess);
            newDeviceVM.DeviceName = validDeviceName;
            newDeviceVM.ArticleNumber = validArticleNumber;
            newDeviceVM.SelectedProducerName = validSelectedProducerName;
            newDeviceVM.Location = validLocation;
            newDeviceVM.Quantity = validQuantity;

            // Act
            newDeviceVM.ClearDeviceForm();

            // Assert
            Assert.Equal("", newDeviceVM.DeviceName);
            Assert.Equal("", newDeviceVM.ArticleNumber);
            Assert.Equal("", newDeviceVM.Location);
            Assert.Equal(0, newDeviceVM.Quantity);
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

            var newDeviceVM = new NewDeviceViewModel(_producerAccess, _deviceAccess);
            newDeviceVM.DeviceName = validDeviceName;
            newDeviceVM.ArticleNumber = validArticleNumber;
            newDeviceVM.SelectedProducerName = validSelectedProducerName;
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
