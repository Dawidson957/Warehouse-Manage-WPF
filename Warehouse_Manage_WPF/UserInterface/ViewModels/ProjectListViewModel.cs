using Caliburn.Micro;
using DataAccess.DataAcc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Warehouse_Manage_WPF.UserInterface.Models;

namespace Warehouse_Manage_WPF.UserInterface.ViewModels
{
    public class ProjectListViewModel : Screen
    {
        private ProjectAccess _projectAccess;
        private BindableCollection<ProjectModel> _projects;
        private SimpleContainer _container { get; set; }
       
        public ProjectModel SelectedProject { get; set; }


        public ProjectListViewModel(SimpleContainer simpleContainer)
        {
            _container = simpleContainer;
            _projectAccess = new ProjectAccess();
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

    }
}
