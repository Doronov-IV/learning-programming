namespace Doronov.ConcurrencyExam.Forms
{
    partial class TreeMainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainTreeView = new System.Windows.Forms.TreeView();
            this.SearchButton = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.ShowStatisticsButton = new System.Windows.Forms.Button();
            this.OpenCopyFolderButton = new System.Windows.Forms.Button();
            this.OpenFileButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.StopButton = new System.Windows.Forms.Button();
            this.ScanButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mainTreeView
            // 
            this.mainTreeView.Location = new System.Drawing.Point(33, 22);
            this.mainTreeView.Name = "mainTreeView";
            this.mainTreeView.Size = new System.Drawing.Size(506, 384);
            this.mainTreeView.TabIndex = 0;
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(713, 412);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(75, 23);
            this.SearchButton.TabIndex = 1;
            this.SearchButton.Text = "Search";
            this.SearchButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.OnSearchButtonClickAsync);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(33, 412);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(506, 26);
            this.progressBar1.TabIndex = 3;
            // 
            // ShowStatisticsButton
            // 
            this.ShowStatisticsButton.Location = new System.Drawing.Point(713, 79);
            this.ShowStatisticsButton.Name = "ShowStatisticsButton";
            this.ShowStatisticsButton.Size = new System.Drawing.Size(75, 23);
            this.ShowStatisticsButton.TabIndex = 4;
            this.ShowStatisticsButton.Text = "Statistics";
            this.ShowStatisticsButton.UseVisualStyleBackColor = true;
            this.ShowStatisticsButton.Click += new System.EventHandler(this.OnStatisticsButtonClick);
            // 
            // OpenCopyFolderButton
            // 
            this.OpenCopyFolderButton.Location = new System.Drawing.Point(672, 22);
            this.OpenCopyFolderButton.Name = "OpenCopyFolderButton";
            this.OpenCopyFolderButton.Size = new System.Drawing.Size(116, 22);
            this.OpenCopyFolderButton.TabIndex = 6;
            this.OpenCopyFolderButton.Text = "Open Copy Folder";
            this.OpenCopyFolderButton.UseVisualStyleBackColor = true;
            this.OpenCopyFolderButton.Click += new System.EventHandler(this.OnOpenCopyFolderButtonClick);
            // 
            // OpenFileButton
            // 
            this.OpenFileButton.Location = new System.Drawing.Point(672, 50);
            this.OpenFileButton.Name = "OpenFileButton";
            this.OpenFileButton.Size = new System.Drawing.Size(116, 23);
            this.OpenFileButton.TabIndex = 7;
            this.OpenFileButton.Text = "Open Filter File";
            this.OpenFileButton.UseVisualStyleBackColor = true;
            this.OpenFileButton.Click += new System.EventHandler(this.OnOpenFileButtonClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(610, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "label1";
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(713, 383);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(75, 23);
            this.StopButton.TabIndex = 9;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.OnStopButtonClick);
            // 
            // ScanButton
            // 
            this.ScanButton.Location = new System.Drawing.Point(632, 412);
            this.ScanButton.Name = "ScanButton";
            this.ScanButton.Size = new System.Drawing.Size(75, 23);
            this.ScanButton.TabIndex = 10;
            this.ScanButton.Text = "Scan";
            this.ScanButton.UseVisualStyleBackColor = true;
            this.ScanButton.Click += new System.EventHandler(this.OnScanButtonClickAsync);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(545, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "label2";
            // 
            // TreeMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ScanButton);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.OpenFileButton);
            this.Controls.Add(this.OpenCopyFolderButton);
            this.Controls.Add(this.ShowStatisticsButton);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.mainTreeView);
            this.Name = "TreeMainForm";
            this.Text = "Literally \"1984\"";
            this.Click += new System.EventHandler(this.OnStopButtonClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TreeView mainTreeView;
        private Button SearchButton;
        private ProgressBar progressBar1;
        private Button ShowStatisticsButton;
        private Button OpenCopyFolderButton;
        private Button OpenFileButton;
        private Label label1;
        private Button StopButton;
        private Button ScanButton;
        private Label label2;
    }
}