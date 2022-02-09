namespace em50.MenuPages.Analysis.FromCC
{
    public partial class MyTable : UserControl
    {
        private AnalysisFromCCViewModel model;
        public MyTable(AnalysisFromCCViewModel model)
        {
            this.model = model;
            DataContext = model;
            InitializeComponent();
        }
    }
}
