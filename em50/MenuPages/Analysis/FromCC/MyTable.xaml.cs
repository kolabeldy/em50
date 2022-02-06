namespace em50.MenuPages.Analysis.FromCC
{
    public partial class MyTable : UserControl
    {
        public MyTable(AnalysisFromCCViewModel model)
        {
            DataContext = model;
            InitializeComponent();
        }
    }
}
