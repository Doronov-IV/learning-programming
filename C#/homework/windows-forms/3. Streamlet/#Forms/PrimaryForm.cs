using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Streamlet.Forms
{
    public partial class PrimaryForm : Form
    {
        public PrimaryForm()
        {
            InitializeComponent();
        }

        private void OnPrimaryFormLoad(object sender, EventArgs e)
        {
            LeftListView.View = View.List;

            var driverList = DriveInfo.GetDrives();

            ListViewItem[] itemList = new ListViewItem[driverList.Count()];

            ListViewItem item = default(ListViewItem);

            for (int i = 0, iSize = driverList.Length; i < iSize; ++i)
            {
                itemList[i] = new ListViewItem(driverList[i].Name);
            }

            LeftListView.Items.AddRange(itemList);
        }

        private void MiddleToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void LeftListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
