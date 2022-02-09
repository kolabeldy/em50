namespace em50.MenuPages.Analysis.FromCC;
public class AnalysisFromCCViewModel : BaseViewModel
{
    MainWindow mainwin = Application.Current.MainWindow as MainWindow;
    private bool isFirstLoad = true;

    private string _Caption = "Анализ по центрам затрат";
    public string Caption
    {
        get => _Caption;
        set
        {
            Set(ref _Caption, value);
        }
    }



    private MyTable _MyTable;
    public MyTable MyTable
    {
        get => _MyTable;
        set
        {
            Set(ref _MyTable, value);
        }
    }
    private MyDetailTable _MyDetailTable;
    public MyDetailTable MyDetailTable
    {
        get => _MyDetailTable;
        set
        {
            Set(ref _MyDetailTable, value);
        }
    }

    // Объявление свойства - FilterPanel
    public FilterPanel FilterPanel { get; set; }

    // Набор коллекций - фильтров
    private List<IdName> periodsFilterList = new();
    private List<IdName> ccFilterList = new();
    private List<IdName> erFilterList = new();

    public List<DataUse> DataTableUse;

    private ObservableCollection<TableData> _TableData;
    public ObservableCollection<TableData> TableData
    {
        get => _TableData;
        set
        {
            Set(ref _TableData, value);
        }
    }

    private TableData _SelectedRow;
    public TableData SelectedRow
    {
        get => _SelectedRow;
        set
        {
            Set(ref _SelectedRow, value);
            if (!isFirstLoad)
                SetDetailData(_SelectedRow);
        }
    }

    private ObservableCollection<DataUse> _DetailTableData;
    public ObservableCollection<DataUse> DetailTableData
    {
        get => _DetailTableData;
        set
        {
            Set(ref _DetailTableData, value);
        }
    }

    private string _DetailTableCaption = "Распределение ресурса по продуктам";
    public string DetailTableCaption
    {
        get => _DetailTableCaption;
        set
        {
            Set(ref _DetailTableCaption, value);
        }
    }

    public AnalysisFromCCViewModel()
    {
        periodsTree = PeriodTree();
        ccTree = CCTree();
        erTree = ERTree();

        List<TreeFilterCollection> treeFilterCollections = new();
        treeFilterCollections.Add(new TreeFilterCollection { FilterCollection = periodsTree, Title = "Период:", InitType = TreeInitType.Last });
        treeFilterCollections.Add(new TreeFilterCollection { FilterCollection = ccTree, Title = "Центры затрат:", InitType = TreeInitType.All });
        treeFilterCollections.Add(new TreeFilterCollection { FilterCollection = erTree, Title = "Энергоресурсы:", InitType = TreeInitType.All });
        FilterPanel = new FilterPanel(treeFilterCollections);

        MyTable = new MyTable(this);
        MyDetailTable = new MyDetailTable(this);

        FilterPanel.ViewModel.onChange += FiltersOnChangeHandler;

        Refresh();
        isFirstLoad = false;
    }

    private RelayCommand _Back_Command;
    public RelayCommand Back_Command
    {
        get
        {
            return _Back_Command ??
                (_Back_Command = new RelayCommand(obj =>
                {
                    //mainwin.model.MenuPanelMaxWidth = 2000;
                    //mainwin.model.ContentPanel.Content = null;
                }));
        }
    }


    #region Подготовка коллекция для передачи в модуль Фильтр

    // Набор коллекций в виде деревьев для инициации FilterPanel
    private List<TreeNode> periodsTree = new List<TreeNode>();
    private List<TreeNode> ccTree = new List<TreeNode>();
    private List<TreeNode> erTree = new List<TreeNode>();

