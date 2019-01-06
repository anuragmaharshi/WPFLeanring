using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace LearnWpf
{
    /// <summary>
    /// Interaction logic for DataGridUI.xaml
    /// </summary>
    public partial class DataGridUI : Window
    {
        LetterRecord singleData;
        public DataGridUI()
        {
            InitializeComponent();
            var Data= InitialiseAndLoadData.GetData();
            Dgrid1.ItemsSource = Data;

            var listPoliceStation = Data.Select(o => o.PoliceStation).Distinct();
            PSList.ItemsSource = listPoliceStation;
            PSFilter.ItemsSource = listPoliceStation;

            var listTopic= Data.Select(o => o.TopicArea).Distinct();
            TopicList.ItemsSource = listTopic;
            TopicFilter.ItemsSource = listTopic;
            
            var listPoliceOfficer = Data.Select(o => o.PoliceOfficer).Distinct();
            PoliceOfficer.ItemsSource = listPoliceOfficer;
            POFilter.ItemsSource = listPoliceOfficer;

            singleData = InitialiseAndLoadData.GetSingleData();

            Refresh();
        }

        private void Dgrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
            DataGridCell RowColumn = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
            string CellValue = ((TextBlock)RowColumn.Content).Text;
            PSList.SelectedItem = CellValue;
            RowColumn = dataGrid.Columns[1].GetCellContent(row).Parent as DataGridCell;
            TopicList.SelectedItem = ((TextBlock)RowColumn.Content).Text;
            RowColumn = dataGrid.Columns[6].GetCellContent(row).Parent as DataGridCell;
            PoliceOfficer.SelectedItem = ((TextBlock)RowColumn.Content).Text;

            var data = e.AddedItems[0] as LetterRecord;
            singleData = data;
            Refresh();
        }

        private void Refresh()
        {
            Reciptdate.SelectedDate = singleData.ReciptDate;
            LetterNo.Text = singleData.LetterNumber.ToString();
            DRno.Text = singleData.DRNumber.ToString();
            //DrDt.SelectedDate = singleData.dRDate;
            Remarks.Text = singleData.Remarks;
        }
    }
}
