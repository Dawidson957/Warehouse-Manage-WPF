using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DataAcc;
using DataAccess.Entities;
using Moq;
using Warehouse_Manage_WPF.UserInterface.Models;
using Xunit;

namespace Warehouse_Manage_WPF.Tests.UserInterfaceTests.ModelsTests
{
    public class ProjectModelTests
    {
        [Fact]
        public async void ConvertToProjectEntity_Test()
        {
            // Arrange
            int sampleCustomerId = 13;

            var customerAccessMock = new Mock<ICustomerAccess>();
            customerAccessMock
                .Setup(x => x.GetCustomerId(It.IsAny<string>()))
                .ReturnsAsync(sampleCustomerId);

            int sampleId = 4;
            string sampleName = "sampleName";
            string sampleStatus = "Open";
            string sampleComment = "someComment";
            string sampleCustomerName = "someCustomerName";

            ProjectModel projectModel = new ProjectModel(customerAccessMock.Object)
            {
                Id = sampleId,
                Name = sampleName,
                Status = sampleStatus,
                Comment = sampleComment,
                CustomerName = sampleCustomerName
            };

            // Act
            var projectEntity = await projectModel.ConvertToProjectEntity();

            // Assert
            Assert.Equal(sampleId, projectEntity.Id);
            Assert.Equal(sampleName, projectEntity.Name);
            Assert.Equal(sampleStatus, projectEntity.Status);
            Assert.Equal(sampleComment, projectEntity.Comment);
            Assert.Equal(sampleCustomerId, projectEntity.CustomerID);
            Assert.True(projectEntity.GetType() == typeof(DataAccess.Entities.Project));
            Assert.Null(projectEntity.Customer);
        }

        [Fact]
        public void CreateProjectModelFromProjectEntity_Test()
        {
            // Arrange
            int sampleCustomerId = 3;
            string sampleCustomerName = "someCustomerName";
            string sampleCustomerAddress = "someAddress";
            string sampleCustomerCity = "someCity";

            Customer customer = new Customer
            {
                Id = sampleCustomerId,
                Name = sampleCustomerName,
                Address = sampleCustomerAddress,
                City = sampleCustomerCity
            };

            int sampleId = 4;
            string sampleName = "sampleName";
            string sampleStatus = "Open";
            string sampleComment = "someComment";

            Project project = new Project
            {
                Id = sampleId,
                Name = sampleName,
                Status = sampleStatus,
                Comment = sampleComment,
                Customer = customer
            };

            var customerAccess = new CustomerAccess();

            // Act
            var projectModel = new ProjectModel(project, customerAccess);

            // Assert
            Assert.Equal(sampleId, projectModel.Id);
            Assert.Equal(sampleName, projectModel.Name);
            Assert.Equal(sampleStatus, projectModel.Status);
            Assert.Equal(sampleComment, projectModel.Comment);
            Assert.Equal(sampleCustomerName, projectModel.CustomerName);
            Assert.True(projectModel.GetType() == typeof(Warehouse_Manage_WPF.UserInterface.Models.ProjectModel));
        }

        [Fact]
        public async void GetCustomerId_Test()
        {
            // Arrange
            int sampleCustomerId = 13;
            string sampleCustomerName = "someCustomer";

            var customerAccessMock = new Mock<ICustomerAccess>();
            customerAccessMock
                .Setup(x => x.GetCustomerId(It.IsAny<string>()))
                .ReturnsAsync(sampleCustomerId);

            ProjectModel projectModel = new ProjectModel(customerAccessMock.Object)
            {
                CustomerName = sampleCustomerName
            };

            // Act
            var customerId = await projectModel.GetCustomerId(projectModel.CustomerName);

            // Assert
            Assert.Equal(sampleCustomerId, customerId);
            Assert.True(customerId.GetType() == typeof(int));
        }
    }
}
