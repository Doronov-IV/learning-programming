using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using Streamlet.Service;


namespace Streamlet.Forms
{
    public partial class PrimaryForm : Form
    {

        string GoUpEscapeString = "[ .. ]";

        FileSystemPointer LeftWindowPointer = new FileSystemPointer();
        FileSystemPointer RightWindowPointer = new FileSystemPointer();

        ListBox ActiveListBox;


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


        private void OnAnyListBoxSelectedItemChanged(ListBox listBox, ref FileSystemPointer DirectoryPointer)
        {
            ActiveListBox = listBox;

            if (listBox.SelectedItem.ToString() != GoUpEscapeString)
            {
                MoveDown(listBox, ref DirectoryPointer);
            }
            else
            {
                MoveUp(listBox, ref DirectoryPointer);
            }
        }


        private void MoveDown(ListBox listBox, ref FileSystemPointer DirectoryPointer)
        {
            try
            {
                if (listBox.SelectedItem is DriveInfo selectedDrive)
                {
                    DirectoryPointer.NextDirectory(selectedDrive.RootDirectory);
                    GetDirectoryContents(listBox, DirectoryPointer);
                }
                else
                {
                    foreach (DirectoryInfo unit in DirectoryPointer.CurrentDirectory.GetDirectories())
                    {
                        if (unit.Name.Equals(listBox.SelectedItem))
                        {
                            DirectoryPointer.NextDirectory(unit);
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


        private void GetDirectoryContents(ListBox listBox, FileSystemPointer DirectoryPointer)
        {
            try
            {
                if (DirectoryPointer?.CurrentDirectory is not null)
                {
                    listBox.Items.Clear();
                    listBox.Items.Add(GoUpEscapeString);
                    DirectoryPointer.CurrentDirectory.GetDirectories().ToList().ForEach(unit =>
                    {
                        listBox.Items.Add(unit.Name);
                    });
                    DirectoryPointer.CurrentDirectory.GetFiles().ToList().ForEach(unit =>
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
            listBox.Items.Add(GoUpEscapeString);
            listBox.Items.Add("\n");
            listBox.Items.Add("\tI'm afraid this folder is protected by the Operating System itself.\n");
            listBox.Items.Add("\tYou may neither see the contents nor interact with the directory.\n");
            listBox.Items.Add("\n");
            listBox.Items.Add("\t\tPress  [ .. ]  to leave....");
        }


        private void MoveUp(ListBox listBox, ref FileSystemPointer DirectoryPointer)
        {
            if (DirectoryPointer != null) DirectoryPointer.NextDirectory(DirectoryPointer.CurrentDirectory.Parent);
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
                LeftAddressTextBox.Text = LeftWindowPointer?.CurrentDirectory?.FullName;
            }
        }

        private void OnRightListBoxMouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (RightListBox.SelectedItem is not null)
            {
                OnAnyListBoxSelectedItemChanged(RightListBox, ref RightWindowPointer);
                RightAddressTextBox.Text = RightWindowPointer?.CurrentDirectory?.FullName;
            }
        }

        private void LeftListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
