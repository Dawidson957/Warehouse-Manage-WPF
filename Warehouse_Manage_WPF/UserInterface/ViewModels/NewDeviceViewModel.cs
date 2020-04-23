using Caliburn.Micro;
using DataAccess.DataAcc;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Warehouse_Manage_WPF.UserInterface.Models;
using Warehouse_Manage_WPF.Validators;

namespace Warehouse_Manage_WPF.UserInterface.ViewModels
{
    public class NewDeviceViewModel : Screen
    {
		private BindableCollection<string> _producersName;
		private ProducerAccess producerAccess;
		private DeviceAccess deviceAccess;
		private Snackbar _snackbarNewDeviceForm;

		public NewDeviceViewModel()
		{
			producerAccess = new ProducerAccess();
			deviceAccess = new DeviceAccess();
		}

		protected override async void OnViewLoaded(object view)
		{
			base.OnViewLoaded(view);
			await LoadProducers();
		}

		private async Task LoadProducers()
		{
			var producersFromAPI = await producerAccess.GetProducerNamesAll();
			ProducersName = new BindableCollection<string>(producersFromAPI);
		}

		public BindableCollection<string> ProducersName
		{
			get { return _producersName; }
			set 
			{ 
				_producersName = value;
				NotifyOfPropertyChange(() => ProducersName);
			}
		}

		public Snackbar SnackbarNotification
		{
			get { return _snackbarNewDeviceForm; }
			set
			{
				_snackbarNewDeviceForm = value;
				NotifyOfPropertyChange(() => SnackbarNotification);
			}
		}

		public void SnackbarLoaded(object sender)
		{
			SnackbarNotification = (Snackbar)sender;
		}

		/// <summary>
		/// Controls for adding new producer form
		/// </summary>
		#region NewProducerForm

		private string _producerName;
		private string _url;


		public string ProducerName
		{
			get { return _producerName; }
			set 
			{ 
				_producerName = value;
				NotifyOfPropertyChange(() => ProducerName);
			}
		}

		public string URL
		{
			get { return _url; }
			set 
			{ 
				_url = value;
				NotifyOfPropertyChange(() => URL);
			}
		}

		public async void SaveNewProducer()
		{
			var producer = new ProducerModel
			{
				Name = ProducerName,
				URL = URL
			};

			var producerValidator = new ProducerValidator();
			var result = producerValidator.Validate(producer);
			

			if (result.IsValid)
			{
				var producerEntity = producer.ConvertToProducerEntity();
				var resultTask = await producerAccess.AddProducer(producerEntity);
				SnackbarNotification.MessageQueue = new SnackbarMessageQueue();
				string message = null;

				if (resultTask)
				{
					message = "Producent dodany poprawnie.";
				}
				else
				{
					message = "Producent nie zostal dodany";
				}

				SnackbarNotification.MessageQueue.Enqueue(message);
				ClearProducerForm();
				await LoadProducers();
			}
			else
			{
				SnackbarNotification.MessageQueue = new SnackbarMessageQueue();
				SnackbarNotification.MessageQueue.Enqueue("Błąd wprowadzonych danych, producent nie został dodany.");
			}
		}

		public void ClearProducerForm()
		{
			ProducerName = "";
			URL = "";
		}

		#endregion


		/// <summary>
		/// Controls for adding new device form
		/// </summary>
		#region NewDeviceForm

		private string _deviceName;
		private string _articleNumber;
		private string _selectedProducerName;
		private string _location;
		private int _quantity;
		


		public string DeviceName
		{
			get { return _deviceName; }
			set 
			{ 
				_deviceName = value;
				NotifyOfPropertyChange(() => DeviceName);
			}
		}

		public string ArticleNumber
		{
			get { return _articleNumber; }
			set 
			{ 
				_articleNumber = value;
				NotifyOfPropertyChange(() => ArticleNumber);
			}
		}

		public string SelectedProducerName
		{
			get { return _selectedProducerName; }
			set 
			{ 
				_selectedProducerName = value;
				NotifyOfPropertyChange(() => SelectedProducerName);
			}
		}

		public string Location
		{
			get { return _location; }
			set 
			{ 
				_location = value;
				NotifyOfPropertyChange(() => Location);
			}
		}

		public int Quantity
		{
			get { return _quantity; }
			set 
			{ 
				_quantity = value;
				NotifyOfPropertyChange(() => Quantity);
			}
		}

		public async void SaveButton()
		{
			var device = new DeviceModel
			{
				Name = DeviceName,
				ArticleNumber = ArticleNumber,
				ProducerName = SelectedProducerName,
				Location = Location,
				Quantity = Quantity
			};

			var deviceValidator = new DeviceModelValidator();
			var result = deviceValidator.Validate(device);

			if (result.IsValid)
			{
				var deviceEntity = await device.ConvertToDeviceEntity();
				deviceEntity.ProjectID = 5;
				var resultTask = await deviceAccess.AddDevice(deviceEntity);
				SnackbarNotification.MessageQueue = new SnackbarMessageQueue();
				string message = null;

				if (resultTask)
				{
					message = "Urządzenie zostało dodane poprawnie.";
				}
				else
				{
					message = "Urządzenie nie zostało dodane.";
				}

				SnackbarNotification.MessageQueue.Enqueue(message);
				ClearDeviceForm();
			}
			else
			{
				SnackbarNotification.MessageQueue = new SnackbarMessageQueue();
				SnackbarNotification.MessageQueue.Enqueue("Błąd wprowadzonych danych, urządzenie nie zostało dodane.");
			}
		}

		public void ClearButton()
		{
			ClearDeviceForm();
		}

		public void ClearDeviceForm()
		{
			DeviceName = "";
			ArticleNumber = "";
			Location = "";
			Quantity = 0;
		}

		#endregion

	}
}
