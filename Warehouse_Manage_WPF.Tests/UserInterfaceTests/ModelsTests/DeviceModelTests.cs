using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Warehouse_Manage_WPF.UserInterface.Models;
using Xunit;
using DataAccess.DataAcc;
using DataAccess.Entities;

namespace Warehouse_Manage_WPF.Tests.UserInterfaceTests.ModelsTests
{
    public class DeviceModelTests
    {
        [Fact]
        public async void ConvertToDeviceEntity_WithoutClearIDs_Test()
        {
            // Arrange
            int sampleProducerId = 2;

            var producerAccess = new Mock<IProducerAccess>();
            producerAccess
                .Setup(x => x.GetProducerId(It.IsAny<string>()))
                .ReturnsAsync(sampleProducerId);

            int sampleId = 1;
            string sampleArticleNumber = "SD9F0-SDF98-SDF98";
            string sampleName = "Panel KTP900 Basic";
            string sampleProducerName = "SIEMENS";
            string sampleLocation = "A2";
            int sampleQuantity = 14;
            int sampleProjectId = 4;


            var deviceModel = new DeviceModel(producerAccess.Object)
            {
                Id = sampleId,
                Name = sampleName,
                ArticleNumber = sampleArticleNumber,
                ProducerName = sampleProducerName,
                Location = sampleLocation,
                Quantity = sampleQuantity,
                ProjectId = sampleProjectId
            };


            // Act
            var deviceEntity = await deviceModel.ConvertToDeviceEntity();


            // Assert

            // Check properties values
            Assert.Equal(sampleId, deviceEntity.Id);
            Assert.Equal(sampleName, deviceEntity.Name);
            Assert.Equal(sampleArticleNumber, deviceEntity.ArticleNumber);
            Assert.Equal(sampleLocation, deviceEntity.Location);
            Assert.Equal(sampleQuantity, deviceEntity.Quantity);
            Assert.Equal(sampleProjectId, deviceEntity.ProjectID);
            Assert.Equal(sampleProducerId, deviceEntity.ProducerID);

            // Check type of converted object
            Assert.True(deviceEntity.GetType() == typeof(DataAccess.Entities.Device));

            // Check default values of converted entity
            Assert.Null(deviceEntity.Producer);
            Assert.Null(deviceEntity.Project);
        }
        
        [Fact]
        public async void ConvertToDeviceEntity_WithClearIDs_Test()
        {
            // Arrange
            int sampleProducerId = 2;

            var producerAccess = new Mock<IProducerAccess>();
            producerAccess
                .Setup(x => x.GetProducerId(It.IsAny<string>()))
                .ReturnsAsync(sampleProducerId);

            int sampleId = 1;
            string sampleArticleNumber = "SD9F0-SDF98-SDF98";
            string sampleName = "Panel KTP900 Basic";
            string sampleProducerName = "SIEMENS";
            string sampleLocation = "A2";
            int sampleQuantity = 14;
            int sampleProjectId = 4;

            var deviceModel = new DeviceModel(producerAccess.Object)
            {
                Id = sampleId,
                Name = sampleName,
                ArticleNumber = sampleArticleNumber,
                ProducerName = sampleProducerName,
                Location = sampleLocation,
                Quantity = sampleQuantity,
                ProjectId = sampleProjectId
            };


            // Act
            var deviceEntity = await deviceModel.ConvertToDeviceEntity(true);


            // Assert

            // Check properties values
            Assert.Equal(0, deviceEntity.Id);
            Assert.Equal(sampleName, deviceEntity.Name);
            Assert.Equal(sampleArticleNumber, deviceEntity.ArticleNumber);
            Assert.Equal(sampleLocation, deviceEntity.Location);
            Assert.Equal(sampleQuantity, deviceEntity.Quantity);
            Assert.Equal(0, deviceEntity.ProjectID);
            Assert.Equal(sampleProducerId, deviceEntity.ProducerID);

            // Check type of converted object
            Assert.True(deviceEntity.GetType() == typeof(DataAccess.Entities.Device));

            // Check default values of converted entity
            Assert.Null(deviceEntity.Producer);
            Assert.Null(deviceEntity.Project);
        }

