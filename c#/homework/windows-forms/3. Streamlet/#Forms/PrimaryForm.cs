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


        // Comments
        /*
         
        

        1.  Вместо 'ListBox' используется 'ListView', так красивее и гораздо удобнее;

        2.  'Listview', 'AddressTextBox' и 'FileSystemPointer' можно было связать одним классом, но я понял это слишком поздно;

        3.  Неправильный ввод в адресную строку не вызывает ошибки, вместо этого он просто стирает неправильный ввод.
           Можно было бы сделать ошибку без труда, просто текущий вариант чуть больше похож на настоящий проводник;

        4.  Если долго ковыряться в коде, можно найти лишние 'ListView'. Это потому что файлы несколько раз
           удалялись самим windows forms из-за моих ошибок при обращении с генерируемыми методами.


        */





        #region Module : ListViews



        #region Specific Handlers - Pairs of handlers one for each side


        /// <summary>
        /// Left listview click event handler;
        /// <br />
        /// Хендлер клика левого списка;
        /// </summary>
        private void OnLeftListViewMouseDoubleClick(object sender, EventArgs e)
        {
            if (LeftListView.SelectedItems != null)
            {
                OnAnyListViewSelectedItemChanged(LeftListView, ref LeftWindowPointer);
                LeftAddressTextBox.Text = LeftWindowPointer?.CurrentDirectory?.FullName;
            }
        }


        /// <summary>
        /// Right listview click event handler;
        /// <br />
        /// Хендлер клика правого списка;
        /// </summary>
        private void OnRightListViewMouseDoubleClick(object sender, EventArgs e)
        {
            if (RightListView.SelectedItems != null)
            {
                OnAnyListViewSelectedItemChanged(RightListView, ref RightWindowPointer);
                RightAddressTextBox.Text = RightWindowPointer?.CurrentDirectory?.FullName;
            }
        }


        /// <summary>
        /// Left listview selected value change handler;
        /// <br />
        /// Хендлер изменения выбранного значения левого списка;
        /// </summary>
        private void OnLeftListViewSelectedValueChanged(object sender, EventArgs e)
        {
            OnAnyListViewSelectedItemChanged(LeftListView, ref LeftWindowPointer);
        }


        /// <summary>
        /// Right listview selected value change handler;
        /// <br />
        /// Хендлер изменения выбранного значения правого списка;
        /// </summary>
        private void OnRighttListViewSelectedValueChanged(object sender, EventArgs e)
        {
            OnAnyListViewSelectedItemChanged(RightListView, ref RightWindowPointer);
        }


        #endregion Specific Handlers - Pairs of handlers one for each side



        #region Generic Handlers - Generic methods that are dispatched by specific ones


        /// <summary>
        /// Any listview selected value change handler;
        /// <br />
        /// Хендлер изменения выбранного значения любого списка;
        /// </summary>
        /// <param name="listView">Listview in which to display;<br />Listview в котором отобразить;</param>
        /// <param name="DirectoryPointer">Respective directory pointer;<br />Соответствующий указатель файловой системы;</param>
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



        #endregion Generic Handlers - Generic methods that are dispatched by specific ones



        #region Common non-handler Logic


        /// <summary>
        /// Get down the level in a local file system;
        /// <br />
        /// Перейти на уровень вниз по файловой системе;
        /// </summary>
        /// <param name="listView">Listview in which to display;<br />Listview в котором отобразить;</param>
        /// <param name="DirectoryPointer">Respective directory pointer;<br />Соответствующий указатель файловой системы;</param>
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
                    ShowDirectoryContents(listView, DirectoryPointer);
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
                                ShowDirectoryContents(listView, DirectoryPointer);
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


        /// <summary>
        /// Get up the level in a local file system;
        /// <br />
        /// Перейти на уровень вверх по файловой системе;
        /// </summary>
        /// <param name="listView">Listview in which to display;<br />Listview в котором отобразить;</param>
        /// <param name="DirectoryPointer">Respective directory pointer;<br />Соответствующий указатель файловой системы;</param>
        private void MoveUp(ListView ListView, ref FileSystemPointer DirectoryPointer)
        {
            if (DirectoryPointer != null) DirectoryPointer.NextDirectory(DirectoryPointer.CurrentDirectory.Parent);
            ShowDirectoryContents(ListView, DirectoryPointer);
        }


        /// <summary>
        /// Show the list of directory contents in a specific listview;
        /// <br />
        /// Отобразить содержимое папки в конкретном listview;
        /// </summary>
        /// <param name="listView">Listview in which to display;<br />Listview в котором отобразить;</param>
        /// <param name="DirectoryPointer">Respective directory pointer;<br />Соответствующий указатель файловой системы;</param>
        private void ShowDirectoryContents(ListView listView, FileSystemPointer DirectoryPointer)
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
                else ShowDrives(listView);
            }
            catch (System.UnauthorizedAccessException ex)
            {
                ShowFailMessage(listView);
            }
        }


        /// <summary>
        /// Show the list of drives in a specific listview;
        /// <br />
        /// Отобразить список дисков в конкретном listview;
        /// </summary>
        /// <param name="listView">Listview in which to display;<br />Listview в котором отобразить;</param>
        private void ShowDrives(ListView listView)
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


        /// <summary>
        /// Show fail message in case the directory is protected by the OS;
        /// <br />
        /// Отобразить сообщение об ошибке, если директория защищена ОС;
        /// </summary>
        /// <param name="listView">Listview in whick to display;<br />Listview в котором отобразить;</param>
        private void ShowFailMessage(ListView listView)
        {
            listView.Items.Clear();
            listView.Items.Add(GoUpEscapeString);
            listView.Items.Add("\n");
            listView.Items.Add("\tAccess denied.\n");
            listView.Items.Add("\n");
            listView.Items.Add("\t\t[ .. ]  to leave....");
        }



        #endregion Common non-handler Logic



        #endregion Module : ListViews



        #region Module : Address TextBoxes 



        #region Specific Handlers (see ListViews region)


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


        #endregion Specific Handlers (see ListViews region)



        #region Generic Handlers (see ListViews region)



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
                ShowDirectoryContents(listView, specificPointer);
            }
            else
            {
                specificTextBox.Text = specificPointer.CurrentDirectory?.FullName;
            }
        }


        #endregion Generic Handlers (see ListViews region)



        #endregion Module : Address TextBoxes 



        #region Module : Icon ToolStrip


        /// <summary>
        /// The 'open' button click handler;
        /// <br />
        /// Обработчик клика по кнопке "открыть";
        /// </summary>
        private void OnOpenToolClick(object sender, EventArgs e)
        {
            if (ActiveListView?.SelectedItems != null)
            {
                if (ActiveListView == LeftListView) OnLeftListViewMouseDoubleClick(sender, e);
                else OnRightListViewMouseDoubleClick(sender, e);
            }
            
        }


        /// <summary>
        /// 'Copy Path' tool click handler;
        /// <br />
        /// Хендлер клика кнопки "Скопировать Путь";
        /// </summary>
        private void OnCopyPathToolClick(object sender, EventArgs e)
        {
            if (ActiveListView?.SelectedItems != null)
            {
                string CopyToClipboardString = "Error. Debug message.";

                if (ActiveListView == LeftListView) 
                    CopyToClipboardString = GetItemPathForAnyActiveListView(LeftListView, LeftWindowPointer);

                else
                    CopyToClipboardString = GetItemPathForAnyActiveListView(RightListView, RightWindowPointer);


                System.Windows.Forms.Clipboard.SetText(CopyToClipboardString);
            }
        }








        /// <summary>
        /// Get the path of the first selected item;
        /// <br />
        /// Получить путь первого выделенного файла;
        /// </summary>
        /// <param name="listView">Specific source listview;<br />Listview-источник;</param>
        /// <param name="specificPointer">Respective f.s.p.;<br />Соответствующий указатель;</param>
        /// <returns></returns>
        private string GetItemPathForAnyActiveListView(ListView listView, FileSystemPointer specificPointer)
        {
            string sRes = "";

            string selectedItemName = listView.SelectedItems[0].Text;

            foreach (var dir in specificPointer.CurrentDirectory.GetDirectories())
            {
                if (dir.Name.Contains(selectedItemName)) sRes = dir.FullName;
            }

            foreach (var file in specificPointer.CurrentDirectory.GetFiles())
            {
                if (file.Name.Contains(selectedItemName)) sRes = file.FullName;
            }

            return sRes;
        }



        #endregion Module : Icon ToolStrip





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


        /// <summary>
        /// Auxiliary list of all drives of a current system;
        /// <br />
        /// Вспомогательный список дисков данной системы;
        /// </summary>
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

            ShowDirectoryContents(LeftListView, LeftWindowPointer);

            ShowDirectoryContents(RightListView, RightWindowPointer);
        }


        #endregion CONSTRUCTION





        #region Trash bin - A Codespace for auto-generated methods for disposal


        private void MiddleToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }





        #endregion Trash bin - A Codespace for auto-generated methods for disposal


    }
}
