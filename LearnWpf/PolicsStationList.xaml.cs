using LearnWpf.ViewModel;
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

namespace LearnWpf
{
    /// <summary>
    /// Interaction logic for PolicsStationList.xaml
    /// </summary>
    public partial class PolicsStationList : UserControl
    {
        public PolicsStationList()
        {
            InitializeComponent();
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            //PoliceStationListViewModel model = new PoliceStationListViewModel();
           // model.LoadData();
           // this.DataContext = model;
            //grid.ItemsSource = model.policeStations;
        }

      
        
    }
}
