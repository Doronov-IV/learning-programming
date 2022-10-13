namespace TrickAndTreat.Forms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrimaryForm));
            this.EasyButton = new System.Windows.Forms.Button();
            this.MediumButton = new System.Windows.Forms.Button();
            this.HardButton = new System.Windows.Forms.Button();
            this.InsaneButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // EasyButton
            // 
            this.EasyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.EasyButton.Location = new System.Drawing.Point(440, 100);
            this.EasyButton.Name = "EasyButton";
            this.EasyButton.Size = new System.Drawing.Size(400, 75);
            this.EasyButton.TabIndex = 0;
            this.EasyButton.Text = "Easy";
            this.EasyButton.UseVisualStyleBackColor = true;
            this.EasyButton.Click += new System.EventHandler(this.OnEasyButtonClick);
            // 
            // MediumButton
            // 
            this.MediumButton.Location = new System.Drawing.Point(440, 233);
            this.MediumButton.Name = "MediumButton";
            this.MediumButton.Size = new System.Drawing.Size(400, 75);
            this.MediumButton.TabIndex = 1;
            this.MediumButton.Text = "Medium";
            this.MediumButton.UseVisualStyleBackColor = true;
            this.MediumButton.Click += new System.EventHandler(this.OnMediumButtonClick);
            // 
            // HardButton
            // 
            this.HardButton.Location = new System.Drawing.Point(440, 366);
            this.HardButton.Name = "HardButton";
            this.HardButton.Size = new System.Drawing.Size(400, 75);
            this.HardButton.TabIndex = 2;
            this.HardButton.Text = "Hard";
            this.HardButton.UseVisualStyleBackColor = true;
            this.HardButton.Click += new System.EventHandler(this.OnHardButtonClick);
            // 
            // InsaneButton
            // 
            this.InsaneButton.Location = new System.Drawing.Point(440, 500);
            this.InsaneButton.Name = "InsaneButton";
            this.InsaneButton.Size = new System.Drawing.Size(400, 75);
            this.InsaneButton.TabIndex = 3;
            this.InsaneButton.Text = "Insane";
            this.InsaneButton.UseVisualStyleBackColor = true;
            this.InsaneButton.Click += new System.EventHandler(this.OnInsaneButtonClick);
            // 
            // PrimaryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.InsaneButton);
            this.Controls.Add(this.HardButton);
            this.Controls.Add(this.MediumButton);
            this.Controls.Add(this.EasyButton);
            this.MaximumSize = new System.Drawing.Size(1280, 720);
            this.MinimumSize = new System.Drawing.Size(1280, 720);
            this.Name = "PrimaryForm";
            this.Text = "T.N.T.";
            this.Load += new System.EventHandler(this.OnPrimaryFormLoad);
            this.ResumeLayout(false);

        }

        #endregion

        private Button EasyButton;
        private Button MediumButton;
        private Button HardButton;
        private Button InsaneButton;
    }
}