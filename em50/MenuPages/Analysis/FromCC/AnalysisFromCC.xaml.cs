namespace em50.MenuPages.Analysis.FromCC;
public partial class AnalysisFromCC : UserControl
{
    private static AnalysisFromCC instance;
    public static AnalysisFromCC GetInstance()
    {
        if (instance == null)
        {
            instance = new AnalysisFromCC();
        }
        return instance;
    }

    private AnalysisFromCCViewModel viewmodel;
    private AnalysisFromCC()
    {
        viewmodel = new();
        DataContext = viewmodel;
        InitializeComponent();
    }

}