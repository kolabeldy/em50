namespace em50.MenuPages.Analysis.FromCC
{
    public partial class MyDetailTable : UserControl
    {
        public MyDetailTable(AnalysisFromCCViewModel model)
        {
            DataContext = model;
            InitializeComponent();
        }
    }
}
