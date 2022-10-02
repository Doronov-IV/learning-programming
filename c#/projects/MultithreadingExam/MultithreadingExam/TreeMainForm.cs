using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using static System.Net.WebRequestMethods;
using System.Diagnostics;

namespace SPExam
{
    public partial class TreeMainForm : Form
    {


        #region PROPERTIES

        /// <summary>
        /// ;
        /// <br />
        /// ;
        /// </summary>
        private List<FileStatistics> totalStatistics;

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
        private Dictionary<string, int> wordCounterPairs;

        /// <summary>
        /// ;
        /// <br />
        /// ;
        /// </summary>
        private List<string> TopWords;

        /// <summary>
        /// ;
        /// <br />
        /// ;
        /// </summary>
        private int overallFileAmount = 0;

        /// <summary>
        /// ;
        /// <br />
        /// ;
        /// </summary>
        private CancellationTokenSource TokenSource;


        #endregion PROPERTIES



        private static readonly AutoResetEvent AutoProgressBarSwitch = new AutoResetEvent(true);


        private static readonly object locker = new object();


        private static List<Task> TaskListWaitAll = new List<Task>();





        private int progressFileCounter = 0;

        private int overallProgressFileCounter = 0;


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
            TaskListWaitAll = new List<Task>();
            TokenSource = new CancellationTokenSource();

            // preparations;
            progressBar1.Step = 1;
            ShowStatisticsButton.Visible = false;
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
                        foreach (DirectoryInfo dir in drive.RootDirectory.GetDirectories())
                        {
                            ScanDirectoryAsync(dir);
                        }
                    });

                    overallProgressFileCounter += drive.RootDirectory.GetFiles().Length;
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
            try
            {
                await Task.Run(() =>
                {
                    try
                    {
                        foreach (DirectoryInfo dirInfo in info.GetDirectories())
                        {
                            overallProgressFileCounter += dirInfo.GetFiles().Length;

                            ScanDirectoryAsync(dirInfo);
                        }
                    }
                    catch { }
                });

                try
                {
                    if (info.GetDirectories().Count() == 0)
                    {
                        overallProgressFileCounter += info.GetFiles().Length;
                    }
                }
                catch { }
            }
            catch (Exception) { }
        }




        /// <summary>
        /// Search button async click handler;
        /// <br />
        /// Ассинхронный(?) обработчик нажатия на кнопку "Поиск";
        /// </summary>
        private async void OnSearchButtonClickAsync(object sender, EventArgs e)
        {
            ClearCopyFolder();
            await ScanAllAsync();
            progressBar1.Maximum = overallProgressFileCounter;
            if (forbiddenWords.Count == 0)
            {
                forbiddenWords.AddRange(new string[1] { "and"});
                //forbiddenWords.ForEach(word => wordCounterPairs.Add(word, 0));
            }

            overallFileAmount = 0;
            progressBar1.Value = 0;

            // await Task.Run(() => Search(new DirectoryInfo(@"D:\")));

            await Task.Run(() => { 
                foreach (var a in new DirectoryInfo(@"C:\").GetDirectories())
                {
                    Search(a);
                }
                });

            //ar topWords = wordCounterPairs.OrderBy(u => u.Value);

            ShowStatisticsButton.Visible = true;
            CreateStatisticsReportFile();
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
                            await Task.Run(() =>
                            {
                                progressFileCounter++;
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
                                        try
                                        {
                                            isFound = false;
                                            for (int i = 0; i < str.Count(); i++)
                                            {
                                                if (Regex.IsMatch(str[i], word, RegexOptions.IgnoreCase & RegexOptions.Compiled))
                                                {
                                                    var compareString = str[i];

                                                    str[i] = str[i].Replace(word, "*******", comparisonType: StringComparison.OrdinalIgnoreCase);

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
                                                    System.IO.File.WriteAllLines($"../../../copied-files/{file.Name} - copy{file.Extension}", str, Encoding.UTF8);
                                                }
                                                catch { }
                                            }
                                        }

                                    });
                                }

                                if (progressBar1.InvokeRequired) Invoke(() =>
                                {
                                    progressBar1.PerformStep();
                                    label1.Text = progressFileCounter.ToString();
                                });
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
        /// Create statistics report;
        /// <br />
        /// Создать отчёт статистики;
        /// </summary>
        private void CreateStatisticsReportFile()
        {
            //List<string> report = new List<string>();
            //totalStatistics.ForEach(str => report.Add("File Name: " + str.FileInfo.Name + ", Path: " + str.FileInfo.FullName + ", replaces: " + str.Counter));
            //report.Add("\n\n\n" + "Top popular words: ");
            //TopWords.ForEach(word => report.Add("\t" + word));

            //System.IO.File.WriteAllLines($"{DriveInfo.GetDrives()[1].RootDirectory}/ProjectReport.txt", report, Encoding.UTF8);
        }

        /// <summary>
        /// Open file button click handler;
        /// <br />
        /// Хендлер клика кнопки "открыть файл";
        /// </summary>
        private void OnOpenFileButtonClick(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = @"C:\";
            fileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

            //if (fileDialog.ShowDialog() == DialogResult.OK) forbiddenWords = System.IO.File.ReadAllLines(fileDialog.FileName).ToList();
            //forbiddenWords.ForEach(word => wordCounterPairs.Add(word, 0));
            //Scan();
        }
    }
}