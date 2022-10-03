using Doronov.ConcurrencyExam.Service;

namespace Doronov.ConcurrencyExam.Forms
{
    public partial class StatisticsForm : Form
    {


        #region PROPERTIES


        public List<FileStatistics> TotalStatistics { get; set; }

        public List<string> TopForbiddenWords { get; set; }


        #endregion PROPERTIES




        public StatisticsForm()
        {
            InitializeComponent();
        }




        private void OnCloseButtonClick(object sender, EventArgs e)
        {
            this.Close();
        }


        private void CompileStatistics()
        {
            List<FileInfo> fileList = new List<FileInfo>();

            ListViewItem listViewItem = new ListViewItem();

            TotalStatistics.ForEach(fileStatistics =>
            {
                listViewItem = new ListViewItem(fileStatistics.FileInfo.Name);
                listViewItem.SubItems.Add(fileStatistics.Counter.ToString());
                listViewItem.SubItems.Add(fileStatistics.FileInfo.Length.ToString());
                listViewItem.SubItems.Add(fileStatistics.FileInfo.FullName);
                FileListView.Items.Add(listViewItem);
            });

            TopWordsListBox.DataSource = TopForbiddenWords;
        }

        private void OnStatisticsFormLoad(object sender, EventArgs e)
        {
            if (TotalStatistics != null) CompileStatistics();
        }
    }
}
