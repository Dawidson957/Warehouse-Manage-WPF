using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Windows;
using Warehouse_Manage_WPF.Validators;
using Warehouse_Manage_WPF.UserInterface.Helpers;
using Warehouse_Manage_WPF.UserInterface.Models;
using DataAccess.DataAcc;
using Warehouse_Manage_WPF.UserInterface.EventModels;

namespace Warehouse_Manage_WPF.UserInterface.ViewModels
{
    public class DeviceDetailsViewModel : Screen
    {
        private IEventAggregator _events { get; set; }

        private IProducerAccess _producerAccess { get; set; }

        private IDeviceAccess _deviceAccess { get; set; }



        public DeviceDetailsViewModel(IEventAggregator eventAggregator, IProducerAccess producerAccess, IDeviceAccess deviceAccess)
        {
            _events = eventAggregator;
            _producerAccess = producerAccess;
            _deviceAccess = deviceAccess;
        }


        #region Window Operations

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadProducers();
        }

        public void LoadDevice(DeviceModel device)
        {
            Device = device;
        }

        private async Task LoadProducers()
        {
            var producersName = await _producerAccess.GetProducerNamesAll();
            Producers = new BindableCollection<string>();

            foreach (var name in producersName)
                Producers.Add(name);
        }

        #endregion


        #region Form Controls

        private DeviceModel _deviceModel;
        private BindableCollection<string> _producers;


        public DeviceModel Device
        {
            get { return _deviceModel; }
            set
            {
                _deviceModel = value;
                NotifyOfPropertyChange(() => Device);
            }
        }

        public BindableCollection<string> Producers
        {
            get { return _producers; }
            set
            {
                _producers = value;
                NotifyOfPropertyChange(() => Producers);
            }
        }

        public async Task SubmitButton()
        {
            var deviceValidator = new DeviceModelValidator();
            var validationResult = deviceValidator.Validate(Device);

            // For testing
            DeviceValidationResult = validationResult.IsValid;

            if (validationResult.IsValid)
            {
                var deviceEntity = await Device.ConvertToDeviceEntity();
                var resultTask = await _deviceAccess.UpdateDevice(deviceEntity);

                // For testing
                DeviceUpdateResult = resultTask;

                if (resultTask)
                {
                    _events.PublishOnUIThread(new DeviceCredentialsChangedEvent());
                    this.TryClose();
                }
                else
                    MessageBox.Show("An error occured.");
            }
            else
            {
                MessageBox.Show("Bad credentials.");
            }
        }

        public async void DeleteButton()
        {
            var resultTask = await _deviceAccess.DeleteDevice(Device.Id);

            // For testing
            DeviceDeleteResult = resultTask;

            if(resultTask)
            {
                _events.PublishOnUIThread(new DeviceCredentialsChangedEvent());
                this.TryClose();
            }
            else
            {
                MessageBox.Show("An error occured.");
            }
        }

        #endregion


        #region For testing

        public bool DeviceValidationResult { get; private set; } = false;

        public bool DeviceUpdateResult { get; private set; } = false;

        public bool DeviceDeleteResult { get; private set; } = false;

        #endregion
    }
}
