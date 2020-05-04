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
    public class CustomersViewModel : Screen
    {
        private CustomerAccess _customers;

        public CustomersViewModel()
        {
            _customers = new CustomerAccess();
        }

        #region Window Operations

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadCustomers();
        }

        private async Task LoadCustomers()
        {
            var customers = await _customers.GetCustomers();
            Customers = new BindableCollection<CustomerModel>();

            foreach (var cust in customers)
                Customers.Add(new CustomerModel(cust));
        }

        #endregion


        #region Customers List

        private BindableCollection<CustomerModel> customers;

        public BindableCollection<CustomerModel> Customers
        {
            get { return customers; }
            set
            {
                customers = value;
                NotifyOfPropertyChange(() => Customers);
            }
        }

        public void EditCustomer(object dataContext)
        {
            var customer = (CustomerModel)dataContext;

            // TODO: Add window for editing customer details
        }

        #endregion


    }
}
