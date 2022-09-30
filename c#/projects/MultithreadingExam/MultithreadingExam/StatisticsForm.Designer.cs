namespace SPExam
{
    partial class StatisticsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.CloseButton = new System.Windows.Forms.Button();
            this.FileListView = new System.Windows.Forms.ListView();
            this.NameColumn = new System.Windows.Forms.ColumnHeader();
            this.ReplacementCounterColumn = new System.Windows.Forms.ColumnHeader();
            this.SizeColumn = new System.Windows.Forms.ColumnHeader();
            this.PathColumn = new System.Windows.Forms.ColumnHeader();
            this.TopWordsListBox = new System.Windows.Forms.ListBox();
            this.TopWordsLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(713, 415);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 0;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.OnCloseButtonClick);
            // 
            // FileListView
            // 
            this.FileListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NameColumn,
            this.ReplacementCounterColumn,
            this.SizeColumn,
            this.PathColumn});
            this.FileListView.Location = new System.Drawing.Point(12, 12);
            this.FileListView.Name = "FileListView";
            this.FileListView.Size = new System.Drawing.Size(506, 390);
            this.FileListView.TabIndex = 1;
            this.FileListView.UseCompatibleStateImageBehavior = false;
            this.FileListView.View = System.Windows.Forms.View.Details;
            // 
            // NameColumn
            // 
            this.NameColumn.Text = "Name";
            this.NameColumn.Width = 100;
            // 
            // ReplacementCounterColumn
            // 
            this.ReplacementCounterColumn.Text = "Replacements";
            this.ReplacementCounterColumn.Width = 100;
            // 
            // SizeColumn
            // 
            this.SizeColumn.Text = "Size, bit";
            this.SizeColumn.Width = 120;
            // 
            // PathColumn
            // 
            this.PathColumn.Text = "Path";
            this.PathColumn.Width = 180;
            // 
            // TopWordsListBox
            // 
            this.TopWordsListBox.FormattingEnabled = true;
            this.TopWordsListBox.ItemHeight = 15;
            this.TopWordsListBox.Location = new System.Drawing.Point(554, 49);
            this.TopWordsListBox.Name = "TopWordsListBox";
            this.TopWordsListBox.Size = new System.Drawing.Size(234, 349);
            this.TopWordsListBox.TabIndex = 2;
            // 
            // TopWordsLabel
            // 
            this.TopWordsLabel.AutoSize = true;
            this.TopWordsLabel.Location = new System.Drawing.Point(606, 12);
            this.TopWordsLabel.Name = "TopWordsLabel";
            this.TopWordsLabel.Size = new System.Drawing.Size(120, 15);
            this.TopWordsLabel.TabIndex = 3;
            this.TopWordsLabel.Text = "Top Forbidden Words";
            // 
            // StatisticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.TopWordsLabel);
            this.Controls.Add(this.TopWordsListBox);
            this.Controls.Add(this.FileListView);
            this.Controls.Add(this.CloseButton);
            this.Name = "StatisticsForm";
            this.Text = "StatisticsForm";
            this.Load += new System.EventHandler(this.OnStatisticsFormLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button CloseButton;
        private ListView FileListView;
        private ColumnHeader NameColumn;
        private ColumnHeader PathColumn;
        private ColumnHeader SizeColumn;
        private ColumnHeader ReplacementCounterColumn;
        private ListBox TopWordsListBox;
        private Label TopWordsLabel;
    }
}