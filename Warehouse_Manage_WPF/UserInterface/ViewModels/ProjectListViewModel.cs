using Caliburn.Micro;
using DataAccess.DataAcc;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Warehouse_Manage_WPF.UserInterface.EventModels;
using Warehouse_Manage_WPF.UserInterface.Models;

namespace Warehouse_Manage_WPF.UserInterface.ViewModels
{
    public class ProjectListViewModel : Screen, IHandle<NewProjectAddedEvent>
    {
        private SimpleContainer _container { get; set; }

        private ProjectAccess _projectAccess { get; set; }

        private IWindowManager _windowManager { get; set; }
       

        public ProjectListViewModel(SimpleContainer simpleContainer, IWindowManager windowManager, IEventAggregator eventAggregator)
        {
            _container = simpleContainer;
            _projectAccess = (ProjectAccess)_container.GetInstance(typeof(IProjectAccess), typeof(ProjectAccess).ToString());
            _windowManager = windowManager;
            eventAggregator.Subscribe(this);
        }


        #region Window Operations

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadProjects();
        }

        private async Task LoadProjects()
        {
            var projectEnts = await _projectAccess.GetProjectsInfo();

            // Removes Warehouse project from list
            projectEnts.Remove(projectEnts.First(x => x.Id == 5));

            Projects = new BindableCollection<ProjectModel>();

            foreach (var projectEnt in projectEnts)
                Projects.Add(new ProjectModel(projectEnt));


            Console.WriteLine("Loaded Projects");
        }

        #endregion


        #region Project List 

        private BindableCollection<ProjectModel> _projects;

        public ProjectModel SelectedProject { get; set; }

        public BindableCollection<ProjectModel> Projects
        {
            get { return _projects; }
            set
            {
                _projects = value;
                NotifyOfPropertyChange(() => Projects);
            }
        }

        public async void MouseDoubleClick_DataGrid()
        {
            if (SelectedProject != null)
            {
                var mainViewConductor = (MainViewModel)this.Parent;
                var projectVM = _container.GetInstance<ProjectViewModel>();
                await projectVM.LoadProject2(SelectedProject.Id);
                mainViewConductor.DeactivateItem(mainViewConductor.ActiveItem, true);
                mainViewConductor.ActivateItem(projectVM);
            }
            else
            {
                MessageBox.Show("Selected Project is null");
            }
        }

        #endregion


        #region Events

        public async void Handle(NewProjectAddedEvent newProjectAddedEvent)
        {
            await LoadProjects();
            SnackbarNotification.MessageQueue = new SnackbarMessageQueue();
            SnackbarNotification.MessageQueue.Enqueue("Projekt został dodany pomyślnie.");
        }

        #endregion


        #region Snackbar PopUp Notification

        private Snackbar _snackbarNotification;

        public Snackbar SnackbarNotification
        {
            get { return _snackbarNotification; }
            set
            {
                _snackbarNotification = value;
                NotifyOfPropertyChange(() => SnackbarNotification);
            }
        }

        public void SnackbarLoaded(object sender)
        {
            SnackbarNotification = (Snackbar)sender;
        }

        #endregion


        #region Button New Project

        public void AddNewProject()
        {
            var newProjectVM = _container.GetInstance<NewProjectViewModel>();
            _windowManager.ShowDialog(newProjectVM);
        }

        #endregion

    }
}
