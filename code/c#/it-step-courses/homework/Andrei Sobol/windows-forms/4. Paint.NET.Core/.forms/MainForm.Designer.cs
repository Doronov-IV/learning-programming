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
            this.TopMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.forwardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ColorButton = new System.Windows.Forms.Button();
            this.MainColorDialog = new System.Windows.Forms.ColorDialog();
            this.MainOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.MainSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.FigureListView = new System.Windows.Forms.ListView();
            this.StyloListView = new System.Windows.Forms.ListView();
            this.RepeatActionButton = new System.Windows.Forms.Button();
            this.CancellActionButton = new System.Windows.Forms.Button();
            this.LineWidthUpDown = new System.Windows.Forms.NumericUpDown();
            this.TextFontButton = new System.Windows.Forms.Button();
            this.DrawStringTextBox = new System.Windows.Forms.TextBox();
            this.MainFontDialog = new System.Windows.Forms.FontDialog();
            ((System.ComponentModel.ISupportInitialize)(this.MainPictureBox)).BeginInit();
            this.TopMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LineWidthUpDown)).BeginInit();
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
            // TopMenuStrip
            // 
            this.TopMenuStrip.BackColor = System.Drawing.SystemColors.Menu;
            this.TopMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.optionsToolStripMenuItem});
            resources.ApplyResources(this.TopMenuStrip, "TopMenuStrip");
            this.TopMenuStrip.Name = "TopMenuStrip";
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
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backToolStripMenuItem,
            this.forwardToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            resources.ApplyResources(this.viewToolStripMenuItem, "viewToolStripMenuItem");
            // 
            // backToolStripMenuItem
            // 
            this.backToolStripMenuItem.Name = "backToolStripMenuItem";
            resources.ApplyResources(this.backToolStripMenuItem, "backToolStripMenuItem");
            this.backToolStripMenuItem.Click += new System.EventHandler(this.OnCancellActionButtonClick);
            // 
            // forwardToolStripMenuItem
            // 
            this.forwardToolStripMenuItem.Name = "forwardToolStripMenuItem";
            resources.ApplyResources(this.forwardToolStripMenuItem, "forwardToolStripMenuItem");
            this.forwardToolStripMenuItem.Click += new System.EventHandler(this.OnRepeatActionButtonClick);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            resources.ApplyResources(this.deleteToolStripMenuItem, "deleteToolStripMenuItem");
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.OnDeleteButtonClick);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            resources.ApplyResources(this.optionsToolStripMenuItem, "optionsToolStripMenuItem");
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
            // LineWidthUpDown
            // 
            resources.ApplyResources(this.LineWidthUpDown, "LineWidthUpDown");
            this.LineWidthUpDown.Name = "LineWidthUpDown";
            this.LineWidthUpDown.ValueChanged += new System.EventHandler(this.OnLineWidthUpDownValueChanged);
            // 
            // TextFontButton
            // 
            resources.ApplyResources(this.TextFontButton, "TextFontButton");
            this.TextFontButton.Name = "TextFontButton";
            this.TextFontButton.UseVisualStyleBackColor = true;
            this.TextFontButton.Click += new System.EventHandler(this.OnFontButtonClick);
            // 
            // DrawStringTextBox
            // 
            resources.ApplyResources(this.DrawStringTextBox, "DrawStringTextBox");
            this.DrawStringTextBox.Name = "DrawStringTextBox";
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Controls.Add(this.DrawStringTextBox);
            this.Controls.Add(this.TextFontButton);
            this.Controls.Add(this.LineWidthUpDown);
            this.Controls.Add(this.CancellActionButton);
            this.Controls.Add(this.RepeatActionButton);
            this.Controls.Add(this.StyloListView);
            this.Controls.Add(this.FigureListView);
            this.Controls.Add(this.ColorButton);
            this.Controls.Add(this.MainPictureBox);
            this.Controls.Add(this.TopMenuStrip);
            this.KeyPreview = true;
            this.MainMenuStrip = this.TopMenuStrip;
            this.Name = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnMainFormFormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.MainPictureBox)).EndInit();
            this.TopMenuStrip.ResumeLayout(false);
            this.TopMenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LineWidthUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox MainPictureBox;
        private MenuStrip TopMenuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem optionsToolStripMenuItem;
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
        private ToolStripMenuItem backToolStripMenuItem;
        private ToolStripMenuItem forwardToolStripMenuItem;
        private NumericUpDown LineWidthUpDown;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private Button TextFontButton;
        private TextBox DrawStringTextBox;
        private FontDialog MainFontDialog;
    }
}