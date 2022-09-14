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
        private ListView ActiveListView;


        private List<DriveInfo> machineDriveInfo;


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
            
            machineDriveInfo = new List<DriveInfo>();
        }


        /// <summary>
        /// Prepare list boxes, show a list of the disks;
        /// <br />
        /// Подготовить лист-боксы, отобразить список дисков;
        /// </summary>
        private void OnPrimaryFormLoad(object sender, EventArgs e)
        {
            LeftListView.Groups.Clear();

            GetDirectoryContents(LeftListView, LeftWindowPointer);

            GetDirectoryContents(RightListView, RightWindowPointer);
        }


        #endregion CONSTRUCTION



        #region Module : ListViews


        private void OnAnyListViewSelectedItemChanged(ListView listView, ref FileSystemPointer DirectoryPointer)
        {
            ActiveListView = listView;

            ListViewItem escapeItem = new ListViewItem(GoUpEscapeString);

            bool bDebugFlag = false;

            foreach (var unit in listView.SelectedItems)
            {
                if (unit.ToString().Contains(escapeItem.ToString())) bDebugFlag = true;
            }

            if (bDebugFlag == false)
            {
                MoveDown(listView, ref DirectoryPointer);
            }
            else
            {
                MoveUp(listView, ref DirectoryPointer);
            }
        }


        private void MoveDown(ListView listView, ref FileSystemPointer DirectoryPointer)
        {
            try
            {
                DriveInfo selectedDrive = null;

                foreach (var item in machineDriveInfo)
                {
                    foreach (var unit in listView.SelectedItems)
                    {
                        if (unit.ToString().Contains(item.Name)) selectedDrive = item;
                    }
                }

                if (null != selectedDrive)
                {
                    DirectoryPointer.NextDirectory(selectedDrive.RootDirectory);
                    GetDirectoryContents(listView, DirectoryPointer);
                }
                else
                {
                    foreach (DirectoryInfo unit in DirectoryPointer.CurrentDirectory.GetDirectories())
                    {
                        foreach (var item in listView.SelectedItems)
                        {
                            if (item.ToString().Contains(unit.Name))
                            {
                                DirectoryPointer.NextDirectory(unit);
                                GetDirectoryContents(listView, DirectoryPointer);
                                break;
                            }
                        }
                    }
                }
            }
            catch (System.UnauthorizedAccessException ex)
            {
                ShowFailMessage(listView);
            }
        }


        private void GetDirectoryContents(ListView listView, FileSystemPointer DirectoryPointer)
        {
            try
            {
                if (DirectoryPointer?.CurrentDirectory != null)
                {
                    listView.Items.Clear();
                    listView.Items.Add(new ListViewItem(GoUpEscapeString));
                    DirectoryPointer.CurrentDirectory.GetDirectories().ToList().ForEach(unit =>
                    {
                        ListViewItem item = new ListViewItem(unit.Name);
                        item.SubItems.Add(unit.Extension);
                        item.SubItems.Add("");
                        item.SubItems.Add(unit.LastWriteTime.ToShortDateString());
                        listView.Items.Add(item);
                    });
                    DirectoryPointer.CurrentDirectory.GetFiles().ToList().ForEach(unit =>
                    {
                        ListViewItem item = new ListViewItem(unit.Name);
                        item.SubItems.Add(unit.Extension);
                        item.SubItems.Add(unit.Length.ToString());
                        item.SubItems.Add(unit.LastWriteTime.ToShortDateString());
                        listView.Items.Add(item);
                    });
                }
                else GetDrives(listView);
            }
            catch (System.UnauthorizedAccessException ex)
            {
                ShowFailMessage(listView);
            }
        }


        private void ShowFailMessage(ListView listView)
        {
            listView.Items.Clear();
            listView.Items.Add(GoUpEscapeString);
            listView.Items.Add("\n");
            listView.Items.Add("\tAccess denied.\n");
            listView.Items.Add("\n");
            listView.Items.Add("\t\t[ .. ]  to leave....");
        }


        private void MoveUp(ListView ListView, ref FileSystemPointer DirectoryPointer)
        {
            if (DirectoryPointer != null) DirectoryPointer.NextDirectory(DirectoryPointer.CurrentDirectory.Parent);
            GetDirectoryContents(ListView, DirectoryPointer);
        }


        private void GetDrives(ListView listView)
        {
            var driveList = DriveInfo.GetDrives();

            listView.Items.Clear();

            if (machineDriveInfo.Count == 0) machineDriveInfo.AddRange(driveList);

            foreach (var item in driveList)
            {
                ListViewItem LVItem = new ListViewItem(item.Name);
                LVItem.SubItems.Add("");
                LVItem.SubItems.Add("");
                LVItem.SubItems.Add("");

                listView.Items.Add(LVItem);
            }
        }

        private void MiddleToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void OnLeftListViewMouseDoubleClick(object sender, EventArgs e)
        {
            if (LeftListView.SelectedItems != null)
            {
                OnAnyListViewSelectedItemChanged(LeftListView, ref LeftWindowPointer);
                LeftAddressTextBox.Text = LeftWindowPointer?.CurrentDirectory?.FullName;
            }
        }

        private void OnRightListViewMouseDoubleClick(object sender, EventArgs e)
        {
            if (RightListView.SelectedItems != null)
            {
                OnAnyListViewSelectedItemChanged(RightListView, ref RightWindowPointer);
                RightAddressTextBox.Text = RightWindowPointer?.CurrentDirectory?.FullName;
            }
        }


        private void OnLeftListViewSelectedValueChanged(object sender, EventArgs e)
        {
            OnAnyListViewSelectedItemChanged(LeftListView, ref LeftWindowPointer);
        }


        private void OnRighttListViewSelectedValueChanged(object sender, EventArgs e)
        {
            OnAnyListViewSelectedItemChanged(RightListView, ref RightWindowPointer);
        }


        #endregion Module : ListViews



        #region Module : Address TextBoxes 


        #region SPECIFIC_METHODS


        /// <summary>
        /// When left address box gets inactive;
        /// <br />
        /// Когда уйдёт фокус с левой адресной строки;
        /// </summary>
        private void OnLeftAddressTextBoxLeave(object sender, EventArgs e)
        {
            OnAnyAddressTextBoxLeave(LeftListView, LeftAddressTextBox, ref LeftWindowPointer);
        }

        /// <summary>
        /// When right address box gets inactive;
        /// <br />
        /// Когда уйдёт фокус с правой адресной строки;
        /// </summary>
        private void OnRightAddressTextBoxLeave(object sender, EventArgs e)
        {
            OnAnyAddressTextBoxLeave(RightListView, RightAddressTextBox, ref RightWindowPointer);
        }



        /// <summary>
        /// When left address box gets some key pressed;
        /// <br />
        /// Когда в левой строке нажата клавиша;
        /// </summary>
        private void OnLeftAddressTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            OnAnyListBoxKeyDown(LeftListView, LeftAddressTextBox, ref LeftWindowPointer, e);
        }

        /// <summary>
        /// When right address box gets some key pressed;
        /// <br />
        /// Когда в правой строке нажата клавиша;
        /// </summary>
        private void OnRightAddressTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            OnAnyListBoxKeyDown(RightListView, RightAddressTextBox, ref RightWindowPointer, e);
        }


            #endregion SPECIFIC_METHODS



        #region GENERIC_METHODS



        /// <summary>
        /// When any address box gets some key pressed;
        /// <br />
        /// Когда в любой строке нажата клавиша;
        /// </summary>
        /// <param name="listView">The exact listbox;<br/>Конкретный листбокс;</param>
        /// <param name="specificTextBox">The very address box;<br/>Конкретный адрес бокс;</param>
        /// <param name="ptr">Respective custom file pointer;<br/>Соответствующий указатель файловой системы;</param>
        /// <param name="e">Key pressed;<br/>Нажатая клавиша;</param>
        private void OnAnyListBoxKeyDown(ListView listView, TextBox specificTextBox, ref FileSystemPointer ptr, KeyEventArgs e)
        {
            // 'enter';
            if (e.KeyCode == Keys.Enter) OnAnyAddressTextBoxLeave(listView, specificTextBox, ref ptr);
            // 'esc';
            else if (e.KeyCode == Keys.Escape)
            {
                specificTextBox.Text = "aaa";
                OnAnyAddressTextBoxLeave(listView, specificTextBox, ref ptr);
            }
        }

        /// <summary>
        /// When any address box gets inactive;
        /// <br />
        /// Когда уйдёт фокус с любой адресной строки;
        /// </summary>
        /// <param name="listBox">The exact listbox;<br/>Конкретный листбокс;</param>
        /// <param name="specificTextBox">The very address box;<br/>Конкретный адрес бокс;</param>
        /// <param name="ptr">Respective custom file pointer;<br/>Соответствующий указатель файловой системы;</param>
        private void OnAnyAddressTextBoxLeave(ListView listView, TextBox specificTextBox, ref FileSystemPointer specificPointer)
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
                GetDirectoryContents(listView, specificPointer);
            }
            else
            {
                specificTextBox.Text = specificPointer.CurrentDirectory?.FullName;
            }
        }


        #endregion GENERIC_METHODS

        #endregion Module : Address TextBoxes 

        private void OnOpenToolClick(object sender, EventArgs e)
        {
            if (ActiveListView?.SelectedItems != null)
            {
                if (ActiveListView == LeftListView) OnLeftListViewMouseDoubleClick(sender, e);
                else OnRightListViewMouseDoubleClick(sender, e);
            }
            
        }
    }
}
