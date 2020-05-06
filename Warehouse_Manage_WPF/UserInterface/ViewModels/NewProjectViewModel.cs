﻿using Caliburn.Micro;
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
    public class NewProjectViewModel : Screen
    {
		private SimpleContainer _container { get; set; }

		private ICustomerAccess _customerAccess { get; set; }

		private IProjectAccess _projectAccess { get; set; }

		private IEventAggregator _events { get; set; }


		public NewProjectViewModel(SimpleContainer simpleContainer, IEventAggregator eventAggregator, 
							       IProjectAccess projectAccess, ICustomerAccess customerAccess)
		{
			_container = simpleContainer;
			_events = eventAggregator;
			_customerAccess = customerAccess;
			_projectAccess = projectAccess;
		}


        #region Window Operations

        protected override async void OnViewLoaded(object view)
		{
			base.OnViewLoaded(view);
			await LoadCustomers();
		}

		private async Task LoadCustomers()
		{
			var customersName = await _customerAccess.GetCustomersName();
			CustomersName = new BindableCollection<string>(customersName);
		}

        #endregion


        #region Form Controls

        private string _projectName;
		private string _comment;
		private BindableCollection<string> _customersName;
		private string _selectedCustomer;

		public string ProjectName
		{
			get { return _projectName; }
			set 
			{ 
				_projectName = value;
				NotifyOfPropertyChange(() => ProjectName);
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

		public BindableCollection<string> CustomersName
		{
			get { return _customersName; }
			set 
			{ 
				_customersName = value;
				NotifyOfPropertyChange(() => CustomersName);
			}
		}

		public string SelectedCustomer
		{
			get { return _selectedCustomer; }
			set
			{
				_selectedCustomer = value;
				NotifyOfPropertyChange(() => SelectedCustomer);
			}
		}

		public void ClearForm()
		{
			ProjectName = "";
			Comment = "";
		}

		public async void CreateProject()
		{
			var projectFormValidator = new ProjectFormValidator();
			var projectModel = new ProjectModel(_customerAccess)
			{
				Name = this.ProjectName,
				Comment = this.Comment,
				CustomerName = this.SelectedCustomer
			};

			var validationResult = projectFormValidator.Validate(projectModel);

			if(validationResult.IsValid)
			{
				// Save Project
				var projectEntity = await projectModel.ConvertToProjectEntity();

				var addProjectResult = await _projectAccess.AddProject(projectEntity);

				if(addProjectResult)
				{
					_events.PublishOnUIThread(new NewProjectAddedEvent());
					this.TryClose();
				}
				else
				{
					// TODO: Handle case when project is not added
					MessageBox.Show("Wystapil blad");
				}
			}
			else
			{
				//TODO: Handle case when validation is not success
				MessageBox.Show("blad walidacji danych");
			}
		}

        #endregion

    }
}
