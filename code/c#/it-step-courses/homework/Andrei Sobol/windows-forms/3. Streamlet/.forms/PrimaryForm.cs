using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

using Streamlet.Service;
using Tools.ClassExtensions;

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
                OnAnyListViewSelectedItemChanged(_LeftWindow);
                _LeftWindow.ExplorerAddressBox.Text = _LeftWindow.ExplorerFilePointer?.CurrentDirectory?.FullName;
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
                OnAnyListViewSelectedItemChanged(_RightWindow);
                _RightWindow.ExplorerAddressBox.Text = _RightWindow.ExplorerFilePointer?.CurrentDirectory?.FullName;
            }
        }


        /// <summary>
        /// Left listview selected value change handler;
        /// <br />
        /// Хендлер изменения выбранного значения левого списка;
        /// </summary>
        private void OnLeftListViewSelectedValueChanged(object sender, EventArgs e)
        {
            OnAnyListViewSelectedItemChanged(_LeftWindow);
        }


        /// <summary>
        /// Right listview selected value change handler;
        /// <br />
        /// Хендлер изменения выбранного значения правого списка;
        /// </summary>
        private void OnRighttListViewSelectedValueChanged(object sender, EventArgs e)
        {
            OnAnyListViewSelectedItemChanged(_RightWindow);
        }


        #endregion Specific Handlers - Pairs of handlers one for each side



        #region Generic Handlers - Generic methods that are dispatched by specific ones


        /// <summary>
        /// Any listview selected value change handler;
        /// <br />
        /// Хендлер изменения выбранного значения любого списка;
        /// </summary>
        /// <param name="listView">
        /// Listview in which to display;
        /// <br />
        /// Listview в котором отобразить;
        /// </param>
        /// <param name="DirectoryPointer">
        /// Respective directory pointer;
        /// <br />
        /// Соответствующий указатель файловой системы;
        /// </param>
        private void OnAnyListViewSelectedItemChanged(ExplorerWindow currentExplorerWindow)
        {
            if (!(currentExplorerWindow != null && currentExplorerWindow.Initialized)) return;

            _ActiveWindow = currentExplorerWindow;

            ListViewItem escapeItem = new ListViewItem(GoUpEscapeString);

            bool bIsEscapeStringDebugFlag = false, bDirectoryFlag = false;

            foreach (ListViewItem unit in currentExplorerWindow.ExplorerListView.SelectedItems)
            {
                if (unit.ToString().Contains(escapeItem.ToString())) bIsEscapeStringDebugFlag = true;
            }

            if (bIsEscapeStringDebugFlag == false)
            {
                if (currentExplorerWindow?.ExplorerFilePointer?.CurrentDirectory == null) 
                {
                    MoveDown(currentExplorerWindow);
                    return;
                }

                else
                {

                    foreach (DirectoryInfo unit in currentExplorerWindow.ExplorerFilePointer.CurrentDirectory.GetDirectories())
                    {
                        if (currentExplorerWindow.ExplorerListView.SelectedItems.Count != 0)
                        {
                            if (currentExplorerWindow.ExplorerListView.SelectedItems[0].Text == unit.Name) bDirectoryFlag = true;
                        }
                    }

                    if (bDirectoryFlag)
                    {
                        MoveDown(currentExplorerWindow);
                    }
                    else
                    {
                        FileInfo fileToOpen = currentExplorerWindow.ExplorerFilePointer.CurrentDirectory
                            .GetFiles()
                            .ToList()
                            .Find(unit => unit.Name == currentExplorerWindow.ExplorerListView.SelectedItems[0].Text);

                        // if it is a text file;
                        if (File.Exists(fileToOpen?.FullName)) 
                        {
                            if (StringExtension.CompareMultiple(
                                sourceString: fileToOpen.Extension,
                                compareType: StringComparison.OrdinalIgnoreCase,
                                compareValues: new string[] { ".txt", ".html", ".xml", ".doc" }))
                            {
                                // then open it in a new form;
                                SecondaryForm textEditorForm = new SecondaryForm(fileToOpen);
                                textEditorForm.Show();
                            }
                        }
                    }
                }
            }
            else
            {
                MoveUp(currentExplorerWindow);
            }
        }


        /// <summary>
        /// Key down event handler;
        /// <br />
        /// Обработчик события нажатия на клавишу;
        /// </summary>
        private void OnPrimaryFormKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    OnAnyListViewSelectedItemChanged(_ActiveWindow);
                break;


                case Keys.Escape:
                    MoveUp(_ActiveWindow);
                break;


                case Keys.Right:
                    OnCopyRightButtonClick(new(), new());
                break;


                case Keys.Left:
                    OnCopyLeftButtonClick(new(), new());
                break;
            }
        }



        #endregion Generic Handlers - Generic methods that are dispatched by specific ones



        #region Common non-handler Logic



        /// <summary>
        /// Get down the level in a local file system;
        /// <br />
        /// Перейти на уровень вниз по файловой системе;
        /// </summary>
        /// <param name="listView">
        /// Listview in which to display;
        /// <br />
        /// Listview в котором отобразить;
        /// </param>
        /// <param name="DirectoryPointer">
        /// Respective directory pointer;
        /// <br />
        /// Соответствующий указатель файловой системы;
        /// </param>
        private void MoveDown(ExplorerWindow currentExplorerWindow)
        {
            bool bDebugFlag = false;


            // TL;DR: Использовать меньше var'ов и/или контролить типы.
            #region The 'var' bug
            /*
             
            
            В общем, в блоке try-catch (ещё ниже) был жёсткий баг, связанный с тем, что я использовал 'var' вместо
            реального типа объекта и цикл выдавал мне элементы в типе 'object' вместо 'ListViewItem'.
            Я сидел над этой хренью наверно час, в попытках исправить сделал вот эту херь ниже.
            Внезапно, она отработала хорошо, но потом мне пришёл в голову вариант проще. 

            Нужно это всё было для того, чтобы исправить баг, связанный с тем, что до 'Equals' у меня был метод 'Contains',
            который неправильно работал. Например, если бы у вас были две папки с именами "A" и "AP Tuner 3.08", когда вы нажмёте
            на вторую, то "проводник" всё равно попадёт в первую. 

             

            string sSelectedItemRealName = "";

            Regex matchRegex;

            foreach (var selectedItem in listView.SelectedItems)
            {
                foreach (ListViewItem generalItem in listView.Items)
                {
                    matchRegex = new Regex(@"\b" + Regex.Escape(generalItem.Text) + @"\b", RegexOptions.IgnoreCase);
                    if (matchRegex.Match(selectedItem.ToString()).Success)
                    {
                        sSelectedItemRealName = generalItem.Text;
                        break;
                    }
                }
            }


            */
            #endregion The 'var' bug



            try
            {
                DriveInfo selectedDrive = null;

                foreach (var item in machineDriveInfo)
                {
                    foreach (var unit in currentExplorerWindow?.ExplorerListView?.SelectedItems)
                    {
                        if (unit.ToString().Contains(item.Name)) selectedDrive = item;
                    }
                }

                if (null != selectedDrive)
                {
                    currentExplorerWindow?.ExplorerFilePointer?.NextDirectory(selectedDrive.RootDirectory);
                    ShowDirectoryContents(currentExplorerWindow);
                }
                else
                {
                    try
                    {
                        var tempNullCheckRef = currentExplorerWindow?.ExplorerFilePointer?.CurrentDirectory?.GetDirectories();

                        if (tempNullCheckRef is not null)
                        {

                            foreach (DirectoryInfo generalItem in currentExplorerWindow?.ExplorerFilePointer?.CurrentDirectory?.GetDirectories())
                            {
                                foreach (ListViewItem selectedItem in currentExplorerWindow?.ExplorerListView?.SelectedItems)
                                {
                                    bDebugFlag = generalItem.Name.Equals(selectedItem.Text);

                                    if (bDebugFlag)
                                    {
                                        currentExplorerWindow?.ExplorerFilePointer.NextDirectory(generalItem);
                                        currentExplorerWindow.ExplorerAddressBox.Text = currentExplorerWindow?.ExplorerFilePointer?.CurrentDirectory.FullName;
                                        ShowDirectoryContents(currentExplorerWindow);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    catch { }
                }
            }
            catch (System.UnauthorizedAccessException ex)
            {
                ShowFailMessage(currentExplorerWindow);
            }
        }


        /// <summary>
        /// Get up the level in a local file system;
        /// <br />
        /// Перейти на уровень вверх по файловой системе;
        /// </summary>
        /// <param name="listView">
        /// Listview in which to display;
        /// <br />
        /// Listview в котором отобразить;
        /// </param>
        /// <param name="ExplorerPointer">
        /// Respective directory pointer;
        /// <br />
        /// Соответствующий указатель файловой системы;
        /// </param>
        private void MoveUp(ExplorerWindow currentExplorerWindow)
        {
            if (currentExplorerWindow.ExplorerFilePointer!= null && currentExplorerWindow.ExplorerFilePointer.CurrentDirectory != null)
            {
                currentExplorerWindow.ExplorerFilePointer.NextDirectory(currentExplorerWindow?.ExplorerFilePointer?.CurrentDirectory?.Parent);
                currentExplorerWindow.ExplorerAddressBox.Text = currentExplorerWindow.ExplorerFilePointer?.CurrentDirectory?.FullName;
            }
            ShowDirectoryContents(currentExplorerWindow);
        }


        /// <summary>
        /// Show the list of directory contents in a specific listview;
        /// <br />
        /// Отобразить содержимое папки в конкретном listview;
        /// </summary>
        /// <param name="listView">
        /// Listview in which to display;
        /// <br />
        /// Listview в котором отобразить;
        /// </param>
        /// <param name="DirectoryPointer">
        /// Respective directory pointer;
        /// <br />
        /// Соответствующий указатель файловой системы;
        /// </param>
        private void ShowDirectoryContents(ExplorerWindow currentExplorerWindow)
        {
            try
            {
                if (currentExplorerWindow?.ExplorerFilePointer?.CurrentDirectory != null)
                {
                    currentExplorerWindow.ExplorerListView.Items.Clear();

                    // Add escape pattern;
                    currentExplorerWindow.ExplorerListView.Items.Add(new ListViewItem(GoUpEscapeString));

                    // directories;
                    currentExplorerWindow.ExplorerFilePointer.CurrentDirectory.GetDirectories().ToList().ForEach(unit =>
                    {
                        ListViewItem item = new ListViewItem(unit.Name);
                        item.SubItems.Add(unit.Extension);
                        item.SubItems.Add("");
                        item.SubItems.Add(unit.LastWriteTime.ToShortDateString());
                        currentExplorerWindow.ExplorerListView.Items.Add(item);
                    });

                    // files;
                    currentExplorerWindow.ExplorerFilePointer.CurrentDirectory.GetFiles().ToList().ForEach(unit =>
                    {
                        ListViewItem item = new ListViewItem(unit.Name);
                        item.SubItems.Add(unit.Extension);
                        item.SubItems.Add(unit.Length.ToString());
                        item.SubItems.Add(unit.LastWriteTime.ToShortDateString());
                        currentExplorerWindow.ExplorerListView.Items.Add(item);
                    });
                }
                else ShowDrives(currentExplorerWindow);
            }
            catch (System.UnauthorizedAccessException ex)
            {
                ShowFailMessage(currentExplorerWindow);
            }
        }


        /// <summary>
        /// Show the list of drives in a specific listview;
        /// <br />
        /// Отобразить список дисков в конкретном listview;
        /// </summary>
        /// <param name="listView">
        /// Listview in which to display;
        /// <br />
        /// Listview в котором отобразить;
        /// </param>
        private void ShowDrives(ExplorerWindow currentExplorerWindow)
        {
            if (!currentExplorerWindow.Initialized) return;

            var driveList = DriveInfo.GetDrives();

            currentExplorerWindow?.ExplorerListView?.Items.Clear();

            if (machineDriveInfo.Count == 0) machineDriveInfo.AddRange(driveList);

            foreach (var item in driveList)
            {
                ListViewItem LVItem = new ListViewItem(item.Name);
                LVItem.SubItems.Add("");
                LVItem.SubItems.Add("");
                LVItem.SubItems.Add("");

                currentExplorerWindow?.ExplorerListView?.Items.Add(LVItem);
            }
        }


        /// <summary>
        /// Show fail message in case the directory is protected by the OS;
        /// <br />
        /// Отобразить сообщение об ошибке, если директория защищена ОС;
        /// </summary>
        /// <param name="listView">
        /// Listview in whick to display;
        /// <br />
        /// Listview в котором отобразить;
        /// </param>
        private void ShowFailMessage(ExplorerWindow currentExplorerWindow)
        {
            currentExplorerWindow?.ExplorerListView?.Items.Clear();
            currentExplorerWindow?.ExplorerListView?.Items.Add(GoUpEscapeString);
            currentExplorerWindow?.ExplorerListView?.Items.Add("\n");
            currentExplorerWindow?.ExplorerListView?.Items.Add("\tAccess denied.\n");
            currentExplorerWindow?.ExplorerListView?.Items.Add("\n");
            currentExplorerWindow?.ExplorerListView?.Items.Add("\t\t[ .. ]  to leave....");
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
            OnAnyAddressTextBoxLeave(_LeftWindow);
        }

        /// <summary>
        /// When right address box gets inactive;
        /// <br />
        /// Когда уйдёт фокус с правой адресной строки;
        /// </summary>
        private void OnRightAddressTextBoxLeave(object sender, EventArgs e)
        {
            OnAnyAddressTextBoxLeave(_RightWindow);
        }



        /// <summary>
        /// When left address box gets some key pressed;
        /// <br />
        /// Когда в левой строке нажата клавиша;
        /// </summary>
        private void OnLeftAddressTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            OnAnyListBoxKeyDown(_LeftWindow, e);
        }

        /// <summary>
        /// When right address box gets some key pressed;
        /// <br />
        /// Когда в правой строке нажата клавиша;
        /// </summary>
        private void OnRightAddressTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            OnAnyListBoxKeyDown(_RightWindow, e);
        }



        #endregion Specific Handlers (see ListViews region)



        #region Generic Handlers (see ListViews region)



        /// <summary>
        /// Any address box key down handler;
        /// <br />
        /// Обработчик нажатия для всех адресных строк;
        /// </summary>
        /// <param name="currentExplorerWindow">
        /// Passed explorer window;
        /// <br />
        /// Передаваемое проводниковое окно;
        /// </param>
        /// <param name="e">
        /// Key pressed;
        /// <br />
        /// Нажатая кнопка;
        /// </param>
        private void OnAnyListBoxKeyDown(ExplorerWindow currentExplorerWindow, KeyEventArgs e)
        {
            // 'enter';
            if (e.KeyCode == Keys.Enter) OnAnyAddressTextBoxLeave(currentExplorerWindow);
            // 'esc';
            else if (e.KeyCode == Keys.Escape)
            {
                currentExplorerWindow.ExplorerAddressBox.Text = "\'/\\]%";
                OnAnyAddressTextBoxLeave(currentExplorerWindow);
            }
        }


        /// <summary>
        /// When any address box gets inactive;
        /// <br />
        /// Когда уйдёт фокус с любой адресной строки;
        /// </summary>
        /// <param name="listBox">
        /// The exact listbox;
        /// <br/>
        /// Конкретный листбокс;
        /// </param>
        /// <param name="specificTextBox">
        /// The very address box;
        /// <br/>
        /// Конкретный адрес бокс;
        /// </param>
        /// <param name="ptr">
        /// Respective custom file pointer;
        /// <br/>
        /// Соответствующий указатель файловой системы;
        /// </param>
        private void OnAnyAddressTextBoxLeave(ExplorerWindow currentExplorerWindow)
        {
            string sText = currentExplorerWindow.ExplorerAddressBox.Text;

            if (File.Exists(sText) && sText.EndsWith(".txt"))
            {
                currentExplorerWindow.ExplorerFilePointer.NextDirectory(new FileInfo(sText).Directory);
                var a = new SecondaryForm();
                a.Show();
            }
            else if (Directory.Exists(sText))
            {
                currentExplorerWindow.ExplorerFilePointer.NextDirectory(new DirectoryInfo(sText));
                ShowDirectoryContents(currentExplorerWindow);
            }
            else
            {
                currentExplorerWindow.ExplorerAddressBox.Text = currentExplorerWindow.ExplorerFilePointer.CurrentDirectory?.FullName;
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
            if (_ActiveWindow?.ExplorerListView?.SelectedItems != null)
            {
                if (_ActiveWindow == _LeftWindow) OnLeftListViewMouseDoubleClick(sender, e);
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
            if (_ActiveWindow?.ExplorerListView.SelectedItems != null)
            {
                string CopyToClipboardString = "Error. Debug message.";

                if (_ActiveWindow == _LeftWindow) 
                    CopyToClipboardString = GetItemPathForAnyActiveListView(_LeftWindow);

                else
                    CopyToClipboardString = GetItemPathForAnyActiveListView(_RightWindow);


                System.Windows.Forms.Clipboard.SetText(CopyToClipboardString);
            }
        }


        /// <summary>
        /// 'Delete' tool click handler;
        /// <br />
        /// Хендлер кнопки "Удалить";
        /// </summary>
        private void OnDeleteToolClick(object sender, EventArgs e)
        {
            if (_ActiveWindow?.ExplorerListView.SelectedItems != null)
            {
                DialogResult result =  
                    MessageBox.Show("Do you really want to delete the item(s)?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.No) return;
                else
                {
                    if (_ActiveWindow == _LeftWindow)
                    {
                        if (TryDeleteItems(_LeftWindow))
                            ShowDirectoryContents(_LeftWindow);
                    }

                    else
                    {
                        if (TryDeleteItems(_RightWindow))
                            ShowDirectoryContents(_RightWindow);
                    }
                }
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
        private string GetItemPathForAnyActiveListView(ExplorerWindow currentExplorerWindow)
        {
            string sRes = "";

            string selectedItemName = currentExplorerWindow.ExplorerListView.SelectedItems[0].Text;

            foreach (var dir in currentExplorerWindow.ExplorerFilePointer.CurrentDirectory.GetDirectories())
            {
                if (dir.Name.Contains(selectedItemName)) sRes = dir.FullName;
            }

            foreach (var file in currentExplorerWindow.ExplorerFilePointer.CurrentDirectory.GetFiles())
            {
                if (file.Name.Contains(selectedItemName)) sRes = file.FullName;
            }

            return sRes;
        }


        /// <summary>
        /// Try and delete selected listview items;
        /// <br />
        /// Попробовать удалить пункты из listview;
        /// </summary>
        /// <param name="currentExplorerWindow">
        /// Current window;
        /// <br />
        /// Текущее окно;
        /// </param>
        /// <returns>
        /// True if no exception, otherwise false;
        /// <br />
        /// "True" если нет эксепшена, иначе "false";
        /// </returns>
        private bool TryDeleteItems(ExplorerWindow currentExplorerWindow)
        {
            bool bRes = false;

            bool isItemAlreadyDeleted = false;


            // for all selected files;
            foreach (var item in currentExplorerWindow.ExplorerListView.SelectedItems)
            {
                isItemAlreadyDeleted = false;

                // for all directories;
                foreach (var dir in currentExplorerWindow.ExplorerFilePointer.CurrentDirectory.GetDirectories())
                {
                    // if target is a directory;
                    if (item.ToString().Contains(dir.Name))
                    {
                        // try delete it;
                        try
                        {
                            dir.Delete();
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show($"You cannot delete this item ({dir.Name}).\n\n{e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        // then we don't need to check if it's a file;
                        isItemAlreadyDeleted = true;

                        break;
                    }
                }

                // if target was a file;
                if (false == isItemAlreadyDeleted)
                {
                    // for all files;
                    foreach (var file in currentExplorerWindow.ExplorerFilePointer.CurrentDirectory.GetFiles())
                    {
                        // if there's a match;
                        if (item.ToString().Contains(file.Name))
                        {
                            // try delete;
                            try
                            {
                                file.Delete();
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show($"You cannot delete this item ({file.Name}).\n\n{e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            // then we don't need to check if it's a file;
                            isItemAlreadyDeleted = true;

                            break;
                        }
                    }
                }

            }

            bRes = isItemAlreadyDeleted;

            return bRes;
        }



        #endregion Module : Icon ToolStrip




        #region Module : Middle icon buttons



        /// <summary>
        /// Copy to right button;
        /// <br />
        /// Кнопка копировать направо;
        /// </summary>
        private void OnCopyRightButtonClick(object sender, EventArgs e)
        {
            OnAnyCopyButtonClick(_RightWindow);
        }


        /// <summary>
        /// Copy to left button;
        /// <br />
        /// Кнопка копировать налево;
        /// </summary>
        private void OnCopyLeftButtonClick(object sender, EventArgs e)
        {
            OnAnyCopyButtonClick(_LeftWindow);
        }


        /// <summary>
        /// Copy buttons click handler;
        /// <br />
        /// Хендлер нажатия копирующих кнопок;
        /// </summary>
        /// <param name="currentExplorerWindow">
        /// Corresponding explorer window;
        /// <br />
        /// Соответствующее окно проводника;
        /// </param>
        private void OnAnyCopyButtonClick(ExplorerWindow currentExplorerWindow)
        {
            // выделение противоположного окна;
            ExplorerWindow oppositeWindow = new();

            if (_RightWindow == currentExplorerWindow) oppositeWindow = _LeftWindow;

            else oppositeWindow = _RightWindow;


            // копирование элементов;
            if (oppositeWindow.ExplorerListView.SelectedItems != null && oppositeWindow.ExplorerListView.SelectedItems.Count != 0)
            {
                foreach (ListViewItem itemName in oppositeWindow.ExplorerListView.SelectedItems)
                {
                    foreach (var file in oppositeWindow.ExplorerFilePointer.CurrentDirectory.GetFiles())
                    {
                        if (file.Name.Equals(itemName.Text))
                        {
                            try
                            {
                                // Здесь почему-то експешн не ловится;
                                if (currentExplorerWindow?.ExplorerFilePointer?.CurrentDirectory != null)
                                    File.Move(file.FullName, $"{currentExplorerWindow?.ExplorerFilePointer?.CurrentDirectory.FullName}\\{file.Name}");

                                else MessageBox.Show("Current destination path is unavailable. Please, choose another one.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            catch
                            {
                                MessageBox.Show("Current destination path is unavailable. Please, choose another one.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }

                    foreach (var dir in oppositeWindow.ExplorerFilePointer.CurrentDirectory.GetDirectories())
                    {
                        if (dir.Name.Equals(itemName.Text))
                        {
                            try
                            {
                                // Здесь почему-то експешн не ловится;
                                if (currentExplorerWindow?.ExplorerFilePointer?.CurrentDirectory != null)
                                    Directory.Move(dir.FullName, currentExplorerWindow?.ExplorerFilePointer?.CurrentDirectory.FullName);
                                else MessageBox.Show("Current destination path is unavailable. Please, choose another one.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            catch
                            {
                                MessageBox.Show("Current destination path is unavailable. Please, choose another one.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }

                ShowDirectoryContents(currentExplorerWindow);

                ShowDirectoryContents(oppositeWindow);

            }
        }



        #endregion Module : Middle icon buttons







        #region PROPERTIES



        /// <summary>
        /// A pattern that represents the 'go-higher' option in the list boxes;
        /// <br />
        /// Набор символов, котоырй представляет собой переход на уровень выше в лист-боксах;
        /// </summary>
        private string GoUpEscapeString = "[ .. ]";


        /// <summary>
        /// Auxiliary list of all drives of a current system;
        /// <br />
        /// Вспомогательный список дисков данной системы;
        /// </summary>
        private List<DriveInfo> machineDriveInfo;


        /// <summary>
        /// Left listview window;
        /// <br />
        /// Левое проводниковое окно;
        /// </summary>
        private ExplorerWindow _LeftWindow;


        /// <summary>
        /// Right listview window;
        /// <br />
        /// Правое проводниковое окно;
        /// </summary>
        private ExplorerWindow _RightWindow;


        /// <summary>
        /// Active listview window;
        /// <br />
        /// Активное проводниковое окно;
        /// </summary>
        private ExplorerWindow _ActiveWindow;



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
            
            machineDriveInfo = new();

            _ActiveWindow = new();

            _LeftWindow = new ExplorerWindow(LeftListView, new(), LeftAddressTextBox);
            _RightWindow = new ExplorerWindow(RightListView, new(), RightAddressTextBox);
        }



        /// <summary>
        /// Prepare list boxes, show a list of the disks;
        /// <br />
        /// Подготовить лист-боксы, отобразить список дисков;
        /// </summary>
        private void OnPrimaryFormLoad(object sender, EventArgs e)
        {
            LeftListView.Groups.Clear();

            ShowDirectoryContents(_LeftWindow);

            ShowDirectoryContents(_RightWindow);
        }



        #endregion CONSTRUCTION






        #region Trash bin - A Codespace for auto-generated methods for disposal



        private void MiddleToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }


        private void LeftListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        #endregion Trash bin - A Codespace for auto-generated methods for disposal


    }
}
