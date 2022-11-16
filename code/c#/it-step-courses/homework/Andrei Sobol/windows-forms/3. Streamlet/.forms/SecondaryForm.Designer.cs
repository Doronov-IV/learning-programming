namespace Streamlet.Forms
{
    partial class SecondaryForm
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
            this.LowerStatusStrip = new System.Windows.Forms.StatusStrip();
            this.MainRichTextBox = new System.Windows.Forms.RichTextBox();
            this.TextMenuStrip = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьКакToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.видToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveAsFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.TextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // LowerStatusStrip
            // 
            this.LowerStatusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.LowerStatusStrip.Location = new System.Drawing.Point(0, 441);
            this.LowerStatusStrip.Name = "LowerStatusStrip";
            this.LowerStatusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            this.LowerStatusStrip.Size = new System.Drawing.Size(1138, 22);
            this.LowerStatusStrip.TabIndex = 0;
            this.LowerStatusStrip.Text = "statusStrip1";
            // 
            // MainRichTextBox
            // 
            this.MainRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainRichTextBox.Location = new System.Drawing.Point(12, 64);
            this.MainRichTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MainRichTextBox.Name = "MainRichTextBox";
            this.MainRichTextBox.Size = new System.Drawing.Size(1114, 375);
            this.MainRichTextBox.TabIndex = 1;
            this.MainRichTextBox.Text = "";
            // 
            // TextMenuStrip
            // 
            this.TextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.TextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.видToolStripMenuItem,
            this.настройкиToolStripMenuItem});
            this.TextMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.TextMenuStrip.Name = "TextMenuStrip";
            this.TextMenuStrip.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.TextMenuStrip.Size = new System.Drawing.Size(1138, 24);
            this.TextMenuStrip.TabIndex = 2;
            this.TextMenuStrip.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.открытьToolStripMenuItem,
            this.сохранитьToolStripMenuItem,
            this.сохранитьКакToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.файлToolStripMenuItem.Text = "File";
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.открытьToolStripMenuItem.Text = "Open";
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.сохранитьToolStripMenuItem.Text = "Save";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.OnSaveFileClick);
            // 
            // сохранитьКакToolStripMenuItem
            // 
            this.сохранитьКакToolStripMenuItem.Name = "сохранитьКакToolStripMenuItem";
            this.сохранитьКакToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.сохранитьКакToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.сохранитьКакToolStripMenuItem.Text = "Save as";
            this.сохранитьКакToolStripMenuItem.Click += new System.EventHandler(this.OnSaveAsClick);
            // 
            // видToolStripMenuItem
            // 
            this.видToolStripMenuItem.Name = "видToolStripMenuItem";
            this.видToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.видToolStripMenuItem.Text = "View";
            // 
            // настройкиToolStripMenuItem
            // 
            this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
            this.настройкиToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.настройкиToolStripMenuItem.Text = "Settings";
            // 
            // SecondaryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1138, 463);
            this.Controls.Add(this.MainRichTextBox);
            this.Controls.Add(this.LowerStatusStrip);
            this.Controls.Add(this.TextMenuStrip);
            this.MainMenuStrip = this.TextMenuStrip;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "SecondaryForm";
            this.Text = "Edit File Text";
            this.Load += new System.EventHandler(this.OnSecondaryFormLoad);
            this.TextMenuStrip.ResumeLayout(false);
            this.TextMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private StatusStrip LowerStatusStrip;
        private RichTextBox MainRichTextBox;
        private MenuStrip TextMenuStrip;
        private ToolStripMenuItem файлToolStripMenuItem;
        private ToolStripMenuItem открытьToolStripMenuItem;
        private ToolStripMenuItem сохранитьToolStripMenuItem;
        private ToolStripMenuItem сохранитьКакToolStripMenuItem;
        private ToolStripMenuItem видToolStripMenuItem;
        private ToolStripMenuItem настройкиToolStripMenuItem;
        private SaveFileDialog SaveAsFileDialog;
    }
}