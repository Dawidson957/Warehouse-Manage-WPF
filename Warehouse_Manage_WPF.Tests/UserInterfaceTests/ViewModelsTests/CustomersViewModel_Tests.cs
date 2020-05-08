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
    public class CustomersViewModel_Tests
    {
        private SimpleContainer _container { get; set; }

        private IWindowManager _windowManager { get; set; }

        private ICustomerAccess _customerAccess { get; set; }

        public CustomersViewModel_Tests()
        {
            var simpleContainerMock = new Mock<SimpleContainer>();
            _container = simpleContainerMock.Object;

            var windowManagerMock = new Mock<IWindowManager>();
            _windowManager = windowManagerMock.Object;

            var customerList = GenerateCustomers();

            var customerAccessMock = new Mock<ICustomerAccess>();
            customerAccessMock
                .Setup(x => x.GetCustomers())
                .ReturnsAsync(customerList);
            customerAccessMock
                .Setup(x => x.AddCustomer(It.IsAny<Customer>()))
                .ReturnsAsync(true);

            _customerAccess = customerAccessMock.Object;
        }

        [Fact]
        public void CreateCustomersViewModel_Test()
        {
            // Arrange
            var customersList = GenerateCustomers();

            // Act
            var customersVM = new CustomersViewModel(_container, _windowManager, _customerAccess);
            customersVM.LoadCustomers_Run(); // Manually run LoadCustomers() 

            // Assert
            Assert.IsType<CustomerModel>(customersVM.NewCustomer);
            Assert.IsType<BindableCollection<CustomerModel>>(customersVM.Customers);
            Assert.True(customersVM.Customers.Count == customersList.Count);
        }

        [Fact]
        public void SaveButton_Test()
        {
            // Arrange
            var customersVM = new CustomersViewModel(_container, _windowManager, _customerAccess);
            customersVM.NewCustomer.Name = "sampleName";
            customersVM.NewCustomer.Address = "sampleAddress";
            customersVM.NewCustomer.City = "sampleCity";

            // Act
            customersVM.SaveButton();

            // Assert
            Assert.True(customersVM.CustomerValidationResult);
            Assert.True(customersVM.CustomerAddResult);
        }

        [Theory]
        [InlineData("", "", "")]
        [InlineData("", "someAddress", "someCity")]
        public void SaveButton_InvalidData_Test(string name, string address, string city)
        {
            // Arrange
            var customersVM = new CustomersViewModel(_container, _windowManager, _customerAccess);
            customersVM.NewCustomer.Name = name;
            customersVM.NewCustomer.Address = address;
            customersVM.NewCustomer.City = city;

            // Act
            customersVM.SaveButton();

            // Assert
            Assert.False(customersVM.CustomerValidationResult);
            Assert.False(customersVM.CustomerAddResult);
        }
        
        [Fact]
        public void ClearButton_Test()
        {
            // Arrange
            var customersVM = new CustomersViewModel(_container, _windowManager, _customerAccess);
            customersVM.NewCustomer.Name = "sampleName";
            customersVM.NewCustomer.Address = "sampleAddress";
            customersVM.NewCustomer.City = "sampleCity";

            // Act
            customersVM.ClearButton();

            // Assert
            Assert.Equal("", customersVM.NewCustomer.Name);
            Assert.Equal("", customersVM.NewCustomer.Address);
            Assert.Equal("", customersVM.NewCustomer.City);
        }

        private List<Customer> GenerateCustomers()
        {
            var customers = new List<Customer>
            {
                new Customer
                {
                    Id = 1,
                    Name = "sampleName1",
                    Address = "sampleAddress1",
                    City = "sampleCity1"
                },
                new Customer
                {
                    Id = 2,
                    Name = "sampleName2",
                    Address = "sampleAddress2",
                    City = "sampleCity2"
                },
                new Customer
                {
                    Id = 3,
                    Name = "sampleName3",
                    Address = "sampleAddress3",
                    City = "sampleCity3"
                }
            };

            return customers;
        }
    }
}
