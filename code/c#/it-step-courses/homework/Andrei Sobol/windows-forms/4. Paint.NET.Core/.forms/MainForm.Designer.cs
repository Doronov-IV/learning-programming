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
            this.MainMenuToolStrip = new System.Windows.Forms.ToolStrip();
            this.ShowInstrumentsButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.InstrumentsListView = new System.Windows.Forms.ListView();
            this.MainMenuToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenuToolStrip
            // 
            this.MainMenuToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowInstrumentsButton,
            this.toolStripButton2,
            this.toolStripButton3});
            resources.ApplyResources(this.MainMenuToolStrip, "MainMenuToolStrip");
            this.MainMenuToolStrip.Name = "MainMenuToolStrip";
            // 
            // ShowInstrumentsButton
            // 
            this.ShowInstrumentsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.ShowInstrumentsButton, "ShowInstrumentsButton");
            this.ShowInstrumentsButton.Name = "ShowInstrumentsButton";
            this.ShowInstrumentsButton.Click += new System.EventHandler(this.OnShowInstrumentsButtonClick);
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
            // InstrumentsListView
            // 
            resources.ApplyResources(this.InstrumentsListView, "InstrumentsListView");
            this.InstrumentsListView.Name = "InstrumentsListView";
            this.InstrumentsListView.UseCompatibleStateImageBehavior = false;
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.InstrumentsListView);
            this.Controls.Add(this.MainMenuToolStrip);
            this.Name = "MainForm";
            this.MainMenuToolStrip.ResumeLayout(false);
            this.MainMenuToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolStrip MainMenuToolStrip;
        private ToolStripButton ShowInstrumentsButton;
        private ToolStripButton toolStripButton2;
        private ToolStripButton toolStripButton3;
        private ListView InstrumentsListView;
    }
}