using Caliburn.Micro;
using DataAccess.DataAcc;
using System.Threading.Tasks;
using System.Windows;
using Warehouse_Manage_WPF.UserInterface.EventModels;
using Warehouse_Manage_WPF.UserInterface.Models;
using Warehouse_Manage_WPF.Validators;

namespace Warehouse_Manage_WPF.UserInterface.ViewModels
{
    public class ProjectDetailsViewModel : Screen
    {
        private IEventAggregator _events { get; set; }

        private ICustomerAccess _customerAccess { get; set; }

        private IProjectAccess _projectAccess { get; set; }

        public ProjectModel project { get; private set; }


        public ProjectDetailsViewModel(IEventAggregator eventAggregator, ICustomerAccess customerAccess, IProjectAccess projectAccess)
        {
            _events = eventAggregator;
            _customerAccess = customerAccess;
            _projectAccess = projectAccess;
        }


        #region Window Operations

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadCustomers();
            LoadProjectStatus();
            SetFormValues();
        }

        public void LoadProject(ProjectModel projectModel)
        {
            project = projectModel;
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
            if(project.Status != null)
            {
                if(ProjectStatus.Contains(project.Status))
                {
                    SelectedProjectStatus = project.Status;
                }
            }

            if(project.CustomerName != null)
            {
                if(Customers.Contains(project.CustomerName))
                {
                    SelectedProjectCustomer = project.CustomerName;
                }
            }

            ProjectName = project.Name;
            Comment = project.Comment;
        }

        #endregion


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
            var projectModel = new ProjectModel(_customerAccess)
            {
                Id = project.Id,
                Name = ProjectName,
                Status = SelectedProjectStatus,
                CustomerName = SelectedProjectCustomer,
                Comment = Comment
            };

            var projectFormValidator = new ProjectFormValidator();
            var validationResult = projectFormValidator.Validate(projectModel);

            // For tests
            ProjectValidationResult = validationResult.IsValid;

            if(validationResult.IsValid)
            {
                var projectEntity = await projectModel.ConvertToProjectEntity();
                var resultTask = await _projectAccess.UpdateProject(projectEntity);

                // For tests
                UpdateProjectResult = resultTask;

                if(resultTask)
                {
                    _events.PublishOnUIThread(new ChangedProjectCredentialsEvent(project.Id));
                    TryClose();
                }
                else
                {
                    MessageBox.Show("Wystąpił błąd podczas zapisu danych. Spróbuj ponownie");
                }
            }
            else
            {
                MessageBox.Show("Błąd walidacji danych");
            }
        }

        #endregion


        #region Only For Tests

        public bool ProjectValidationResult { get; private set; } = false;

        public bool UpdateProjectResult { get; private set; } = false;

        public async void LoadCustomers_Run() { await LoadCustomers(); }

        public void LoadProjectStatus_Run() { LoadProjectStatus(); }

        public void SetFormValues_Run() { SetFormValues(); }

        #endregion
    }
}
