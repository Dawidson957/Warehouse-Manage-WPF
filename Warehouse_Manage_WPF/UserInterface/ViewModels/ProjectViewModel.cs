using Caliburn.Micro;
using DataAccess.DataAcc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Manage_WPF.UserInterface.Models;

namespace Warehouse_Manage_WPF.UserInterface.ViewModels
{
    public class ProjectViewModel : Screen
    {
		private BindableCollection<DeviceModel> _projectDevices;
		private ProjectModel _project;
		private ProjectAccess _projectAccess;

		public ProjectModel projectModel
		{
			get { return _project; }
			set
			{
				_project = value;
				NotifyOfPropertyChange(() => projectModel);
			}
		}


		public ProjectViewModel()
		{
			_projectAccess = new ProjectAccess();
		}

		public BindableCollection<DeviceModel> ProjectDevices
		{
			get { return _projectDevices; }
			set 
			{ 
				_projectDevices = value;
				NotifyOfPropertyChange(() => ProjectDevices);
			}
		}

		public void LoadProject(ProjectModel projectModel)
		{
			_project = projectModel;
		}

		public async void TestAddDevice()
		{
			var projectfromdb = await _projectAccess.GetProjectById(1);
			Console.WriteLine();
		}

	}
}