        [Fact]
        public void DeviceModel_CopyConstructor()
        {
            // Arrange
            int sampleId = 1;
            string sampleArticleNumber = "SD9F0-SDF98-SDF98";
            string sampleName = "Panel KTP900 Basic";
            string sampleProducerName = "SIEMENS";
            string sampleLocation = "A2";
            int sampleQuantity = 14;
            int sampleProjectId = 4;
            IProducerAccess producerAccess = new ProducerAccess();

            var deviceModel = new DeviceModel(producerAccess)
            {
                Id = sampleId,
                Name = sampleName,
                ArticleNumber = sampleArticleNumber,
                ProducerName = sampleProducerName,
                Location = sampleLocation,
                Quantity = sampleQuantity,
                ProjectId = sampleProjectId
            };


            // Act
            var copiedDeviceModel = new DeviceModel(deviceModel);


            // Assert
            Assert.Equal(sampleId, copiedDeviceModel.Id);
            Assert.Equal(sampleName, copiedDeviceModel.Name);
            Assert.Equal(sampleArticleNumber, copiedDeviceModel.ArticleNumber);
            Assert.Equal(sampleProducerName, copiedDeviceModel.ProducerName);
            Assert.Equal(sampleLocation, copiedDeviceModel.Location);
            Assert.Equal(sampleQuantity, copiedDeviceModel.Quantity);
            Assert.Equal(sampleProjectId, copiedDeviceModel.ProjectId);
        }

        [Fact]
        public void CreateDeviceModelFromDeviceEntity()
        {
            // Arrange
            string sampleProducerName = "SIEMENS";
            string sampleProducerURL = "https://siemens.com/";

            Producer producerEntity = new Producer()
            {
                Name = sampleProducerName,
                URL = sampleProducerURL
            };

            int sampleId = 1;
            string sampleArticleNumber = "SD9F0-SDF98-SDF98";
            string sampleName = "Panel KTP900 Basic";
            string sampleLocation = "A2";
            int sampleQuantity = 14;
            int sampleProjectId = 4;

            Device deviceEntity = new Device()
            {
                Id = sampleId,
                Name = sampleName,
                ArticleNumber = sampleArticleNumber,
                Location = sampleLocation,
                Quantity = sampleQuantity,
                ProjectID = sampleProjectId,
                Producer = producerEntity
            };

            IProducerAccess producerAccess = new ProducerAccess();

            // Act
            var deviceModel = new DeviceModel(deviceEntity, producerAccess);

            // Assert
            Assert.Equal(sampleId, deviceModel.Id);
            Assert.Equal(sampleName, deviceModel.Name);
            Assert.Equal(sampleArticleNumber, deviceModel.ArticleNumber);
            Assert.Equal(sampleLocation, deviceModel.Location);
            Assert.Equal(sampleQuantity, deviceModel.Quantity);
            Assert.Equal(sampleProjectId, deviceModel.ProjectId);
            Assert.Equal(sampleProducerName, deviceModel.ProducerName);
        }

        [Fact]
        public async void GetProducerId_Test()
        {
            int sampleProducerId = 2;

            var producerAccess = new Mock<IProducerAccess>();
            producerAccess
                .Setup(x => x.GetProducerId(It.IsAny<string>()))
                .ReturnsAsync(sampleProducerId);

            DeviceModel deviceModel = new DeviceModel(producerAccess.Object)
            {
                ProducerName = "someProducer"
            };

            // Act
            var producerId = await deviceModel.GetProducerId(deviceModel.ProducerName);

            // Act
            Assert.Equal(sampleProducerId, producerId);
            Assert.True(producerId.GetType() == typeof(int));
        }
    }
}
