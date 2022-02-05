using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Controls;
using em50.Views;
using MyServicesLibrary.Controls.UniversalDataGrid;
using MyServicesLibrary.SideMenu;
using MyServicesLibrary.ViewModelsBase;

namespace em50;
public class MainWindowViewModel : BaseViewModel
{
    const double MenuPanelMaxWidthValue = 2000;
    const double MenuPanelMinWidthValue = 1;
    public SideMenu? SideMenu { get; set; }


    private UserControl _ContentPanel = new();
    public UserControl ContentPanel
    {
        get => _ContentPanel;
        set
        {
            Set(ref _ContentPanel, value);
        }
    }
    private double _MenuPanelMaxWidth = new();
    public double MenuPanelMaxWidth
    {
        get => _MenuPanelMaxWidth;
        set
        {
            Set(ref _MenuPanelMaxWidth, value);
        }
    }


    public MainWindowViewModel()
    {
        MenuPanelMaxWidth = MenuPanelMaxWidthValue;
        string settingPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.db");
        SideMenu = new MyServicesLibrary.SideMenu.SideMenu(settingPath);
        SideMenu.OnMenuChoise += MainMenuChoiseCommand;
    }

    private void MainMenuChoiseCommand(string methodName)
    {
        if (methodName != null)
        {
            MethodInfo? method = typeof(MainWindowViewModel).GetMethod(methodName);
            if (method != null) method.Invoke(this, null);
        }

    }

    public void MonitorMonthShow()
    {
        MenuPanelMaxWidth = MenuPanelMaxWidthValue;
    }


    public void DashboardShow()
    {
        MenuPanelMaxWidth = MenuPanelMinWidthValue;
        UserControl newPanel = new Dashboard(this);
        ContentPanel.Content = newPanel;
        //ContentPanel = null;
        //MenuPanelMaxWidth = MenuPanelMaxWidthValue;
    }
    public void AnalysisFromCCShow()
    {
        MenuPanelMaxWidth = MenuPanelMaxWidthValue;
        ContentPanel.Content = AnalysisFromCC.GetInstance();
    }


}
