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
    /// <summary>
    /// File explorer form;
    /// <br />
    /// Форма-проводник;
    /// </summary>
    public partial class PrimaryForm : Form
    {


        #region PROPERTIES


        /// <summary>
        /// A pattern that represents the 'go-higher' option in the list boxes;
        /// <br />
        /// Набор символов, котоырй представляет собой переход на уровень выше в лист-боксах;
        /// </summary>
        private string GoUpEscapeString = "[ .. ]";


        /// <summary>
        /// A file-system pointer for the left list;
        /// <br />
        /// Указатель файловой системы для левого списка;
        /// </summary>
        private FileSystemPointer LeftWindowPointer = new FileSystemPointer();


        /// <summary>
        /// A file-system pointer for the right list;
        /// <br />
        /// Указатель файловой системы для правого списка;
        /// </summary>
        private FileSystemPointer RightWindowPointer = new FileSystemPointer();


        /// <summary>
        /// A reference to the focused list box;
        /// <br />
        /// Ссылка на активный лист-бокс;
        /// </summary>
        private ListBox ActiveListBox;


        #endregion PROPERTIES



        #region CONSTRUCTION


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по-умолчанию;
        /// </summary>
        public PrimaryForm()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Prepare list boxes, show a list of the disks;
        /// <br />
        /// Подготовить лист-боксы, отобразить список дисков;
        /// </summary>
        private void OnPrimaryFormLoad(object sender, EventArgs e)
        {
            GetDirectoryContents(LeftListBox, LeftWindowPointer);

            GetDirectoryContents(RightListBox, RightWindowPointer);
        }


        #endregion CONSTRUCTION



        #region Module : ListBoxes 


        #region RIGHT ONE


        //


        #endregion RIGHT ONE


        #region LEFT ONE


        //


        #endregion LEFT ONE


        #endregion Module : ListBoxes 



        #region Module : Address TextBoxes 


        #region RIGHT ONE


        //


        #endregion RIGHT ONE


        #region LEFT ONE


        //


        #endregion LEFT ONE


        #endregion Module : Address TextBoxes 



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

        private void OnLeftAddressTextBoxLeave(object sender, EventArgs e)
        {
            OnAnyAddressTextBoxLeave(LeftListBox, LeftAddressTextBox, ref LeftWindowPointer);
        }

        private void OnLeftAddressTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13) OnLeftAddressTextBoxLeave(sender, e);
        }





        private void OnRightAddressTextBoxLeave(object sender, EventArgs e)
        {
            OnAnyAddressTextBoxLeave(RightListBox, RightAddressTextBox, ref RightWindowPointer);
        }

        private void OnRightListBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13) OnRightAddressTextBoxLeave(sender, e);
        }


        private void OnAnyAddressTextBoxLeave(ListBox listBox, TextBox specificTextBox, ref FileSystemPointer specificPointer)
        {
            string sText = specificTextBox.Text;

            if (File.Exists(sText) && sText.EndsWith(".txt"))
            {
                specificPointer.NextDirectory(new FileInfo(sText).Directory);
                var a = new SecondaryForm();
                a.Show();
            }
            else if (Directory.Exists(sText))
            {
                specificPointer.NextDirectory(new DirectoryInfo(sText));
                GetDirectoryContents(listBox, specificPointer);
            }
            else
            {
                specificTextBox.Text = specificPointer.CurrentDirectory?.FullName;
            }
        }
    }
}
