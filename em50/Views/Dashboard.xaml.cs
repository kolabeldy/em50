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

namespace em50.Views
{
    /// <summary>
    /// Логика взаимодействия для Dashboard.xaml
    /// </summary>
    public partial class Dashboard : UserControl
    {
        private MainWindowViewModel dashboard;
        private double maxWidth;
        public Dashboard(MainWindowViewModel dashboard)
        {
            this.dashboard = dashboard;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dashboard.ContentPanel.Content = null;
            dashboard.MenuPanelMaxWidth = 2000;
        }
    }
}
