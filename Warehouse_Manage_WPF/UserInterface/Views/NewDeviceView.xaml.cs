using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Warehouse_Manage_WPF.UserInterface.Views
{
    /// <summary>
    /// Interaction logic for NewDeviceView.xaml
    /// </summary>
    public partial class NewDeviceView : UserControl
    {
        public NewDeviceView()
        {
            InitializeComponent();
        }

        private void NewProducerWindowOpenButton_Click(object sender, RoutedEventArgs e)
        {
            NewProducerTitle.Visibility = Visibility.Visible;
            NewProducerName.Visibility = Visibility.Visible;
            NewProducerURL.Visibility = Visibility.Visible;
            SaveNewProducer.Visibility = Visibility.Visible;
        }
    }
}
