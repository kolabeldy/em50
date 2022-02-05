using System.Windows;

namespace em50;
public partial class MainWindow : Window
{
    public MainWindow()
    {
        MainWindowViewModel model = new();
        DataContext = model;
        InitializeComponent();
    }
}
