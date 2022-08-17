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
            if (listBox.SelectedItem.ToString() != "/..")
            {
                MoveDown(listBox, ref DirectoryPointer);
            }
            else
            {
                MoveUp(listBox, ref DirectoryPointer);
            }
        }


        private void MoveDown(ListBox listBox, ref DirectoryInfo DirectoryPointer)
        {
            if (listBox.SelectedItem is DriveInfo selectedDrive)
            {
                DirectoryPointer = selectedDrive.RootDirectory;
                GetDirectoryContents(listBox, DirectoryPointer);
            }
            else
            {
                foreach (DirectoryInfo unit in DirectoryPointer.GetDirectories())
                {
                    if (unit.Name.Equals(listBox.SelectedItem))
                    {
                        DirectoryPointer = unit;
                        GetDirectoryContents(listBox, DirectoryPointer);
                        break;
                    }
                }
            }
        }


        private void GetDirectoryContents(ListBox listBox, DirectoryInfo DirectoryPointer)
        {
            if (DirectoryPointer is not null)
            {
                listBox.Items.Clear();
                listBox.Items.Add("/..");
                DirectoryPointer.GetDirectories().ToList().ForEach(unit =>
                {
                    listBox.Items.Add(unit.Name);
                });
                DirectoryPointer.GetFiles().ToList().ForEach(unit =>
                {
                    listBox.Items.Add(unit.Name);
                });
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

        private void OnLeftListBoxMouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (LeftListBox.SelectedItem is not null) OnAnyListBoxSelectedItemChanged(LeftListBox, ref LeftWindowPointer);
        }

        private void OnRightListBoxMouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (RightListBox.SelectedItem is not null) OnAnyListBoxSelectedItemChanged(RightListBox, ref RightWindowPointer);
        }
    }
}
