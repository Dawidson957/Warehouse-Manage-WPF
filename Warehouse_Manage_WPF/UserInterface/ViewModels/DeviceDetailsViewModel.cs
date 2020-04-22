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

namespace Warehouse_Manage_WPF.UserInterface.ViewModels
{
    public class DeviceDetailsViewModel : Screen
    {
        private string _name;
        private string _articleNumber;
        private int _quantity;
        private string _location;
        private string _producerName;
        private bool _somethingChangedFlag;

        public BindableCollection<string> Producers { get; set; }

        public DeviceDetailsViewModel(IWindowManager windowManager)
        {
            
            LoadProducers();
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

        private void LoadProducers()
        {
            
        }

        public string Name
        {
            get { return _name; }
            set 
            { 
                _name = value;
                NotifyOfPropertyChange(() => Name);
                SomethingChangedFlag = true;
            }
        }

        public string ArticleNumber
        {
            get { return _articleNumber; }
            set 
            { 
                _articleNumber = value;
                NotifyOfPropertyChange(() => ArticleNumber);
                SomethingChangedFlag = true;
            }
        }

        public int Quantity
        {
            get { return _quantity; }
            set 
            { 
                _quantity = value;
                NotifyOfPropertyChange(() => Quantity);
                SomethingChangedFlag = true;
            }
        }

        public string Location
        {
            get { return _location; }
            set 
            { 
                _location = value;
                NotifyOfPropertyChange(() => Location);
                SomethingChangedFlag = true;
            }
        }

        public string ProducerName
        {
            get { return _producerName; }
            set 
            { 
                _producerName = value;
                NotifyOfPropertyChange(() => ProducerName);
                SomethingChangedFlag = true;
            }
        }

        public bool SomethingChangedFlag
        {
            get { return _somethingChangedFlag; }
            set 
            { 
                _somethingChangedFlag = value; 
            }
        }

        public void SubmitButton()
        {
            /*
            var device = new DeviceModel(deviceModel.Id, Name, ArticleNumber, Location, Quantity, ProducerName);
            var deviceValidator = new DeviceValidator();
            var result = deviceValidator.Validate(device);

            if (result.IsValid)
            {
                var converter = new EntitiesConverter();
                var resultt = _dataAPI.UpdateDevice(converter.DeviceToEntityDevice(device));

                if (resultt)
                {
                    SomethingChangedFlag = false;
                    MessageBox.Show("Evertything is so good.");
                }
                else
                    MessageBox.Show("Something bad happend");
            }
            else
            {
                string resultString = "";

                foreach (var failure in result.Errors)
                    resultString += "Property: " + failure.PropertyName + " - Error: " + failure.ErrorMessage + "\n";

                MessageBox.Show(resultString);
            }
            */
        }

        public void DeleteButton()
        {
            /*
            MessageBoxResult result = MessageBox.Show("Na pewno chcesz usunąć?", "Title", MessageBoxButton.YesNo);

            if(result == MessageBoxResult.Yes)
            {
                var device = new DeviceModel(deviceModel.Id, Name, ArticleNumber, Location, Quantity, ProducerName);
                var converter = new EntitiesConverter();
                var resultt = _dataAPI.DeleteDevice(converter.DeviceToEntityDevice(device));

                if (resultt)
                    MessageBox.Show("Everything is good");
                else
                    MessageBox.Show("Something bad happend");
            }
            */
        }
    }
}
