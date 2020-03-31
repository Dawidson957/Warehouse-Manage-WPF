using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Warehouse_Manage_WPF.UserInterface.Models;
using Warehouse_Manage_WPF.DataAccess;

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

        private DataAPI _dataAPI { get; set; }

        public Device DeviceModel { get; set; }

        public BindableCollection<string> Producers { get; set; }

        public DeviceDetailsViewModel(Device device)
        {
            _dataAPI = new DataAPI();
            LoadProducers();
        }

        private void LoadProducers()
        {
            Producers = new BindableCollection<string>();
            var producersFromAPI = _dataAPI.GetAllProducers();

            foreach (var producer in producersFromAPI)
                Producers.Add(producer.Name);
        }

        public void LoadDevice(Device device)
        {
            Name = device.Name;
            ArticleNumber = device.ArticleNumber;
            ProducerName = device.ProducerName;
            Quantity = device.Quantity;
            Location = device.Location;
        }

        public string Name
        {
            get { return _name; }
            set 
            { 
                _name = value; 
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
            }
        }

        public string Location
        {
            get { return _location; }
            set 
            { 
                _location = value; 
            }
        }

        public string ProducerName
        {
            get { return _producerName; }
            set 
            { 
                _producerName = value; 
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

    }
}
