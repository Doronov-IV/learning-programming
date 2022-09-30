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
            this.SuspendLayout();
            // 
            // mainTreeView
            // 
            this.mainTreeView.Location = new System.Drawing.Point(33, 22);
            this.mainTreeView.Name = "mainTreeView";
            this.mainTreeView.Size = new System.Drawing.Size(506, 384);
            this.mainTreeView.TabIndex = 0;
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(713, 412);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(75, 23);
            this.searchButton.TabIndex = 1;
            this.searchButton.Text = "Search";
            this.searchButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.OnSearchButtonClickAsync);
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
            this.ShowStatisticsButton.Location = new System.Drawing.Point(623, 412);
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
            // TreeMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.OpenFileButton);
            this.Controls.Add(this.OpenCopyFolderButton);
            this.Controls.Add(this.ShowStatisticsButton);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.mainTreeView);
            this.Name = "TreeMainForm";
            this.Text = "Literally \"1984\"";
            this.ResumeLayout(false);

        }

        #endregion

        private TreeView mainTreeView;
        private Button searchButton;
        private ProgressBar progressBar1;
        private Button ShowStatisticsButton;
        private Button OpenCopyFolderButton;
        private Button OpenFileButton;
    }
}