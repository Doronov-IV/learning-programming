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
            totalStatistics = new List<FileStatistics>();
            wordCounterPairs = new Dictionary<string, int>();
            TopWords = new List<string>();
            TokenSource = new CancellationTokenSource();

            // preparations;
            Scan();
            progressBar1.Maximum = overallFileAmount;
            progressBar1.Step = 1;
            ShowStatisticsButton.Visible = false;
        }



        /// <summary>
        /// Scan all the system;
        /// <br />
        /// Сканировать всю систему;
        /// </summary>
        public void Scan()
        {
            mainTreeView.Nodes.Clear();
            try
            {
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    if (drive.Name != @"C:\")
                    {
                        TreeNode driveNode = new TreeNode(drive.Name);
                        driveNode.Tag = drive;
                        GetChildNode(driveNode, drive.Name);

                        UpdateTreeView(driveNode);
                    }
                }
            }
            catch (Exception) { }
        }



        /// <summary>
        /// Scan directory;
        /// <br />
        /// Сканировать директорию;
        /// </summary>
        private void Scan(DirectoryInfo info)
        {
            try
            {
                foreach (DirectoryInfo dirInfo in info.GetDirectories())
                {
                    overallFileAmount += dirInfo.GetFiles().Length;
                    Scan(dirInfo);
                }
            }
            catch (Exception) { }
        }



        /// <summary>
        /// Update tree view model;
        /// <br />
        /// Обновить модель tree view;
        /// </summary>
        /// <param name="driveNode">
        /// Tree view node;
        /// <br />
        /// Нода "tree view";
        /// </param>
        private void UpdateTreeView(TreeNode driveNode)
        {
            if (mainTreeView.InvokeRequired)
                mainTreeView.Invoke(new Action<TreeNode>(UpdateTreeView), driveNode);
            else
                mainTreeView.Nodes.Add(driveNode);
        }


        /// <summary>
        /// Get child node of the passed one;
        /// <br />
        /// Получить дочернюю ноду для передаваемой ноды;
        /// </summary>
        private void GetChildNode(TreeNode driveNode, string path)
        {
            try
            {
                string[] dirs = Directory.GetDirectories(path);
                string[] files = Directory.GetFiles(path);
                if (dirs.Length == 0 && files.Length == 0) return;


                foreach (string dir in dirs)
                {
                    TreeNode dirNode = new TreeNode();
                    dirNode.Text = dir.Remove(0, dir.LastIndexOf("\\") + 1);
                    dirNode.Tag = dir;

                    GetChildNode(dirNode, dir);
                    driveNode.Nodes.Add(dirNode);
                }

                foreach (string file in files)
                {
                    TreeNode fileNode = new TreeNode();
                    fileNode.Text = file.Remove(0, file.LastIndexOf("\\") + 1);
                    fileNode.Tag = file;
                                        
                    driveNode.Nodes.Add(fileNode);
                }

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
            if (mainTreeView.SelectedNode != null)
            {
                if (forbiddenWords.Count == 0)
                {
                forbiddenWords.AddRange(new string[4] { "a", "the", "in", "at" });
                forbiddenWords.ForEach(word => wordCounterPairs.Add(word, 0));
                }

                TopWords.Clear();
                totalStatistics.Clear();
                overallFileAmount = 0;
                progressBar1.Value = 0;

                Scan(new DirectoryInfo(mainTreeView.SelectedNode.FullPath));
                progressBar1.Maximum = overallFileAmount;

                TreeNode tn = mainTreeView.SelectedNode;
                DirectoryInfo di = new DirectoryInfo(tn.FullPath);

                var token = TokenSource.Token;

                await Task.Run(() => Search(di));

                var topWords = wordCounterPairs.OrderBy(u => u.Value);

                foreach (var number in topWords)
                {
                    if (number.Value != 0) TopWords.Add(number.Key);

                    if (TopWords.Count >= 5)
                        break;
                }
                ShowStatisticsButton.Visible = true;
                CreateStatisticsReportFile();
            }
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
        private void Search(DirectoryInfo di)
        {
            try
            {
                DirectoryInfo[] directories = di.GetDirectories();

                foreach (DirectoryInfo dir in directories)
                    Search(dir);

                if (directories.Count() == 0 || di.GetFiles().Count() > 0)
                {
                    FileInfo[] files = di.GetFiles();

                        foreach (FileInfo file in files)
                        {
                            FileStatistics statistics = new();
                            statistics.FileInfo = file;
                            FileInfo fi = null;
                            bool isFound = false;
                            // читает файл по пути
                            string[] str = System.IO.File.ReadAllLines(file.FullName);

                            foreach (string word in forbiddenWords)
                            {
                                isFound = false;
                                for (int i = 0; i < str.Count(); i++)
                                {
                                    if (Regex.IsMatch(str[i], word, RegexOptions.IgnoreCase & RegexOptions.Compiled))
                                    {
                                    var compareString = str[i];

                                    str[i] = str[i].Replace(" " + word + " ", " ******* ");

                                    if (!compareString.Equals(str[i]))
                                    {
                                        isFound = true;
                                        statistics.Counter++;
                                        wordCounterPairs[word]++;
                                    }
                                    }
                                }
                            }

                            if (isFound)
                            {

                                lock (new object())
                                {
                                    try
                                    { 
                                    System.IO.File.WriteAllLines($"../../../copied-files/{file.Name} - copy{file.Extension}", str, Encoding.UTF8);
                                    }
                                    catch { }
                                }
                                totalStatistics.Add(statistics);
                            }

                            if (progressBar1.InvokeRequired) Invoke(() =>
                            {
                                progressBar1.PerformStep();
                            });


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
            StatisticsForm statistics = new StatisticsForm();
            statistics.TotalStatistics = totalStatistics;
            TopWords.Reverse();
            statistics.TopForbiddenWords = TopWords;
            statistics.Show();
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
            List<string> report = new List<string>();
            totalStatistics.ForEach(str => report.Add("File Name: " + str.FileInfo.Name + ", Path: " + str.FileInfo.FullName + ", replaces: " + str.Counter));
            report.Add("\n\n\n" + "Top popular words: ");
            TopWords.ForEach(word => report.Add("\t" + word));

            System.IO.File.WriteAllLines($"{DriveInfo.GetDrives()[1].RootDirectory}/ProjectReport.txt", report, Encoding.UTF8);
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

            if (fileDialog.ShowDialog() == DialogResult.OK) forbiddenWords = System.IO.File.ReadAllLines(fileDialog.FileName).ToList();
            forbiddenWords.ForEach(word => wordCounterPairs.Add(word, 0));
            Scan();
        }
    }
}