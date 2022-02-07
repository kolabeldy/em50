using System.Windows;

namespace em50;
public partial class MainWindow : Window
{
    public MainWindowViewModel model;
    public MainWindow()
    {
        model = new();
        DataContext = model;
        InitializeComponent();
    }
}
