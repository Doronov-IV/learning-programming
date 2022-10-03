using System.Runtime;

namespace Doronov.ConcurrencyExam.Forms
{
    public partial class TreeMainForm : Form
    {


        #region PROPERTIES

        /// <summary>
        /// ;
        /// <br />
        /// ;
        /// </summary>
        private List<string> forbiddenWords;

        #endregion PROPERTIES


        private static readonly ManualResetEvent manualReset = new ManualResetEvent(false);


        private static readonly string ReportFileName = "../../../copied-files/!REPORT.rep";


        private static readonly object locker = new object();

        private static int progressFileCounter = 0;

        private static int overallProgressFileCounter = 0;

        private static bool IsRunnin = false;


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public TreeMainForm()
        {
            InitializeComponent();

            // memory;
            forbiddenWords = new List<string>();

            // preparations;
            progressBar1.Step = 1;

            SearchButton.Enabled = false;

            manualReset.Set();
        }


        private async void OnScanButtonClickAsync(object sender, EventArgs e)
        {
            IsRunnin = true;

            await Task.Run(() => 
            {
                manualReset.WaitOne();
                ScanDirectoryAsync(new DirectoryInfo(@"C:\Users"));
                //ScanAllAsync();
            });

            SearchButton.Enabled = true;
        }



        /// <summary>
        /// Scan all the system;
        /// <br />
        /// Сканировать всю систему;
        /// </summary>
        public async Task ScanAllAsync()
        {
            try
            {
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    await Task.Run(() =>
                    {
                        manualReset.WaitOne();

                        try
                        {
                            ScanDirectoryAsync(drive.RootDirectory);
                        }
                        catch { }
                        

                    });
                }
            }
            catch (Exception) { }
        }



        /// <summary>
        /// Scan directory;
        /// <br />
        /// Сканировать директорию;
        /// </summary>
        private async Task ScanDirectoryAsync(DirectoryInfo info)
        {
            await Task.Run(() =>
            {
                manualReset.WaitOne();
                try
                {
                    foreach (DirectoryInfo dir in info.GetDirectories())
                    {
                        try
                        {
                            foreach (FileInfo file in dir.GetFiles())
                            {
                                if (file.Extension == ".txt" || file.Extension == ".xml")
                                {
                                    if (dir.Name != "copied-files") overallProgressFileCounter += 1;
                                }
                            }

                            if (label2.InvokeRequired)
                            {
                                Invoke(() =>
                                {
                                    label2.Text = overallProgressFileCounter.ToString();
                                });
                            }


                            //ScanAllAsync();
                            ScanDirectoryAsync(dir);
                        }
                        catch { }
                    }

                    //Task.WhenAll(info.GetDirectories().AsParallel().Select(dir => ScanDirectoryAsync(dir)));
                }
                catch { }

            });
        }
    




        /// <summary>
        /// Search button async click handler;
        /// <br />
        /// Ассинхронный(?) обработчик нажатия на кнопку "Поиск";
        /// </summary>
        private async void OnSearchButtonClickAsync(object sender, EventArgs e)
        {
            ClearCopyFolder();
            IsRunnin = true;
            progressFileCounter = 0;
            progressBar1.Maximum = overallProgressFileCounter;
            if (forbiddenWords.Count == 0)
            {
                forbiddenWords.AddRange(new string[1] { "and"});
                //forbiddenWords.ForEach(word => wordCounterPairs.Add(word, 0));
            }

            progressBar1.Value = 0;

            await Task.Run(() => Search(new DirectoryInfo(@"C:\Users")));

            /*await Task.Run(() => 
            {
                manualReset.WaitOne();
                try
                {
                    try
                    {
                        foreach (DriveInfo drive in DriveInfo.GetDrives())
                        {
                            foreach (DirectoryInfo folder in drive.RootDirectory.GetDirectories())
                            {
                                Search(folder);
                            }
                        }
                    }
                    catch { }
                    
                }
                catch { }
                
            });*/

        }


