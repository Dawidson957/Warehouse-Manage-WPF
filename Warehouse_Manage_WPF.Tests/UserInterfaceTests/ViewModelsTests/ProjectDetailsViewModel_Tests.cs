using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using DataAccess.DataAcc;
using DataAccess.Entities;
using Moq;
using Warehouse_Manage_WPF.UserInterface;
using Warehouse_Manage_WPF.UserInterface.Models;
using Warehouse_Manage_WPF.UserInterface.ViewModels;
using Xunit;

namespace Warehouse_Manage_WPF.Tests.UserInterfaceTests.ViewModelsTests
{
    public class ProjectDetailsViewModel_Tests
    {
        private IEventAggregator _events { get; set; }

        private ICustomerAccess _customerAccess { get; set; }

        private IProjectAccess _projectAccess { get; set; }


        public ProjectDetailsViewModel_Tests()
        {
            var customersList = GenerateCustomersName();

            var customerAccessMock = new Mock<ICustomerAccess>();
            customerAccessMock
                .Setup(x => x.GetCustomersName())
                .ReturnsAsync(customersList);

            _customerAccess = customerAccessMock.Object;


            var projectAccessMock = new Mock<IProjectAccess>();
            projectAccessMock
                .Setup(x => x.UpdateProject(It.IsAny<Project>()))
                .ReturnsAsync(true);

            _projectAccess = projectAccessMock.Object;

            _events = new EventAggregator();
        }


        [Fact]
        public void LoadProject_Test()
        {
            // Arrange
            ProjectModel projectModel = new ProjectModel(_customerAccess)
            {
                Id = 4,
                Name = "sampleProjectName",
                CustomerName = "sampleCustomerName",
                Status = "Open",
                Comment = "sampleComment"
            };

            var projectDetailsVM = new ProjectDetailsViewModel(_events, _customerAccess, _projectAccess);

            // Act
            projectDetailsVM.LoadProject(projectModel);

            // Assert
            Assert.IsType<ProjectModel>(projectDetailsVM.project);
            Assert.Equal(projectModel.Id, projectDetailsVM.project.Id);
            Assert.Equal(projectModel.Name, projectDetailsVM.project.Name);
            Assert.Equal(projectModel.CustomerName, projectDetailsVM.project.CustomerName);
            Assert.Equal(projectModel.Status, projectDetailsVM.project.Status);
            Assert.Equal(projectModel.Comment, projectDetailsVM.project.Comment);
        }

        [Fact]
        public void LoadProjectStatus_Test()
        {
            // Arrange
            var projectDetailsVM = new ProjectDetailsViewModel(_events, _customerAccess, _projectAccess);

            // Act
            projectDetailsVM.LoadProjectStatus_Run();

            // Assert
            Assert.IsType<BindableCollection<string>>(projectDetailsVM.Customers);
            Assert.NotEmpty(projectDetailsVM.Customers);
        }

        [Fact]
        public void LoadCustomers_Test()
        {
            // Arrange
            var projectDetailsVM = new ProjectDetailsViewModel(_events, _customerAccess, _projectAccess);

            // Act
            projectDetailsVM.LoadCustomers_Run();

            // Assert
            Assert.IsType<BindableCollection<string>>(projectDetailsVM.Customers);
            Assert.NotEmpty(projectDetailsVM.Customers);
        }

        [Fact]
        public void LoadCustomers_NoCustomersFound_Test()
        {
            // Arrange
            List<string> customersList = null;

            var customerAccessMock = new Mock<ICustomerAccess>();
            customerAccessMock
                .Setup(x => x.GetCustomersName())
                .ReturnsAsync(customersList);

            var customerAccess = customerAccessMock.Object;
            var projectDetailsVM = new ProjectDetailsViewModel(_events, customerAccess, _projectAccess);

            // Act
            projectDetailsVM.LoadCustomers_Run();

            // Assert
            Assert.IsType<BindableCollection<string>>(projectDetailsVM.Customers);
            Assert.Empty(projectDetailsVM.Customers);
            Assert.NotNull(projectDetailsVM.Customers);
        }

        [Fact]
        public void SetFormValues_Test()
        {
            // Arrange
            ProjectModel projectModel = new ProjectModel(_customerAccess)
            {
                Id = 4,
                Name = "sampleProjectName",
                CustomerName = "Klient1",
                Status = "Otwarty",
                Comment = "sampleComment"
            };

            var projectDetailsVM = new ProjectDetailsViewModel(_events, _customerAccess, _projectAccess);
            projectDetailsVM.LoadProject(projectModel);

            // Act
            projectDetailsVM.SetFormValues_Run();

            // Assert
            Assert.Equal(projectModel.Name, projectDetailsVM.ProjectName);
            Assert.Equal(projectModel.CustomerName, projectDetailsVM.SelectedProjectCustomer);
            Assert.Equal(projectModel.Status, projectDetailsVM.SelectedProjectStatus);
            Assert.Equal(projectModel.Comment, projectDetailsVM.Comment);
        }

        [Fact]
        public void SaveButton_Test()
        {
            // Arrange
            ProjectModel projectModel = new ProjectModel(_customerAccess)
            {
                Id = 4,
                Name = "sampleProjectName",
                CustomerName = "Klient1",
                Status = "Otwarty",
                Comment = "sampleComment"
            };

            var projectDetailsVM = new ProjectDetailsViewModel(_events, _customerAccess, _projectAccess);
            projectDetailsVM.LoadProject(projectModel);
            projectDetailsVM.LoadCustomers_Run();
            projectDetailsVM.SetFormValues_Run();

            // Act
            projectDetailsVM.SaveButton();

            // Assert
            Assert.True(projectDetailsVM.ProjectValidationResult);
            Assert.True(projectDetailsVM.UpdateProjectResult);
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
