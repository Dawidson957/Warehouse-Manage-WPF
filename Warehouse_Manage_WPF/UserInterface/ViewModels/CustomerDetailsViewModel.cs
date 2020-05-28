using Caliburn.Micro;
using DataAccess.DataAcc;
using System.Windows;
using Warehouse_Manage_WPF.UserInterface.EventModels;
using Warehouse_Manage_WPF.UserInterface.Models;
using Warehouse_Manage_WPF.Validators;

namespace Warehouse_Manage_WPF.UserInterface.ViewModels
{
    public class CustomerDetailsViewModel : Screen
    {
        private IEventAggregator _events { get; set; }

        private ICustomerAccess _customers { get; set; }

        public int customerId { get; private set; }


        public CustomerDetailsViewModel(IEventAggregator eventAggregator, ICustomerAccess customerAccess)
        {
            _events = eventAggregator;
            _customers = customerAccess;
        }

        public void LoadCustomer(CustomerModel customerModel)
        {
            customerId = customerModel.Id;
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

            // For testing
            CustomerValidationResult = validationResult.IsValid;

            if(validationResult.IsValid)
            {
                var customerEntity = customerModel.ConvertToCustomerEntity();
                customerEntity.Id = customerId;

                var resultTask = await _customers.UpdateCustomer(customerEntity);

                // For testing
                CustomerUpdateResult = resultTask;

                if(resultTask)
                {
                    _events.PublishOnUIThread(new CustomerCredentialsChangedEvent());
                    this.TryClose();
                }
                else
                {
                    MessageBox.Show("Błąd podczas zapisu danych. Spróbuj ponownie");
                }
            }
            else
            {
                MessageBox.Show("Błąd walidacji danych");
            }
        }

        #endregion


        #region Only For Tests

        public bool CustomerValidationResult { get; private set; } = false;

        public bool CustomerUpdateResult { get; private set; } = false;

        #endregion

    }
}