        /// <summary>
        /// Search the directory;
        /// <br />
        /// Искать в папке;
        /// </summary>
        /// <param name="di">
        /// Directory;
        /// <br />
        /// Директория;
        /// </param>
        private async void Search(DirectoryInfo di)
        {
            string[] str = new string[0];
            int nReplacesCounter;

            try
            {
                DirectoryInfo[] directories = di.GetDirectories();

                Parallel.ForEach(directories, Search);

                if (directories.Count() == 0 || di.GetFiles().Count() > 0)
                {
                    nReplacesCounter = 0;
                    FileInfo[] files = di.GetFiles();

                        foreach (FileInfo file in files)
                        {
                        if (file.Extension == ".txt" || file.Extension == ".xml")
                        {
                            if (di.Name != "copied-files") progressFileCounter++;
                            await Task.Run(() =>
                            {
                                manualReset.WaitOne();
                                bool isFound = false;
                                // читает файл по пути
                                try
                                {
                                    str = System.IO.File.ReadAllLines(file.FullName);
                                }
                                catch { }
                                foreach (string word in forbiddenWords)
                                {
                                    Task.Run(() =>
                                    {
                                        manualReset.WaitOne();
                                        try
                                        {
                                            isFound = false;
                                            for (int i = 0; i < str.Count(); i++)
                                            {
                                                isFound = false;
                                                if
                                                (
                                                        Regex.IsMatch(str[i], " " + word + " ", RegexOptions.IgnoreCase & RegexOptions.Compiled) 
                                                    || (Regex.IsMatch(str[i], word + " ", RegexOptions.IgnoreCase & RegexOptions.Compiled))
                                                    || (Regex.IsMatch(str[i], " " + word + ",", RegexOptions.IgnoreCase & RegexOptions.Compiled))
                                                    || (Regex.IsMatch(str[i], " " + word + ".", RegexOptions.IgnoreCase & RegexOptions.Compiled))
                                                )
                                                {
                                                    var compareString = str[i];

                                                    str[i] = str[i].Replace(" " + word + " ", " ******* ", comparisonType: StringComparison.OrdinalIgnoreCase);
                                                    str[i] = str[i].Replace( word + " ", " ******* ", comparisonType: StringComparison.OrdinalIgnoreCase);
                                                    str[i] = str[i].Replace(" " + word + ",", " ******* ", comparisonType: StringComparison.OrdinalIgnoreCase);
                                                    str[i] = str[i].Replace(" " + word + ".", " ******* ", comparisonType: StringComparison.OrdinalIgnoreCase);

                                                    if (!compareString.Equals(str[i]))
                                                    {
                                                        isFound = true;
                                                        nReplacesCounter++;
                                                    }
                                                }
                                            }
                                        }
                                        catch (Exception exce)
                                        {

                                        }




                                        if (isFound)
                                        {
                                            lock (locker)
                                            {
                                                try
                                                {
                                                    if (file.FullName != ReportFileName)
                                                    {
                                                        System.IO.File.WriteAllLines($"../../../copied-files/{file.Name} - copy{file.Extension}", str, Encoding.UTF8);
                                                        System.IO.File.AppendAllText(ReportFileName, $"{file.Name}, word: {word}.\n", Encoding.UTF8);
                                                    }
                                                }
                                                catch { }
                                            }
                                        }
                                        

                                    });
                                }
                               

                                try
                                {
                                    if (progressBar1.InvokeRequired) Invoke(() =>
                                    {
                                        progressBar1.PerformStep();
                                        label1.Text = progressFileCounter.ToString();
                                    });
                                }
                                    
                                catch { }
                            });


                        }


                        }




                    return;

                }
            }
            catch (Exception) { }
        }


        /// <summary>
        /// Show search statistics;
        /// <br />
        /// Показать статистику поиска;
        /// </summary>
        private void ShowStatistics()
        {
            
        }

        /// <summary>
        /// Statistics button click handler;
        /// <br />
        /// Хендлер клика по кнопке "Статистика";
        /// </summary>
        private void OnStatisticsButtonClick(object sender, EventArgs e)
        {
            ShowStatistics();
        }


        /// <summary>
        /// Clear the "copies" folder;
        /// <br />
        /// Очистить кнопку "Копии";
        /// </summary>
        private void ClearCopyFolder()
        {
            DirectoryInfo info = new DirectoryInfo("../../../copied-files");
            foreach (FileInfo file in info.GetFiles())
            {
                try
                {
                    System.IO.File.Delete(file.FullName);
                }
                catch
                {

                }
            }
        }



        /// <summary>
        /// Copy folder button click handler;
        /// <br />
        /// Хендлер клика по кнопке "Открыть папку копий";
        /// </summary>
        private void OnOpenCopyFolderButtonClick(object sender, EventArgs e)
        {
            DirectoryInfo info = new DirectoryInfo(@"../../../copied-files");
            try 
            { 
            Process.Start("explorer", info.FullName);
            }
            catch (Exception ex)
            {
                Directory.CreateDirectory(info.FullName);
                Process.Start("explorer", info.FullName);
            }
        }

        /// <summary>
        /// Open file button click handler;
        /// <br />
        /// Хендлер клика кнопки "открыть файл";
        /// </summary>
        private void OnOpenFileButtonClick(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.InitialDirectory = @"C:\";
                fileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    forbiddenWords = System.IO.File.ReadAllLines(fileDialog.FileName).ToList();
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Unable to open your file." + "\nException: " +ex.Message, "Exception.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnStopButtonClick(object sender, EventArgs e)
        {
            if (IsRunnin)
            {
                manualReset.Reset();
                IsRunnin = false;
                StopButton.Text = "Resume";
            }
            else
            {
                manualReset.Set();
                IsRunnin = true;
                StopButton.Text = "Stop";
            }
        }

        private void TreeMainForm_Load(object sender, EventArgs e)
        {

        }

        private void OnClearFolderButtonClick(object sender, EventArgs e)
        {
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(@"../../../copied-files");
                directoryInfo.GetFiles().ToList().ForEach(file => file.Delete());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to clear your directory." + "\nException: " + ex.Message, "Exception.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}