namespace em50.MenuPages.Analysis.FromCC
{
    public partial class MyCausesTable : UserControl
    {
        public MyCausesTable(AnalysisFromCCViewModel model)
        {
            DataContext = model;
            InitializeComponent();
        }
    }
}
