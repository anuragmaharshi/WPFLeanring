using NLog;
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

namespace RecordTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        public MainWindow()
        {
            try
            {
                _logger.Info("Inside Mainwindow .xaml");
                InitializeComponent();
                this.Height = SystemParameters.WorkArea.Height;
                this.Width = SystemParameters.WorkArea.Width;
                this.WindowStyle = WindowStyle.SingleBorderWindow;
                this.WindowState = WindowState.Maximized;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                _logger.Error("Some error occures"+e.StackTrace);
            }
        }
            
    }
}
