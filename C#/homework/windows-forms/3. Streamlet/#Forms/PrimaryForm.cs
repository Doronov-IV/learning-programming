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
            GetDirectoryContents(LeftListBox, LeftWindowPointer);

            GetDirectoryContents(RightListBox, RightWindowPointer);
        }

        private void OnLeftListBoxSelectedValueChanged(object sender, EventArgs e)
        {
            OnAnyListBoxSelectedItemChanged(LeftListBox, ref LeftWindowPointer);
        }


        private void OnRighttListBoxSelectedValueChanged(object sender, EventArgs e)
        {
            OnAnyListBoxSelectedItemChanged(RightListBox, ref RightWindowPointer);
        }


        private void OnAnyListBoxSelectedItemChanged(ListBox listBox, ref DirectoryInfo DirectoryPointer)
        {
            if (listBox.SelectedItem is DriveInfo selectedDrive)
            {
                DirectoryPointer = selectedDrive.RootDirectory;
                GetDirectoryContents(listBox, DirectoryPointer);
            }
            else if (listBox.SelectedItem is DirectoryInfo selectedDir)
            {
                DirectoryPointer = selectedDir;
                GetDirectoryContents(listBox, DirectoryPointer);
            }
            else
            {
                MoveUp(listBox, ref DirectoryPointer);
            }
        }


        private void GetDirectoryContents(ListBox listBox, DirectoryInfo DirectoryPointer)
        {
            if (DirectoryPointer is not null)
            {
                listBox.Items.Clear();
                listBox.Items.Add("/..");
                listBox.Items.AddRange(DirectoryPointer.GetDirectories());
                listBox.Items.AddRange(DirectoryPointer.GetFiles());
            }
            else GetDrives(listBox);
        }


        private void MoveUp(ListBox listBox, ref DirectoryInfo DirectoryPointer)
        {
            if (DirectoryPointer is not null) DirectoryPointer = DirectoryPointer.Parent;
            GetDirectoryContents(listBox, DirectoryPointer);
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
