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
            LeftListBox.Items.Add(new StreamletListObject("DEFAULT"));

            RightListBox.Items.Add(new StreamletListObject("DEFAULT"));

            GetDirectoryContents(LeftListBox, LeftWindowPointer);

            GetDirectoryContents(RightListBox, RightWindowPointer);
        }


        #endregion CONSTRUCTION



        #region Module : ListBoxes 


        private void OnAnyListBoxSelectedItemChanged(ListBox listBox, ref FileSystemPointer DirectoryPointer)
        {
            ActiveListBox = listBox;

            if (!listBox.SelectedItem.ToString().Contains(GoUpEscapeString))
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
                DriveInfo selectedDrive = null;

                foreach (var item in machineDriveInfo)
                {
                    if (listBox.SelectedItem.ToString().Contains(item.Name))
                    {
                        selectedDrive = item;
                    }
                }

                if (null != selectedDrive)
                {
                    DirectoryPointer.NextDirectory(selectedDrive.RootDirectory);
                    GetDirectoryContents(listBox, DirectoryPointer);
                }
                else
                {
                    foreach (DirectoryInfo unit in DirectoryPointer.CurrentDirectory.GetDirectories())
                    {
                        if (listBox.SelectedItem.ToString().Contains(unit.Name))
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
                    ClearListBoxItems(listBox);
                    listBox.Items.Add(new StreamletListObject(GoUpEscapeString));
                    DirectoryPointer.CurrentDirectory.GetDirectories().ToList().ForEach(unit =>
                    {
                        listBox.Items.Add(new StreamletListObject(unit));
                    });
                    DirectoryPointer.CurrentDirectory.GetFiles().ToList().ForEach(unit =>
                    {
                        listBox.Items.Add(new StreamletListObject(unit));
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
            var driveList = DriveInfo.GetDrives();

            listBox.Items.Clear();

            if (machineDriveInfo.Count == 0) machineDriveInfo.AddRange(driveList);

            foreach (var item in driveList)
            {
                listBox.Items.Add(new StreamletListObject(item.Name));
            }
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


        private void OnLeftListBoxSelectedValueChanged(object sender, EventArgs e)
        {
            OnAnyListBoxSelectedItemChanged(LeftListBox, ref LeftWindowPointer);
        }


        private void OnRighttListBoxSelectedValueChanged(object sender, EventArgs e)
        {
            OnAnyListBoxSelectedItemChanged(RightListBox, ref RightWindowPointer);
        }


        /// <summary>
        /// Clear all list items except for the header one;
        /// <br />
        /// Очистить список кроме заголовка;
        /// </summary>
        /// <param name="listBox">Specific listbox;<br \>Конкретный лист бокс;</param>
        private void ClearListBoxItems(ListBox listBox)
        {
            listBox.Items.Clear();
            listBox.Items.Add(new StreamletListObject("DEFAULT"));
        }


        #endregion Module : ListBoxes 



        #region Module : Address TextBoxes 


            #region SPECIFIC_METHODS


        /// <summary>
        /// When left address box gets inactive;
        /// <br />
        /// Когда уйдёт фокус с левой адресной строки;
        /// </summary>
        private void OnLeftAddressTextBoxLeave(object sender, EventArgs e)
        {
            OnAnyAddressTextBoxLeave(LeftListBox, LeftAddressTextBox, ref LeftWindowPointer);
        }

        /// <summary>
        /// When right address box gets inactive;
        /// <br />
        /// Когда уйдёт фокус с правой адресной строки;
        /// </summary>
        private void OnRightAddressTextBoxLeave(object sender, EventArgs e)
        {
            OnAnyAddressTextBoxLeave(RightListBox, RightAddressTextBox, ref RightWindowPointer);
        }



        /// <summary>
        /// When left address box gets some key pressed;
        /// <br />
        /// Когда в левой строке нажата клавиша;
        /// </summary>
        private void OnLeftAddressTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            OnAnyListBoxKeyDown(LeftListBox, LeftAddressTextBox, ref LeftWindowPointer, e);
        }

        /// <summary>
        /// When right address box gets some key pressed;
        /// <br />
        /// Когда в правой строке нажата клавиша;
        /// </summary>
        private void OnRightAddressTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            OnAnyListBoxKeyDown(RightListBox, RightAddressTextBox, ref RightWindowPointer, e);
        }


            #endregion SPECIFIC_METHODS



            #region GENERIC_METHODS



        /// <summary>
        /// When any address box gets some key pressed;
        /// <br />
        /// Когда в любой строке нажата клавиша;
        /// </summary>
        /// <param name="listBox">The exact listbox;<br/>Конкретный листбокс;</param>
        /// <param name="specificTextBox">The very address box;<br/>Конкретный адрес бокс;</param>
        /// <param name="ptr">Respective custom file pointer;<br/>Соответствующий указатель файловой системы;</param>
        /// <param name="e">Key pressed;<br/>Нажатая клавиша;</param>
        private void OnAnyListBoxKeyDown(ListBox listBox, TextBox specificTextBox, ref FileSystemPointer ptr, KeyEventArgs e)
        {
            // 'enter';
            if (e.KeyCode == Keys.Enter) OnAnyAddressTextBoxLeave(listBox, specificTextBox, ref ptr);
            // 'esc';
            else if (e.KeyCode == Keys.Escape)
            {
                specificTextBox.Text = "aaa";
                OnAnyAddressTextBoxLeave(listBox, specificTextBox, ref ptr);
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


            #endregion GENERIC_METHODS


        #endregion Module : Address TextBoxes 
    }
}
