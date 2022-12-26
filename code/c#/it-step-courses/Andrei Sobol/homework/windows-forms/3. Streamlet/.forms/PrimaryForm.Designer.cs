namespace Streamlet.Forms
{
    partial class PrimaryForm
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
            this.TopMenuToolStrip = new System.Windows.Forms.MenuStrip();
            this.FilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MiddleToolStrip = new System.Windows.Forms.ToolStrip();
            this.OpenTool = new System.Windows.Forms.ToolStripButton();
            this.CopyPathTool = new System.Windows.Forms.ToolStripButton();
            this.DeleteTool = new System.Windows.Forms.ToolStripButton();
            this.LeftListView = new System.Windows.Forms.ListView();
            this.LeftNameColumn = new System.Windows.Forms.ColumnHeader();
            this.LeftExtColumn = new System.Windows.Forms.ColumnHeader();
            this.LeftSizeColumn = new System.Windows.Forms.ColumnHeader();
            this.LeftDateColumn = new System.Windows.Forms.ColumnHeader();
            this.LeftAddressTextBox = new System.Windows.Forms.TextBox();
            this.RightAddressTextBox = new System.Windows.Forms.TextBox();
            this.RightListView = new System.Windows.Forms.ListView();
            this.RightNameColumn = new System.Windows.Forms.ColumnHeader();
            this.RightExtColumn = new System.Windows.Forms.ColumnHeader();
            this.RightSizeColumn = new System.Windows.Forms.ColumnHeader();
            this.RightDateColumn = new System.Windows.Forms.ColumnHeader();
            this.MoveRightButton = new System.Windows.Forms.Button();
            this.MoveLeftButton = new System.Windows.Forms.Button();
            this.TopMenuToolStrip.SuspendLayout();
            this.MiddleToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // TopMenuToolStrip
            // 
            this.TopMenuToolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.TopMenuToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FilesToolStripMenuItem,
            this.SelectionToolStripMenuItem,
            this.ViewToolStripMenuItem,
            this.SettingsToolStripMenuItem});
            this.TopMenuToolStrip.Location = new System.Drawing.Point(0, 0);
            this.TopMenuToolStrip.Name = "TopMenuToolStrip";
            this.TopMenuToolStrip.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.TopMenuToolStrip.Size = new System.Drawing.Size(1209, 24);
            this.TopMenuToolStrip.TabIndex = 1;
            this.TopMenuToolStrip.Text = "menuStrip2";
            // 
            // FilesToolStripMenuItem
            // 
            this.FilesToolStripMenuItem.Name = "FilesToolStripMenuItem";
            this.FilesToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.FilesToolStripMenuItem.Text = "Файлы";
            // 
            // SelectionToolStripMenuItem
            // 
            this.SelectionToolStripMenuItem.Name = "SelectionToolStripMenuItem";
            this.SelectionToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.SelectionToolStripMenuItem.Text = "Выделение";
            // 
            // ViewToolStripMenuItem
            // 
            this.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem";
            this.ViewToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.ViewToolStripMenuItem.Text = "Вид";
            // 
            // SettingsToolStripMenuItem
            // 
            this.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem";
            this.SettingsToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.SettingsToolStripMenuItem.Text = "Настройки";
            // 
            // MiddleToolStrip
            // 
            this.MiddleToolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MiddleToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenTool,
            this.CopyPathTool,
            this.DeleteTool});
            this.MiddleToolStrip.Location = new System.Drawing.Point(0, 24);
            this.MiddleToolStrip.Name = "MiddleToolStrip";
            this.MiddleToolStrip.Size = new System.Drawing.Size(1209, 27);
            this.MiddleToolStrip.TabIndex = 2;
            this.MiddleToolStrip.Text = "toolStrip1";
            this.MiddleToolStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.MiddleToolStrip_ItemClicked);
            // 
            // OpenTool
            // 
            this.OpenTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.OpenTool.Image = global::Streamlet.Properties.Resources.OpenIcon;
            this.OpenTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenTool.Name = "OpenTool";
            this.OpenTool.Size = new System.Drawing.Size(24, 24);
            this.OpenTool.Text = "Open";
            this.OpenTool.Click += new System.EventHandler(this.OnOpenToolClick);
            // 
            // CopyPathTool
            // 
            this.CopyPathTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CopyPathTool.Image = global::Streamlet.Properties.Resources.CopyPathIcon;
            this.CopyPathTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CopyPathTool.Name = "CopyPathTool";
            this.CopyPathTool.Size = new System.Drawing.Size(24, 24);
            this.CopyPathTool.Text = "Copy Path";
            this.CopyPathTool.Click += new System.EventHandler(this.OnCopyPathToolClick);
            // 
            // DeleteTool
            // 
            this.DeleteTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DeleteTool.Image = global::Streamlet.Properties.Resources.DeletionIcon;
            this.DeleteTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DeleteTool.Name = "DeleteTool";
            this.DeleteTool.Size = new System.Drawing.Size(24, 24);
            this.DeleteTool.Text = "Delete";
            this.DeleteTool.Click += new System.EventHandler(this.OnDeleteToolClick);
            // 
            // LeftListView
            // 
            this.LeftListView.AllowDrop = true;
            this.LeftListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.LeftNameColumn,
            this.LeftExtColumn,
            this.LeftSizeColumn,
            this.LeftDateColumn});
            this.LeftListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LeftListView.Location = new System.Drawing.Point(10, 82);
            this.LeftListView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LeftListView.Name = "LeftListView";
            this.LeftListView.Size = new System.Drawing.Size(569, 549);
            this.LeftListView.TabIndex = 7;
            this.LeftListView.UseCompatibleStateImageBehavior = false;
            this.LeftListView.View = System.Windows.Forms.View.Details;
            this.LeftListView.DoubleClick += new System.EventHandler(this.OnLeftListViewMouseDoubleClick);
            this.LeftListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnPrimaryFormKeyDown);
            // 
            // LeftNameColumn
            // 
            this.LeftNameColumn.Text = "Name";
            this.LeftNameColumn.Width = 200;
            // 
            // LeftExtColumn
            // 
            this.LeftExtColumn.Text = "Ext";
            this.LeftExtColumn.Width = 120;
            // 
            // LeftSizeColumn
            // 
            this.LeftSizeColumn.Text = "Size";
            this.LeftSizeColumn.Width = 120;
            // 
            // LeftDateColumn
            // 
            this.LeftDateColumn.Text = "Date";
            this.LeftDateColumn.Width = 120;
            // 
            // LeftAddressTextBox
            // 
            this.LeftAddressTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LeftAddressTextBox.Location = new System.Drawing.Point(10, 51);
            this.LeftAddressTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LeftAddressTextBox.Name = "LeftAddressTextBox";
            this.LeftAddressTextBox.Size = new System.Drawing.Size(569, 26);
            this.LeftAddressTextBox.TabIndex = 5;
            this.LeftAddressTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnLeftAddressTextBoxKeyDown);
            this.LeftAddressTextBox.Leave += new System.EventHandler(this.OnLeftAddressTextBoxLeave);
            // 
            // RightAddressTextBox
            // 
            this.RightAddressTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RightAddressTextBox.Location = new System.Drawing.Point(652, 51);
            this.RightAddressTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RightAddressTextBox.Name = "RightAddressTextBox";
            this.RightAddressTextBox.Size = new System.Drawing.Size(547, 26);
            this.RightAddressTextBox.TabIndex = 6;
            this.RightAddressTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnRightAddressTextBoxKeyDown);
            this.RightAddressTextBox.Leave += new System.EventHandler(this.OnRightAddressTextBoxLeave);
            // 
            // RightListView
            // 
            this.RightListView.AllowDrop = true;
            this.RightListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.RightNameColumn,
            this.RightExtColumn,
            this.RightSizeColumn,
            this.RightDateColumn});
            this.RightListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RightListView.Location = new System.Drawing.Point(652, 82);
            this.RightListView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RightListView.Name = "RightListView";
            this.RightListView.Size = new System.Drawing.Size(547, 549);
            this.RightListView.TabIndex = 8;
            this.RightListView.UseCompatibleStateImageBehavior = false;
            this.RightListView.View = System.Windows.Forms.View.Details;
            this.RightListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnPrimaryFormKeyDown);
            this.RightListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OnRightListViewMouseDoubleClick);
            // 
            // RightNameColumn
            // 
            this.RightNameColumn.Text = "Name";
            this.RightNameColumn.Width = 200;
            // 
            // RightExtColumn
            // 
            this.RightExtColumn.Text = "Ext";
            this.RightExtColumn.Width = 120;
            // 
            // RightSizeColumn
            // 
            this.RightSizeColumn.Text = "Size";
            this.RightSizeColumn.Width = 120;
            // 
            // RightDateColumn
            // 
            this.RightDateColumn.Text = "Date";
            this.RightDateColumn.Width = 120;
            // 
            // MoveRightButton
            // 
            this.MoveRightButton.Image = global::Streamlet.Properties.Resources.RightMoveIcon;
            this.MoveRightButton.Location = new System.Drawing.Point(597, 302);
            this.MoveRightButton.Name = "MoveRightButton";
            this.MoveRightButton.Size = new System.Drawing.Size(35, 30);
            this.MoveRightButton.TabIndex = 9;
            this.MoveRightButton.UseVisualStyleBackColor = true;
            this.MoveRightButton.Click += new System.EventHandler(this.OnCopyRightButtonClick);
            // 
            // MoveLeftButton
            // 
            this.MoveLeftButton.Image = global::Streamlet.Properties.Resources.LeftMoveIcon;
            this.MoveLeftButton.Location = new System.Drawing.Point(597, 356);
            this.MoveLeftButton.Name = "MoveLeftButton";
            this.MoveLeftButton.Size = new System.Drawing.Size(35, 30);
            this.MoveLeftButton.TabIndex = 10;
            this.MoveLeftButton.UseVisualStyleBackColor = true;
            this.MoveLeftButton.Click += new System.EventHandler(this.OnCopyLeftButtonClick);
            // 
            // PrimaryForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1209, 640);
            this.Controls.Add(this.MoveLeftButton);
            this.Controls.Add(this.MoveRightButton);
            this.Controls.Add(this.RightListView);
            this.Controls.Add(this.RightAddressTextBox);
            this.Controls.Add(this.LeftAddressTextBox);
            this.Controls.Add(this.LeftListView);
            this.Controls.Add(this.MiddleToolStrip);
            this.Controls.Add(this.TopMenuToolStrip);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "PrimaryForm";
            this.Text = "Streamlet";
            this.Load += new System.EventHandler(this.OnPrimaryFormLoad);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnPrimaryFormKeyDown);
            this.TopMenuToolStrip.ResumeLayout(false);
            this.TopMenuToolStrip.PerformLayout();
            this.MiddleToolStrip.ResumeLayout(false);
            this.MiddleToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip TopMenuToolStrip;
        private ToolStripMenuItem FilesToolStripMenuItem;
        private ToolStripMenuItem SelectionToolStripMenuItem;
        private ToolStripMenuItem ViewToolStripMenuItem;
        private ToolStripMenuItem SettingsToolStripMenuItem;
        private ToolStrip MiddleToolStrip;
        private ToolStripButton OpenTool;
        private ToolStripButton CopyPathTool;
        private ToolStripButton DeleteTool;
        private TextBox LeftAddressTextBox;
        private TextBox RightAddressTextBox;
        private ListView LeftListView;
        private ColumnHeader name;
        private ColumnHeader ext;
        private ColumnHeader size;
        private ColumnHeader date;
        private ColumnHeader LeftNameColumn;
        private ColumnHeader LeftExtColumn;
        private ColumnHeader LeftSizeColumn;
        private ColumnHeader LeftDateColumn;
        private ListView RightListView;
        private ColumnHeader RightNameColumn;
        private ColumnHeader RightExtColumn;
        private ColumnHeader RightSizeColumn;
        private ColumnHeader RightDateColumn;
        private Button MoveRightButton;
        private Button MoveLeftButton;
    }
}