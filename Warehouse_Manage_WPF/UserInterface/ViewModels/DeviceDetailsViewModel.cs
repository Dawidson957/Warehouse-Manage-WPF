﻿using System;
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

namespace Warehouse_Manage_WPF.UserInterface.ViewModels
{
    public class DeviceDetailsViewModel : Screen
    {
        private ProducerAccess _producerAccess { get; set; }

        private DeviceAccess _deviceAccess { get; set; }


        public DeviceDetailsViewModel(IWindowManager windowManager)
        {
            _producerAccess = new ProducerAccess();
            _deviceAccess = new DeviceAccess();
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
            Producers = new BindableCollection<string>(producersName);
        }

        public override void CanClose(Action<bool> callback)
        {
            if (SomethingChangedFlag)
            {
                MessageBoxResult result = MessageBox.Show("Czy chcesz zapisać zmiany?", "Title", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                    return;
                else
                    callback(true);
            }
            else
                callback(true);
        }

        #endregion


        #region Helpers

        private bool _somethingChangedFlag;

        public bool SomethingChangedFlag
        {
            get { return _somethingChangedFlag; }
            set
            {
                _somethingChangedFlag = value;
            }
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

            if (validationResult.IsValid)
            {
                var deviceEntity = await Device.ConvertToDeviceEntity();
                var resultTask = await _deviceAccess.UpdateDevice(deviceEntity);

                if (resultTask)
                    MessageBox.Show("Device credentials has been changed successfully.");
                else
                    MessageBox.Show("An error occured.");
            }
            else
            {
                MessageBox.Show("Bad credentials.");
            }
        }

        public void DeleteButton()
        {

        }

        #endregion
    }
}
