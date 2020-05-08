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
    public class NewProjectViewModel_Tests
    {
        private ICustomerAccess _customerAccess { get; set; }

        private IProjectAccess _projectAccess { get; set; }

        private IEventAggregator _events { get; set; }

        public NewProjectViewModel_Tests()
        {
            var eventAggregatorMock = new Mock<IEventAggregator>();
            _events = eventAggregatorMock.Object;

            var customersList = GenerateCustomersName();

            var customerAccessMock = new Mock<ICustomerAccess>();
            customerAccessMock
                .Setup(x => x.GetCustomersName())
                .ReturnsAsync(customersList);
            customerAccessMock
                .Setup(x => x.GetCustomerId(It.IsAny<string>()))
                .ReturnsAsync(2);

            var projectAccessMock = new Mock<IProjectAccess>();
            projectAccessMock
                .Setup(x => x.AddProject(It.IsAny<Project>()))
                .ReturnsAsync(true);

            _customerAccess = customerAccessMock.Object;
            _projectAccess = projectAccessMock.Object;
        }

        [Fact]
        public void CreateNewProjectViewModel_Test()
        {
            // Arrange
            var customerList = GenerateCustomersName();

            // Act
            var newProjectVM = new NewProjectViewModel(_events, _projectAccess, _customerAccess);
            newProjectVM.LoadCustomers_Run();

            // Assert
            Assert.IsType<BindableCollection<string>>(newProjectVM.CustomersName);
            Assert.True(newProjectVM.CustomersName.Count == customerList.Count);
        }

        [Fact]
        public void CreateProject_Test()
        {
            // Arrange
            string projectName = "sampleProjectName";
            string customerName = "sampleCustomerName";
            string comment = "sampleComment";

            var newProjectVM = new NewProjectViewModel(_events, _projectAccess, _customerAccess);
            newProjectVM.ProjectName = projectName;
            newProjectVM.SelectedCustomer = customerName;
            newProjectVM.Comment = comment;

            // Act
            newProjectVM.CreateProject();

            // Assert
            Assert.True(newProjectVM.ProjectValidationResult);
            Assert.True(newProjectVM.ProjectAddResult);
        }


        [Theory]
        [InlineData("", "sampleCustomerName", "sampleComment")]
        [InlineData("", "", "")]
        public void CreateProject_InvalidData_Test(string name, string customerName, string comment)
        {
            // Arrange
            var newProjectVM = new NewProjectViewModel(_events, _projectAccess, _customerAccess);
            newProjectVM.ProjectName = name;
            newProjectVM.SelectedCustomer = customerName;
            newProjectVM.Comment = comment;

            // Act
            newProjectVM.CreateProject();

            // Assert
            Assert.False(newProjectVM.ProjectValidationResult);
            Assert.False(newProjectVM.ProjectAddResult);
        }

        private List<string> GenerateCustomersName()
        {
            List<string> customers = new List<string>()
            {
                "Klient1",
                "Klient2",
                "Klient3"
            };

            return customers;
        }

    }
}
