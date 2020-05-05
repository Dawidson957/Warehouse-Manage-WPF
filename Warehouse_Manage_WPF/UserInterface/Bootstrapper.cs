using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using DataAccess.DataAcc;
using Warehouse_Manage_WPF.UserInterface.ViewModels;

namespace Warehouse_Manage_WPF.UserInterface
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer _container = new SimpleContainer();

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            _container.Instance(_container);

            _container.RegisterSingleton(typeof(IDeviceAccess), typeof(DeviceAccess).ToString(), typeof(DeviceAccess));
            _container.RegisterSingleton(typeof(IProducerAccess), typeof(ProducerAccess).ToString(), typeof(ProducerAccess));
            _container.RegisterSingleton(typeof(ICustomerAccess), typeof(CustomerAccess).ToString(), typeof(CustomerAccess));
            _container.RegisterSingleton(typeof(IProjectAccess), typeof(ProjectAccess).ToString(), typeof(ProjectAccess));

            _container
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>();

            GetType().Assembly.GetTypes()
                .Where(type => type.IsClass)
                .Where(type => type.Name.EndsWith("ViewModel"))
                .ToList()
                .ForEach(viewModelType => _container.RegisterPerRequest(
                    viewModelType, viewModelType.ToString(), viewModelType));
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<MainViewModel>();
        }
    }
}