    private List<TreeNode> PeriodTree()
    {
        List<TreeNode> result = new();
        foreach (int r in Period.Years)
        {
            result.Add(new TreeNode()
            {
                Name = r.ToString(),
                TreeNodeItems = ConvertToTreeItemList(Period.YearPeriods(r))
            });
        }
        return result;
    }
    private List<TreeNode> CCTree()
    {
        CostCenter cc = new CostCenter();
        List<TreeNode> result = new()
        {
            new TreeNode()
            {
                Name = "Основные",
                TreeNodeItems = ConvertToTreeItemList(CostCenter.TechMainList)
            },
            new TreeNode()
            {
                Name = "Прочие технологические",
                TreeNodeItems = ConvertToTreeItemList(CostCenter.TechOtherList)
            },
            new TreeNode()
            {
                Name = "Вспомогательные",
                TreeNodeItems = ConvertToTreeItemList(CostCenter.SlaveList)
            }
        };
        return result;
    }
    private List<TreeNode> ERTree()
    {
        EnergyResource er = new EnergyResource();

        List<TreeNode> result = new()
        {
            new TreeNode()
            {
                Name = "Первичные",
                TreeNodeItems = ConvertToTreeItemList(EnergyResource.PrimeList)
            },
            new TreeNode()
            {
                Name = "Вторичные",
                TreeNodeItems = ConvertToTreeItemList(EnergyResource.SecondaryList)
            }
        };
        return result;
    }
    private List<TreeItem> ConvertToTreeItemList<T>(List<T> tList)
    {
        List<IdName> ids = new List<IdName>((IEnumerable<IdName>)tList);
        List<TreeItem> result = new List<TreeItem>();
        foreach (IdName r in ids)
        {
            TreeItem n = new TreeItem();
            n.Id = r.Id;
            n.Name = r.Name;
            result.Add(n);
        }
        return result;
    }

    #endregion

    private void FiltersOnChangeHandler() // Получение итоговых наборов фильтров по Period, CC, ER
    {
        Refresh();
    }

    #region Установка набора фильтров и запись в базу данных

    protected List<IdName> GetItemFilters(List<TreeNode> treeSource)
    {
        List<IdName> result = new();
        foreach (TreeNode family in treeSource)
            foreach (TreeItem person in family.TreeNodeItems)
                if (ItemHelper.GetIsChecked(person) == true)
                {
                    result.Add(new IdName() { Id = person.Id, Name = person.Name });
                }
        return result;
    }

    private List<FilterTable> GetFilters()
    {
        List<FilterTable> result = new();
        foreach (var r in periodsFilterList)
        {
            result.Add(new FilterTable { Category = "Analysis", Item = "AnalysisFromCC", Indicator = "Period", Value = r.Id });
        }
        foreach (var r in ccFilterList)
        {
            result.Add(new FilterTable { Category = "Analysis", Item = "AnalysisFromCC", Indicator = "CC", Value = r.Id });
        }
        foreach (var r in erFilterList)
        {
            result.Add(new FilterTable { Category = "Analysis", Item = "AnalysisFromCC", Indicator = "ER", Value = r.Id });
        }

        return result;
    }

    private void Refresh()
    {
        periodsFilterList = GetItemFilters(periodsTree);
        ccFilterList = GetItemFilters(ccTree);
        erFilterList = GetItemFilters(erTree);
        List<FilterTable> filtersTable = GetFilters();
        FilterTable.Delete("Analysis", "AnalysisFromCC");
        FilterTable.AddRange(filtersTable);
        DataTableUse = DataUse.Get();
        TableData = new ObservableCollection<TableData>(Models.TableData.Get(DataTableUse));
        //DetailTableData = new ObservableCollection<DataUse>(DataTableUse);
    }

    private void SetDetailData(TableData row)
    {
        int period = row.Period;
        int idcc = row.IdCC;
        int ider = row.IdER;
        string ername = row.ERName;
        DetailTableData = new ObservableCollection<DataUse>(from o in DataTableUse
                                                            where o.Period == period && o.IdCC == idcc && o.IdER == ider
                                                            select new DataUse()
                                                            {
                                                                IdProduct = o.IdProduct,
                                                                ProductName = o.ProductName,
                                                                UnitName = o.UnitName,
                                                                Fact = o.Fact,
                                                                Plan = o.Plan,
                                                                Diff = o.Diff,
                                                                FactCost = o.FactCost,
                                                                PlanCost = o.PlanCost,
                                                                DiffCost = o.DiffCost,
                                                                DiffProc = Math.Round(o.Diff, 2) * 100 / Math.Round(o.Plan, 2)
                                                            });
        DetailTableCaption = "Распределение ресурса (" + ider.ToString() + "_" + ername + ") по продуктам";
    }

    #endregion



}