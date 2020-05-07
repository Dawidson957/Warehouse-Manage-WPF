using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Caliburn.Micro;
using DataAccess.DataAcc;
using DataAccess.Entities;
using Warehouse_Manage_WPF.UserInterface.ViewModels;
using Warehouse_Manage_WPF.UserInterface.Models;

namespace Warehouse_Manage_WPF.Tests.UserInterfaceTests.ViewModelsTests
{
    public class WarehouseViewModel_Tests
    {
        private SimpleContainer simpleContainer { get; set; }

        private IWindowManager windowManager { get; set; }

        private IDeviceAccess deviceAccess { get; set; }

        private IProducerAccess producerAccess { get; set; }


        public WarehouseViewModel_Tests()
        {
            simpleContainer = new SimpleContainer();
            windowManager = new WindowManager();

            var producerAccessMock = new Mock<IProducerAccess>();
            producerAccessMock
                .Setup(x => x.GetProducerId(It.IsAny<string>()))
                .ReturnsAsync(2);

            producerAccess = producerAccessMock.Object;

            var devicesList = GenerateDeviceList();
            var deviceAccessMock = new Mock<IDeviceAccess>();

            deviceAccessMock
                .Setup(x => x.GetDevicesAll(It.IsAny<int>()))
                .ReturnsAsync(devicesList);

            deviceAccess = deviceAccessMock.Object;
        }

        [Fact]
        public void LoadDevices_Test()
        {
            // Arrange
            List<Device> expectedDeviceList = GenerateDeviceList();

            // Act
            var warehouseVM = new WarehouseViewModel(simpleContainer, windowManager, deviceAccess, producerAccess);

            // Assert
            if(warehouseVM.IsInitialized)
            {
                Assert.Equal(expectedDeviceList.Count, warehouseVM.Devices.Count);
                Assert.IsType<BindableCollection<DeviceModel>>(warehouseVM.Devices);

                // Check first device credentials
                Assert.Equal(expectedDeviceList[0].Id, warehouseVM.Devices[0].Id);
                Assert.Equal(expectedDeviceList[0].Name, warehouseVM.Devices[0].Name);
                Assert.Equal(expectedDeviceList[0].ArticleNumber, warehouseVM.Devices[0].ArticleNumber);
                Assert.Equal(expectedDeviceList[0].Producer.Name, warehouseVM.Devices[0].ProducerName);
                Assert.Equal(expectedDeviceList[0].ProjectID, warehouseVM.Devices[0].ProjectId);
                Assert.Equal(expectedDeviceList[0].Location, warehouseVM.Devices[0].Location);
                Assert.Equal(expectedDeviceList[0].Quantity, warehouseVM.Devices[0].Quantity);
            }
        }


        private List<Device> GenerateDeviceList()
        {
            List<Device> devices = new List<Device>()
            {
                new Device
                {
                    Id = 1,
                    Name = "someDeviceName",
                    ArticleNumber = "DS98F-SD98F-DS8",
                    Location = "A2",
                    Quantity = 13,
                    ProjectID = 3,
                    Producer = new Producer
                    {
                        Name = "someProducerName",
                        URL = "someURL"
                    }
                },
                new Device
                {
                    Id = 13,
                    Name = "someDeviceName2",
                    ArticleNumber = "VC09B-AS98-D9AD",
                    Location = "B2",
                    Quantity = 8,
                    ProjectID = 9,
                    Producer = new Producer
                    {
                        Name = "someProducerName2",
                        URL = "someURL2"
                    }
                },
                new Device
                {
                    Id = 84,
                    Name = "someDeviceName32",
                    ArticleNumber = "FDG-FD09G-394832",
                    Location = "D1",
                    Quantity = 33,
                    ProjectID = 14,
                    Producer = new Producer
                    {
                        Name = "someProducerName213",
                        URL = "someURL22"
                    }
                }
            };

            return devices;
        }
    }
}
