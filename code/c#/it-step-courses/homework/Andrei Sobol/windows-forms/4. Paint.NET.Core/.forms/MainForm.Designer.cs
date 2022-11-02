namespace Paint.NET.Core.Forms
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MainPictureBox = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.ColorButton = new System.Windows.Forms.Button();
            this.MainColorDialog = new System.Windows.Forms.ColorDialog();
            this.MainOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.MainSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.FigureListView = new System.Windows.Forms.ListView();
            this.StyloListView = new System.Windows.Forms.ListView();
            this.RepeatActionButton = new System.Windows.Forms.Button();
            this.CancellActionButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.MainPictureBox)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainPictureBox
            // 
            resources.ApplyResources(this.MainPictureBox, "MainPictureBox");
            this.MainPictureBox.Name = "MainPictureBox";
            this.MainPictureBox.TabStop = false;
            this.MainPictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.OnMainPictureBoxPaint);
            this.MainPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnImageBoxMouseDown);
            this.MainPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnImageBoxMouseMove);
            this.MainPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnImageBoxMouseUp);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.optionsToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            resources.ApplyResources(this.openToolStripMenuItem, "openToolStripMenuItem");
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OnOpenFileButtonClick);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            resources.ApplyResources(this.saveToolStripMenuItem, "saveToolStripMenuItem");
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.OnSaveButtonClick);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            resources.ApplyResources(this.saveAsToolStripMenuItem, "saveAsToolStripMenuItem");
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.OnSaveAsFileButtonClick);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            resources.ApplyResources(this.viewToolStripMenuItem, "viewToolStripMenuItem");
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            resources.ApplyResources(this.optionsToolStripMenuItem, "optionsToolStripMenuItem");
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButton1, "toolStripButton1");
            this.toolStripButton1.Name = "toolStripButton1";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButton2, "toolStripButton2");
            this.toolStripButton2.Name = "toolStripButton2";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButton3, "toolStripButton3");
            this.toolStripButton3.Name = "toolStripButton3";
            // 
            // ColorButton
            // 
            this.ColorButton.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.ColorButton, "ColorButton");
            this.ColorButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.ColorButton.FlatAppearance.BorderSize = 0;
            this.ColorButton.Image = global::Paint.NET.Core.Properties.Resources.color_palette3;
            this.ColorButton.Name = "ColorButton";
            this.ColorButton.UseVisualStyleBackColor = false;
            this.ColorButton.Click += new System.EventHandler(this.OnColorButtonClick);
            // 
            // FigureListView
            // 
            resources.ApplyResources(this.FigureListView, "FigureListView");
            this.FigureListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("FigureListView.Items")))});
            this.FigureListView.MultiSelect = false;
            this.FigureListView.Name = "FigureListView";
            this.FigureListView.TileSize = new System.Drawing.Size(20, 20);
            this.FigureListView.UseCompatibleStateImageBehavior = false;
            this.FigureListView.SelectedIndexChanged += new System.EventHandler(this.OnFiguresListViewSelectedIndexChanged);
            // 
            // StyloListView
            // 
            resources.ApplyResources(this.StyloListView, "StyloListView");
            this.StyloListView.MultiSelect = false;
            this.StyloListView.Name = "StyloListView";
            this.StyloListView.UseCompatibleStateImageBehavior = false;
            this.StyloListView.SelectedIndexChanged += new System.EventHandler(this.OnStyloListViewSelectedIndexChanged);
            // 
            // RepeatActionButton
            // 
            resources.ApplyResources(this.RepeatActionButton, "RepeatActionButton");
            this.RepeatActionButton.Name = "RepeatActionButton";
            this.RepeatActionButton.UseVisualStyleBackColor = true;
            this.RepeatActionButton.Click += new System.EventHandler(this.OnRepeatActionButtonClick);
            // 
            // CancellActionButton
            // 
            resources.ApplyResources(this.CancellActionButton, "CancellActionButton");
            this.CancellActionButton.Name = "CancellActionButton";
            this.CancellActionButton.UseVisualStyleBackColor = true;
            this.CancellActionButton.Click += new System.EventHandler(this.OnCancellActionButtonClick);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Controls.Add(this.CancellActionButton);
            this.Controls.Add(this.RepeatActionButton);
            this.Controls.Add(this.StyloListView);
            this.Controls.Add(this.FigureListView);
            this.Controls.Add(this.ColorButton);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.MainPictureBox);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnMainFormFormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnMainFormKeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.MainPictureBox)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox MainPictureBox;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButton1;
        private ToolStripButton toolStripButton2;
        private ToolStripButton toolStripButton3;
        private Button ColorButton;
        private ColorDialog MainColorDialog;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private OpenFileDialog MainOpenFileDialog;
        private SaveFileDialog MainSaveFileDialog;
        private ListView FigureListView;
        private ListView StyloListView;
        private Button RepeatActionButton;
        private Button CancellActionButton;
    }
}