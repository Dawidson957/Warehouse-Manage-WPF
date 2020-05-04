using Caliburn.Micro;
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
    public class CustomerDetailsViewModel : Screen
    {
        private CustomerAccess _customers;

        private IEventAggregator _events;

        private int _customerId;

        public CustomerDetailsViewModel(IEventAggregator eventAggregator)
        {
            _customers = new CustomerAccess();
            _events = eventAggregator;
        }

        public void LoadCustomer(CustomerModel customerModel)
        {
            _customerId = customerModel.Id;
            CustomerName = customerModel.Name;
            CustomerAddress = customerModel.Address;
            CustomerCity = customerModel.City;
        }


        #region Form Controls

        private string _customerName;
        private string _customerAddress;
        private string _customerCity;


        public string CustomerName
        {
            get { return _customerName; }
            set 
            { 
                _customerName = value;
                NotifyOfPropertyChange(() => CustomerName);
            }
        }

        public string CustomerAddress
        {
            get { return _customerAddress; }
            set 
            {
                _customerAddress = value;
                NotifyOfPropertyChange(() => CustomerAddress);
            }
        }

        public string CustomerCity
        {
            get { return _customerCity; }
            set 
            { 
                _customerCity = value;
                NotifyOfPropertyChange(() => CustomerCity);
            }
        }

        public async void SaveButton()
        {
            var customerModel = new CustomerModel()
            {
                Name = CustomerName,
                Address = CustomerAddress,
                City = CustomerCity
            };

            var customerFormValidator = new CustomerFormValidator();
            var validationResult = customerFormValidator.Validate(customerModel);

            if(validationResult.IsValid)
            {
                var customerEntity = customerModel.ConvertToCustomerEntity();
                customerEntity.Id = _customerId;

                var resultTask = await _customers.UpdateCustomer(customerEntity);

                if(resultTask)
                {
                    _events.PublishOnUIThread(new CustomerCredentialsChangedEvent());
                    this.TryClose();
                }
                else
                {
                    MessageBox.Show("An error occured");
                }
            }
            else
            {
                MessageBox.Show("Błąd walidacji danych");
            }
        }

        #endregion
    }
}
