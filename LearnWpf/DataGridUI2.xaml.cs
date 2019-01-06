using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LearnWpf
{
    /// <summary>
    /// Interaction logic for DataGridUI2.xaml
    /// </summary>
    public partial class DataGridUI2 : Window
    {
        
        LetterRecord singleData;
        IEnumerable<string> listPoliceStation, listTopic, listPoliceOfficer;
        public DataGridUI2()
        {
            //CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            //ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            //Thread.CurrentThread.CurrentCulture = ci;
            InitializeComponent();
            this.Height = SystemParameters.WorkArea.Height;
            this.Width = SystemParameters.WorkArea.Width;
            this.WindowStyle = WindowStyle.SingleBorderWindow;
            this.WindowState = WindowState.Maximized;
            //this.SizeToContent = SizeToContent.WidthAndHeight;
            var Data = InitialiseAndLoadData.GetData();
            Dgrid1.ItemsSource = Data;

            listPoliceStation = Data.Select(o => o.PoliceStation).Distinct();
          
            PSFilter.ItemsSource = listPoliceStation;

            listTopic = Data.Select(o => o.TopicArea).Distinct();
          
            TopicFilter.ItemsSource = listTopic;

            listPoliceOfficer = Data.Select(o => o.PoliceOfficer).Distinct();
            
            POFilter.ItemsSource = listPoliceOfficer;

            singleData = InitialiseAndLoadData.GetSingleData();

            Dgrid1.Width = this.Width;
            Dgrid1.Height = this.Height;
        }
        private void Dgrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
            DataGridCell RowColumn = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
            string CellValue = ((TextBlock)RowColumn.Content).Text;
           
            RowColumn = dataGrid.Columns[1].GetCellContent(row).Parent as DataGridCell;
           
            RowColumn = dataGrid.Columns[6].GetCellContent(row).Parent as DataGridCell;
           

            var data = e.AddedItems[0] as LetterRecord;
            singleData = data;
            
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var picker = sender as DatePicker;
            DateTime? date = picker.SelectedDate;

            if (date == null)
            {
                this.Title = "No date";
            }
            else
            {
                this.Title = date.Value.ToShortDateString();
            }
        }

        

        private void Dgrid1_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            /*
            foreach (DataGridColumn col in Dgrid1.Columns)
            {
                FrameworkElement cellContent = col.GetCellContent(e.Row);
                
                //FrameworkElement cellContent = col.GetCellContent(0);
                ComboBox combo = cellContent as ComboBox;
                if (combo != null)
                {
                    if (combo.Tag.ToString() == "Combo")
                    {
                        combo.ItemsSource = listPoliceStation;
                        combo.DisplayMemberPath = "Name";
                    }
                }
                //TextBox tbitem = cellContent as TextBox;
            }

            DataGridRow row = e.Row;
            DataGridColumn 
            */
           cbxPS.ItemsSource = listPoliceStation;
           cbxTA.ItemsSource = listTopic;
           cbxPO.ItemsSource = listPoliceOfficer;
        }
    }
}
