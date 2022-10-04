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


        /// <summary>
        /// ;
        /// <br />
        /// ;
        /// </summary>
        private static readonly ManualResetEvent manualReset = new ManualResetEvent(false);


        /// <summary>
        /// ;
        /// <br />
        /// ;
        /// </summary>
        private static readonly string ReportFileName = "../../../copied-files/!REPORT.rep";


        /// <summary>
        /// ;
        /// <br />
        /// ;
        /// </summary>
        private static readonly object locker = new object();


        /// <summary>
        /// ;
        /// <br />
        /// ;
        /// </summary>
        private static int progressFileCounter = 0;


        /// <summary>
        /// ;
        /// <br />
        /// ;
        /// </summary>
        private static int overallProgressFileCounter = 0;


        /// <summary>
        /// ;
        /// <br />
        /// ;
        /// </summary>
        private static bool IsRunnin = false;



        #endregion PROPERTIES





        #region HANDLERS


        /// <summary>
        /// Scan Button click handler;
        /// <br />
        /// Хендлер клика по кнопке "Scan";
        /// </summary>
        private async void OnScanButtonClickAsync(object sender, EventArgs e)
        {
            IsRunnin = true;

            await Task.Run(() =>
            {
                manualReset.WaitOne();
                // Searching all system takes too long on ITStep computers, the teacher said this folder is enough;
                // for now, this is ok. we cannot use await inside of another one;
                ScanDirectoryAsync(new DirectoryInfo(@"C:\Users"));

                //
                //
                //ScanAllAsync();
            });

            SearchButton.Enabled = true;
        }



        /// <summary>
        /// Search button async click handler;
        /// <br />
        /// Асинхронный(?) обработчик нажатия на кнопку "Поиск";
        /// </summary>
        private async void OnSearchButtonClickAsync(object sender, EventArgs e)
        {
            // clear the 'copied-files' folder so that it won't stack up;
            ClearCopyFolder();

            // Change flag for stop/resume button;
            IsRunnin = true;

            // set current progressbar score to zero;
            progressFileCounter = 0;
            progressBar1.Value = progressFileCounter;

            // max progressbar score;
            progressBar1.Maximum = overallProgressFileCounter;

            // if the lisft of words is empty, add something
            if (forbiddenWords.Count == 0)
            {
                forbiddenWords.AddRange(new string[1] { "and" });
            }



            // Searching all system takes too long on ITStep computers, the teacher said this folder is enough;
            await Task.Run(() => Search(new DirectoryInfo(@"C:\Users")));



            // Unlock this if you want to search all the filesystem;
            //
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
                MessageBox.Show("Unable to open your file." + "\nException: " + ex.Message, "Exception.", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


        #endregion HANDLERS



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
                // Add manual reset event to this method;
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

                            // It's ok, we cannot call one await inside of another. I guess it will be called automatically;
                            ScanDirectoryAsync(dir);
                        }
                        catch { }
                    }
                }
                catch { }
            });
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
            // It's an old algorythm and I almost haven't changed it;
            string[] str = new string[0];
            int nReplacesCounter;

            try
            {
                DirectoryInfo[] directories = di.GetDirectories();

                // scan all directories in a current one untill we get to the very bottom;
                // dunnow if this works. the tests say it does work;
                Parallel.ForEach(directories, Search);

                // if there's no directories left;
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
                                        // Add manual reset event to this method;
                                        manualReset.WaitOne();
                                        try
                                        {
                                            isFound = false;
                                            for (int i = 0; i < str.Count(); i++)
                                            {
                                                isFound = false;

                                                // if we search for mathes just as it is, w/o " " and/or "," etc; we will find uncensored words
                                                // in parts of some other words. For example: word "and" will be found in the word "Android" and etc.
                                                if
                                                (
                                                        Regex.IsMatch(str[i], " " + word + " ", RegexOptions.IgnoreCase & RegexOptions.Compiled)
                                                    || (Regex.IsMatch(str[i], word + " ", RegexOptions.IgnoreCase & RegexOptions.Compiled))
                                                    || (Regex.IsMatch(str[i], " " + word + ",", RegexOptions.IgnoreCase & RegexOptions.Compiled))
                                                    || (Regex.IsMatch(str[i], " " + word + ".", RegexOptions.IgnoreCase & RegexOptions.Compiled))
                                                )
                                                {
                                                    var compareString = str[i];

                                                    // yeah, unfortunatelly, we'll have to copy-paste it;
                                                    str[i] = str[i].Replace(" " + word + " ", " ******* ", comparisonType: StringComparison.OrdinalIgnoreCase);
                                                    str[i] = str[i].Replace(word + " ", " ******* ", comparisonType: StringComparison.OrdinalIgnoreCase);
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
                                        catch { }

                                        // If the uncensored word is found in a string of a file;
                                        if (isFound)
                                        {
                                            // it writes down info about it in a log file and also copies the found file itself;
                                            // to do this, we need to lock the output files. otherwise it won't just throw an exception,
                                            // but it would not work at all.
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


                                // this stuff we need to use win forms controls in whatever thread we are in right now.
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

                }
            }
            catch (Exception) { }
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


        private void TreeMainForm_Load(object sender, EventArgs e)
        {

        }





        #region CONSTRUCTION



        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public TreeMainForm()
        {
            // default stuff;
            InitializeComponent();

            // memory;
            forbiddenWords = new List<string>();

            // preparations;
            progressBar1.Step = 1;

            // make a hint for yourself to click 'Scan' before 'Search';
            SearchButton.Enabled = false;

            // unblock reset event;
            manualReset.Set();
        }



        #endregion CONSTRUCTION
    }
}