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

        DirectoryInfo LeftWindowPointer;
        DirectoryInfo RightWindowPointer;


        public PrimaryForm()
        {
            InitializeComponent();
        }

        private void OnPrimaryFormLoad(object sender, EventArgs e)
        {

            var driverList = DriveInfo.GetDrives();

            LeftListBox.Items.AddRange(driverList);
            RightListBox.Items.AddRange(driverList);
        }

        private void OnLeftListBoxSelectedValueChanged(object sender, EventArgs e)
        {
            if (LeftListBox.SelectedItem is DriveInfo selectedDrive)
            {
                LeftWindowPointer = selectedDrive.RootDirectory;
                GetDirectoryContents(LeftListBox, LeftWindowPointer);
            }
            else if (LeftListBox.SelectedItem is DirectoryInfo selectedDir)
            {
                LeftWindowPointer = selectedDir;
                GetDirectoryContents(LeftListBox, LeftWindowPointer);
            }
            else
            {
                if (LeftWindowPointer != null) LeftWindowPointer = LeftWindowPointer.Parent;
                GetDirectoryContents(LeftListBox, LeftWindowPointer);
            }
            
        }

        private void GetDirectoryContents(ListBox listBox, DirectoryInfo DirectoryPointer)
        {
            if (DirectoryPointer is not null)
            {
                listBox.Items.Clear();
                listBox.Items.Add("/..");
                listBox.Items.AddRange(LeftWindowPointer.GetDirectories());
                listBox.Items.AddRange(LeftWindowPointer.GetFiles());
            }
            else GetDrives(listBox);
        }

        private void GetDrives(ListBox listBox)
        {
            var driverList = DriveInfo.GetDrives();

            listBox.Items.Clear();

            listBox.Items.AddRange(driverList);
        }

        private void MiddleToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void LeftListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
