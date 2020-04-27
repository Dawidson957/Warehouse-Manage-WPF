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
        private ProjectAccess _projectAccess;
        private BindableCollection<ProjectModel> _projects;
        private SimpleContainer _container { get; set; }

        private IWindowManager _windowManager;
       
        public ProjectModel SelectedProject { get; set; }

        


        public ProjectListViewModel(SimpleContainer simpleContainer, IWindowManager windowManager, IEventAggregator eventAggregator)
        {
            _container = simpleContainer;
            _projectAccess = new ProjectAccess();
            _windowManager = windowManager;
            eventAggregator.Subscribe(this);
        }


        public BindableCollection<ProjectModel> Projects
        {
            get { return _projects; }
            set 
            { 
                _projects = value;
                NotifyOfPropertyChange(() => Projects);
            }
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadProjects();
        }

        private async Task LoadProjects()
        {
            var projectEnts = await _projectAccess.GetProjectsInfo();
            Projects = new BindableCollection<ProjectModel>();

            foreach (var projectEnt in projectEnts)
                Projects.Add(new ProjectModel(projectEnt));


            Console.WriteLine("Loaded Projects");
        }

        public void SnackbarLoaded(object sender)
        {
            SnackbarNotification = (Snackbar)sender;
        }

        public void MouseDoubleClick_DataGrid()
        {
            if(SelectedProject != null)
            {
                var mainViewConductor = (MainViewModel)this.Parent;
                var projectVM = _container.GetInstance<ProjectViewModel>();
                projectVM.LoadProject(SelectedProject);
                mainViewConductor.DeactivateItem(mainViewConductor.ActiveItem, true);
                mainViewConductor.ActivateItem(projectVM);
            }
            else
            {
                MessageBox.Show("Selected Project is NULL!!!!!!!!!!!!!!1");
            }
        }

        public void AddNewProject()
        {
            var newProjectVM = _container.GetInstance<NewProjectViewModel>();
            _windowManager.ShowDialog(newProjectVM);
        }


        public async void Handle(NewProjectAddedEvent newProjectAddedEvent)
        {
            await LoadProjects();
            SnackbarNotification.MessageQueue = new SnackbarMessageQueue();
            SnackbarNotification.MessageQueue.Enqueue("Projekt został dodany pomyślnie.");
        }

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



    }
}
