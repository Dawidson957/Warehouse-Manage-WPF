using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Manage_WPF.UserInterface.Models;
using Xunit;

namespace Warehouse_Manage_WPF.Tests.UserInterfaceTests.ModelsTests
{
    public class CustomerModelTests
    {
        [Fact]
        public void CreateCustomerModelFromCustomerEntity_Test()
        {
            // Arrange
            int sampleId = 3;
            string sampleName = "sampleName";
            string sampleAddress = "someStreet 32A/3";
            string sampleCity = "someCityName";

            Customer customerEntity = new Customer
            {
                Id = sampleId,
                Name = sampleName,
                Address = sampleAddress,
                City = sampleCity
            };


            // Act
            var customerModel = new CustomerModel(customerEntity);


            // Assert
            Assert.Equal(sampleId, customerModel.Id);
            Assert.Equal(sampleName, customerModel.Name);
            Assert.Equal(sampleAddress, customerModel.Address);
            Assert.Equal(sampleCity, customerModel.City);

            Assert.True(customerModel.GetType() == typeof(Warehouse_Manage_WPF.UserInterface.Models.CustomerModel));
        }

        [Fact]
        public void ConvertToCustomerEntity_Test()
        {
            // Arrange
            int sampleId = 3;
            string sampleName = "sampleName";
            string sampleAddress = "someStreet 32A/3";
            string sampleCity = "someCityName";

            CustomerModel customerModel = new CustomerModel
            {
                Id = sampleId,
                Name = sampleName,
                Address = sampleAddress,
                City = sampleCity
            };

            // Act
            var customerEntity = customerModel.ConvertToCustomerEntity();

            // Assert
            Assert.Equal(0, customerEntity.Id);
            Assert.Equal(sampleName, customerEntity.Name);
            Assert.Equal(sampleAddress, customerEntity.Address);
            Assert.Equal(sampleCity, customerEntity.City);
            Assert.True(customerEntity.GetType() == typeof(DataAccess.Entities.Customer));
            Assert.Null(customerEntity.Projects);
        }
    }
}
