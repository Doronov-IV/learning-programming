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
            this.LeftListBox = new System.Windows.Forms.ListBox();
            this.RightListBox = new System.Windows.Forms.ListBox();
            this.LeftAddressTextBox = new System.Windows.Forms.TextBox();
            this.RightAddressTextBox = new System.Windows.Forms.TextBox();
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
            this.TopMenuToolStrip.Size = new System.Drawing.Size(1382, 28);
            this.TopMenuToolStrip.TabIndex = 1;
            this.TopMenuToolStrip.Text = "menuStrip2";
            // 
            // FilesToolStripMenuItem
            // 
            this.FilesToolStripMenuItem.Name = "FilesToolStripMenuItem";
            this.FilesToolStripMenuItem.Size = new System.Drawing.Size(70, 24);
            this.FilesToolStripMenuItem.Text = "Файлы";
            // 
            // SelectionToolStripMenuItem
            // 
            this.SelectionToolStripMenuItem.Name = "SelectionToolStripMenuItem";
            this.SelectionToolStripMenuItem.Size = new System.Drawing.Size(101, 24);
            this.SelectionToolStripMenuItem.Text = "Выделение";
            // 
            // ViewToolStripMenuItem
            // 
            this.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem";
            this.ViewToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            this.ViewToolStripMenuItem.Text = "Вид";
            // 
            // SettingsToolStripMenuItem
            // 
            this.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem";
            this.SettingsToolStripMenuItem.Size = new System.Drawing.Size(98, 24);
            this.SettingsToolStripMenuItem.Text = "Настройки";
            // 
            // MiddleToolStrip
            // 
            this.MiddleToolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MiddleToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenTool,
            this.CopyPathTool,
            this.DeleteTool});
            this.MiddleToolStrip.Location = new System.Drawing.Point(0, 28);
            this.MiddleToolStrip.Name = "MiddleToolStrip";
            this.MiddleToolStrip.Size = new System.Drawing.Size(1382, 27);
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
            this.OpenTool.Size = new System.Drawing.Size(29, 24);
            this.OpenTool.Text = "Open";
            // 
            // CopyPathTool
            // 
            this.CopyPathTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CopyPathTool.Image = global::Streamlet.Properties.Resources.CopyPathIcon;
            this.CopyPathTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CopyPathTool.Name = "CopyPathTool";
            this.CopyPathTool.Size = new System.Drawing.Size(29, 24);
            this.CopyPathTool.Text = "Copy Path";
            // 
            // DeleteTool
            // 
            this.DeleteTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DeleteTool.Image = global::Streamlet.Properties.Resources.DeletionIcon;
            this.DeleteTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DeleteTool.Name = "DeleteTool";
            this.DeleteTool.Size = new System.Drawing.Size(29, 24);
            this.DeleteTool.Text = "Delete";
            // 
            // LeftListBox
            // 
            this.LeftListBox.FormattingEnabled = true;
            this.LeftListBox.ItemHeight = 20;
            this.LeftListBox.Location = new System.Drawing.Point(12, 91);
            this.LeftListBox.Name = "LeftListBox";
            this.LeftListBox.Size = new System.Drawing.Size(682, 744);
            this.LeftListBox.TabIndex = 3;
            this.LeftListBox.SelectedIndexChanged += new System.EventHandler(this.LeftListBox_SelectedIndexChanged);
            this.LeftListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OnLeftListBoxMouseDoubleClick);
            // 
            // RightListBox
            // 
            this.RightListBox.FormattingEnabled = true;
            this.RightListBox.ItemHeight = 20;
            this.RightListBox.Location = new System.Drawing.Point(714, 91);
            this.RightListBox.Name = "RightListBox";
            this.RightListBox.Size = new System.Drawing.Size(656, 744);
            this.RightListBox.TabIndex = 4;
            this.RightListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OnRightListBoxMouseDoubleClick);
            // 
            // LeftAddressTextBox
            // 
            this.LeftAddressTextBox.Location = new System.Drawing.Point(12, 58);
            this.LeftAddressTextBox.Name = "LeftAddressTextBox";
            this.LeftAddressTextBox.Size = new System.Drawing.Size(682, 27);
            this.LeftAddressTextBox.TabIndex = 5;
            // 
            // RightAddressTextBox
            // 
            this.RightAddressTextBox.Location = new System.Drawing.Point(714, 58);
            this.RightAddressTextBox.Name = "RightAddressTextBox";
            this.RightAddressTextBox.Size = new System.Drawing.Size(656, 27);
            this.RightAddressTextBox.TabIndex = 6;
            // 
            // PrimaryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1382, 853);
            this.Controls.Add(this.RightAddressTextBox);
            this.Controls.Add(this.LeftAddressTextBox);
            this.Controls.Add(this.RightListBox);
            this.Controls.Add(this.LeftListBox);
            this.Controls.Add(this.MiddleToolStrip);
            this.Controls.Add(this.TopMenuToolStrip);
            this.Name = "PrimaryForm";
            this.Text = "Streamlet";
            this.Load += new System.EventHandler(this.OnPrimaryFormLoad);
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
        private ListBox LeftListBox;
        private ListBox RightListBox;
        private TextBox LeftAddressTextBox;
        private TextBox RightAddressTextBox;
    }
}