namespace SPExam
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
            this.searchButton = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.ShowStatisticsButton = new System.Windows.Forms.Button();
            this.OpenCopyFolderButton = new System.Windows.Forms.Button();
            this.OpenFileButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mainTreeView
            // 
            this.mainTreeView.Location = new System.Drawing.Point(38, 29);
            this.mainTreeView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.mainTreeView.Name = "mainTreeView";
            this.mainTreeView.Size = new System.Drawing.Size(578, 511);
            this.mainTreeView.TabIndex = 0;
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(815, 549);
            this.searchButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(86, 31);
            this.searchButton.TabIndex = 1;
            this.searchButton.Text = "Search";
            this.searchButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.OnSearchButtonClickAsync);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(38, 549);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(578, 35);
            this.progressBar1.TabIndex = 3;
            // 
            // ShowStatisticsButton
            // 
            this.ShowStatisticsButton.Location = new System.Drawing.Point(712, 549);
            this.ShowStatisticsButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ShowStatisticsButton.Name = "ShowStatisticsButton";
            this.ShowStatisticsButton.Size = new System.Drawing.Size(86, 31);
            this.ShowStatisticsButton.TabIndex = 4;
            this.ShowStatisticsButton.Text = "Statistics";
            this.ShowStatisticsButton.UseVisualStyleBackColor = true;
            this.ShowStatisticsButton.Click += new System.EventHandler(this.OnStatisticsButtonClick);
            // 
            // OpenCopyFolderButton
            // 
            this.OpenCopyFolderButton.Location = new System.Drawing.Point(768, 29);
            this.OpenCopyFolderButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.OpenCopyFolderButton.Name = "OpenCopyFolderButton";
            this.OpenCopyFolderButton.Size = new System.Drawing.Size(133, 29);
            this.OpenCopyFolderButton.TabIndex = 6;
            this.OpenCopyFolderButton.Text = "Open Copy Folder";
            this.OpenCopyFolderButton.UseVisualStyleBackColor = true;
            this.OpenCopyFolderButton.Click += new System.EventHandler(this.OnOpenCopyFolderButtonClick);
            // 
            // OpenFileButton
            // 
            this.OpenFileButton.Location = new System.Drawing.Point(768, 67);
            this.OpenFileButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.OpenFileButton.Name = "OpenFileButton";
            this.OpenFileButton.Size = new System.Drawing.Size(133, 31);
            this.OpenFileButton.TabIndex = 7;
            this.OpenFileButton.Text = "Open Filter File";
            this.OpenFileButton.UseVisualStyleBackColor = true;
            this.OpenFileButton.Click += new System.EventHandler(this.OnOpenFileButtonClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(635, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "label1";
            // 
            // TreeMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 600);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.OpenFileButton);
            this.Controls.Add(this.OpenCopyFolderButton);
            this.Controls.Add(this.ShowStatisticsButton);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.mainTreeView);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "TreeMainForm";
            this.Text = "Literally \"1984\"";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TreeView mainTreeView;
        private Button searchButton;
        private ProgressBar progressBar1;
        private Button ShowStatisticsButton;
        private Button OpenCopyFolderButton;
        private Button OpenFileButton;
        private Label label1;
    }
}