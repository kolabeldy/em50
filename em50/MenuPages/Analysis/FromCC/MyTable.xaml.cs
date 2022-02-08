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


        private void dataGridCC_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            model.SelectedRow = (TableData)dataGridCC.SelectedItem;
        }
    }
}
