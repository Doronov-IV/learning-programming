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
            try
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
            catch (System.UnauthorizedAccessException ex)
            {
                ShowFailMessage(listBox);
            }
        }


        private void GetDirectoryContents(ListBox listBox, DirectoryInfo DirectoryPointer)
        {
            try
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
            catch (System.UnauthorizedAccessException ex)
            {
                ShowFailMessage(listBox);
            }
        }


        private void ShowFailMessage(ListBox listBox)
        {
            listBox.Items.Clear();
            listBox.Items.Add("/..");
            listBox.Items.Add("\n");
            listBox.Items.Add("\tI'm afraid this folder is protected by the Operating System itself.\n");
            listBox.Items.Add("\tYou may neither see the contents nor interact with the directory.\n");
            listBox.Items.Add("\n");
            listBox.Items.Add("\t\tPress Backspace to leave....");
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
            if (LeftListBox.SelectedItem is not null)
            {
                OnAnyListBoxSelectedItemChanged(LeftListBox, ref LeftWindowPointer);
                LeftAddressTextBox.Text = LeftWindowPointer?.FullName;
            }
        }

        private void OnRightListBoxMouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (RightListBox.SelectedItem is not null)
            {
                OnAnyListBoxSelectedItemChanged(RightListBox, ref RightWindowPointer);
                RightAddressTextBox.Text = RightWindowPointer?.FullName;
            }
        }

        private void LeftListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
