

namespace TrickAndTreat.Forms
{
    partial class SecondaryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SecondaryForm));
            this.AgileButton = new System.Windows.Forms.Button();
            this.PositiveScoreLabel = new System.Windows.Forms.Label();
            this.NegariveScoreLabel = new System.Windows.Forms.Label();
            this.RestartButton = new System.Windows.Forms.Button();
            this.GiveUpButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AgileButton
            // 
            this.AgileButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.AgileButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.AgileButton.Font = new System.Drawing.Font("Consolas", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.AgileButton.Location = new System.Drawing.Point(570, 75);
            this.AgileButton.Margin = new System.Windows.Forms.Padding(0);
            this.AgileButton.Name = "AgileButton";
            this.AgileButton.Size = new System.Drawing.Size(150, 75);
            this.AgileButton.TabIndex = 0;
            this.AgileButton.Text = "Hit me bro";
            this.AgileButton.UseVisualStyleBackColor = false;
            this.AgileButton.Click += new System.EventHandler(this.OnAgileButtonMouseClick);
            this.AgileButton.MouseEnter += new System.EventHandler(this.OnAgileButtonMouseEnter);
            // 
            // PositiveScoreLabel
            // 
            this.PositiveScoreLabel.AutoSize = true;
            this.PositiveScoreLabel.ForeColor = System.Drawing.Color.ForestGreen;
            this.PositiveScoreLabel.Location = new System.Drawing.Point(35, 34);
            this.PositiveScoreLabel.Name = "PositiveScoreLabel";
            this.PositiveScoreLabel.Size = new System.Drawing.Size(104, 20);
            this.PositiveScoreLabel.TabIndex = 1;
            this.PositiveScoreLabel.Text = "PosScoreLabel";
            // 
            // NegariveScoreLabel
            // 
            this.NegariveScoreLabel.AutoSize = true;
            this.NegariveScoreLabel.ForeColor = System.Drawing.Color.Red;
            this.NegariveScoreLabel.Location = new System.Drawing.Point(35, 64);
            this.NegariveScoreLabel.Name = "NegariveScoreLabel";
            this.NegariveScoreLabel.Size = new System.Drawing.Size(110, 20);
            this.NegariveScoreLabel.TabIndex = 2;
            this.NegariveScoreLabel.Text = "NegScoreLabel";
            // 
            // RestartButton
            // 
            this.RestartButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("RestartButton.BackgroundImage")));
            this.RestartButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.RestartButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.RestartButton.Location = new System.Drawing.Point(1206, 21);
            this.RestartButton.Name = "RestartButton";
            this.RestartButton.Size = new System.Drawing.Size(32, 32);
            this.RestartButton.TabIndex = 3;
            this.RestartButton.Text = " ";
            this.RestartButton.UseVisualStyleBackColor = true;
            this.RestartButton.Click += new System.EventHandler(this.OnRestartButtonClick);
            // 
            // GiveUpButton
            // 
            this.GiveUpButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("GiveUpButton.BackgroundImage")));
            this.GiveUpButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.GiveUpButton.Location = new System.Drawing.Point(1167, 21);
            this.GiveUpButton.Name = "GiveUpButton";
            this.GiveUpButton.Size = new System.Drawing.Size(32, 32);
            this.GiveUpButton.TabIndex = 4;
            this.GiveUpButton.UseVisualStyleBackColor = true;
            this.GiveUpButton.Click += new System.EventHandler(this.OnGiveUpButtonClick);
            // 
            // SecondaryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.GiveUpButton);
            this.Controls.Add(this.RestartButton);
            this.Controls.Add(this.NegariveScoreLabel);
            this.Controls.Add(this.PositiveScoreLabel);
            this.Controls.Add(this.AgileButton);
            this.MaximumSize = new System.Drawing.Size(1280, 720);
            this.MinimumSize = new System.Drawing.Size(1280, 720);
            this.Name = "SecondaryForm";
            this.Text = "T.N.T.";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnSecondaryFormFormClosed);
            this.Load += new System.EventHandler(this.OnSecondaryFormLoad);
            this.Click += new System.EventHandler(this.OnSecondaryFormClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button AgileButton;
        private Label PositiveScoreLabel;
        private Label NegariveScoreLabel;
        private Button RestartButton;
        private Button GiveUpButton;
    }
}