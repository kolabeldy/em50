global using System;
global using System.Collections.Generic;
global using System.IO;
global using System.Globalization;
global using System.Windows;
global using System.Windows.Markup;
global using System.ComponentModel;
global using System.Runtime.CompilerServices;
global using System.Windows.Controls;
global using System.Linq;
global using System.Collections.ObjectModel;
global using System.Windows.Controls.Primitives;
global using System.Reflection;
global using System.Data;
global using System.Windows.Data;

global using MaterialDesignThemes.Wpf;

global using em50.Models;
global using em50.MenuPages.Analysis.FromCC;
global using em50.Models.Base;

global using MyServicesLibrary.ViewModelsBase;
global using MyServicesLibrary.Helpers;
global using MyServicesLibrary.Infrastructure.MessageBoxes;
global using MyServicesLibrary.DataAccess;
global using MyServicesLibrary.Controls.CheckedTree;
global using MyServicesLibrary.Controls.FilterPanelCheckedTree;

namespace em50;
public enum SelectChoise { All, True, False }
public delegate void IsFilterPanelClosed();

public static class Global
{
    public static string dbpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "db\\emdb.db");
    public static List<CostCenter> CostCenterSourceList= new List<CostCenter>();
    public static List<Unit> UnitSourceList = new List<Unit>();
    public static int DynamicPeriodMonthCount = 12;

}
