using Caliburn.Micro;
using DataAccess.DataAcc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Warehouse_Manage_WPF.UserInterface.EventModels;
using Warehouse_Manage_WPF.UserInterface.Models;
using Warehouse_Manage_WPF.Validators;

namespace Warehouse_Manage_WPF.UserInterface.ViewModels
{
    public class ProjectDetailsViewModel : Screen
    {
        private SimpleContainer _container { get; set; }

        private IEventAggregator _events { get; set; }

        private CustomerAccess _customerAccess { get; set; }

        private ProjectAccess _projectAccess { get; set; }

        private ProjectModel _project { get; set; }


        public ProjectDetailsViewModel(SimpleContainer simpleContainer, IEventAggregator eventAggregator)
        {
            _container = simpleContainer;
            _events = eventAggregator;
            _customerAccess = _container.GetInstance<CustomerAccess>();
            _projectAccess = _container.GetInstance<ProjectAccess>();
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadCustomers();
            LoadProjectStatus();
            SetFormValues();
        }

        public void LoadProject(ProjectModel projectModel)
        {
            _project = projectModel;
        }

        private async Task LoadCustomers()
        {
            var customersName = await _customerAccess.GetCustomersName();
            Customers = new BindableCollection<string>();

            foreach (var name in customersName)
                Customers.Add(name);
        }

        private void LoadProjectStatus()
        {
            ProjectStatus = new BindableCollection<string>();
            ProjectStatus.Add("Otwarty");
            ProjectStatus.Add("W trakcie realizacji");
            ProjectStatus.Add("Zawieszony");
            ProjectStatus.Add("Zamknięty");
        }

        private void SetFormValues()
        {
            if(_project.Status != null)
            {
                if(ProjectStatus.Contains(_project.Status))
                {
                    SelectedProjectStatus = _project.Status;
                }
            }

            if(_project.CustomerName != null)
            {
                if(Customers.Contains(_project.CustomerName))
                {
                    SelectedProjectCustomer = _project.CustomerName;
                }
            }

            ProjectName = _project.Name;
            Comment = _project.Comment;
        }

        #region Project Form

        private string _projectName;
        private BindableCollection<string> _projectStatus;
        private string _selectedProjectStatus;
        private BindableCollection<string> _customers;
        private string _selectedCustomer;
        private string _comment;


        public string ProjectName
        {
            get { return _projectName; }
            set 
            { 
                _projectName = value;
                NotifyOfPropertyChange(() => ProjectName);
            }
        }

        public BindableCollection<string> ProjectStatus
        {
            get { return _projectStatus; }
            set 
            { 
                _projectStatus = value;
                NotifyOfPropertyChange(() => ProjectStatus);
            }
        }

        public string SelectedProjectStatus
        {
            get { return _selectedProjectStatus; }
            set
            {
                _selectedProjectStatus = value;
                NotifyOfPropertyChange(() => SelectedProjectStatus);
            }
        }

        public BindableCollection<string> Customers
        {
            get { return _customers; }
            set 
            { 
                _customers = value;
                NotifyOfPropertyChange(() => Customers);
            }
        }

        public string SelectedProjectCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                _selectedCustomer = value;
                NotifyOfPropertyChange(() => SelectedProjectCustomer);
            }
        }

        public string Comment
        {
            get { return _comment; }
            set 
            {
                _comment = value;
                NotifyOfPropertyChange(() => Comment);
            }
        }

        public async void SaveButton()
        {
            var projectModel = new ProjectModel()
            {
                Id = _project.Id,
                Name = ProjectName,
                Status = SelectedProjectStatus,
                CustomerName = SelectedProjectCustomer,
                Comment = Comment
            };

            var projectFormValidator = new ProjectFormValidator();
            var validationResult = projectFormValidator.Validate(projectModel);

            if(validationResult.IsValid)
            {
                var projectEntity = await projectModel.ConvertToProjectEntity();
                var resultTask = await _projectAccess.UpdateProject(projectEntity);

                if(resultTask)
                {
                    _events.PublishOnUIThread(new ChangedProjectCredentialsEvent(_project.Id));
                    this.TryClose();
                }
                else
                {
                    MessageBox.Show("An error occured.");
                }
            }
            else
            {
                MessageBox.Show("Błąd walidacji danych");
            }
        }



        #endregion
    }
}
