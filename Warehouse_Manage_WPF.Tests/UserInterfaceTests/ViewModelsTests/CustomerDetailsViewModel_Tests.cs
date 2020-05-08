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
    public class CustomerDetailsViewModel_Tests
    {
        private IEventAggregator _events { get; set; }

        private ICustomerAccess _customerAccess { get; set; }

        public CustomerDetailsViewModel_Tests()
        {
            var customerAccessMock = new Mock<ICustomerAccess>();
            customerAccessMock
                .Setup(x => x.UpdateCustomer(It.IsAny<Customer>()))
                .ReturnsAsync(true);

            _customerAccess = customerAccessMock.Object;

            var eventAggregatorMock = new Mock<IEventAggregator>();
            _events = eventAggregatorMock.Object;
        }

        [Fact]
        public void LoadCustomer_Test()
        {
            // Arrange
            var customer = new CustomerModel()
            {
                Id = 3,
                Name = "sampleCustomerName",
                Address = "sampleAddress",
                City = "sampleCity"
            };

            var customerDetailsVM = new CustomerDetailsViewModel(_events, _customerAccess);

            // Act
            customerDetailsVM.LoadCustomer(customer);

            // Assert
            Assert.Equal(customer.Id, customerDetailsVM.customerId);
            Assert.Equal(customer.Name, customerDetailsVM.CustomerName);
            Assert.Equal(customer.Address, customerDetailsVM.CustomerAddress);
            Assert.Equal(customer.City, customerDetailsVM.CustomerCity);
        }

        [Fact]
        public void SaveButton_Test()
        {
            // Arrange
            var customer = new CustomerModel()
            {
                Id = 3,
                Name = "sampleCustomerName",
                Address = "sampleAddress",
                City = "sampleCity"
            };

            var customerDetailsVM = new CustomerDetailsViewModel(_events, _customerAccess);
            customerDetailsVM.LoadCustomer(customer);

            // Act
            customerDetailsVM.SaveButton();

            // Assert
            Assert.True(customerDetailsVM.CustomerValidationResult);
            Assert.True(customerDetailsVM.CustomerUpdateResult);
        }

        [Theory]
        [InlineData(3, "", "", "")]
        [InlineData(4, "", "sampleAddress", "sampleCity")]
        public void SaveButton_InvalidData_Test(int Id, string name, string address, string city)
        {
            // Arrange
            var customer = new CustomerModel()
            {
                Id = Id,
                Name = name,
                Address = address,
                City = city
            };

            var customerDetailsVM = new CustomerDetailsViewModel(_events, _customerAccess);
            customerDetailsVM.LoadCustomer(customer);

            // Act
            customerDetailsVM.SaveButton();

            // Assert
            Assert.False(customerDetailsVM.CustomerValidationResult);
            Assert.False(customerDetailsVM.CustomerUpdateResult);
        }
    }
}
