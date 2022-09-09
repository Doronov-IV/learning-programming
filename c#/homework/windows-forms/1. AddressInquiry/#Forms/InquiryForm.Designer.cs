namespace AddressInquiry.Forms
{
    partial class InquiryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InquiryForm));
            this.AddressPanel = new System.Windows.Forms.Panel();
            this.MainPictureBox = new System.Windows.Forms.PictureBox();
            this.Ipv4Label = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.CityLabel = new System.Windows.Forms.Label();
            this.CountryLabel = new System.Windows.Forms.Label();
            this.AddressTextBox = new System.Windows.Forms.TextBox();
            this.ButtonsPanel = new System.Windows.Forms.Panel();
            this.MainButton = new System.Windows.Forms.Button();
            this.ImagePanel = new System.Windows.Forms.Panel();
            this.ExampleLabel = new System.Windows.Forms.Label();
            this.ExampleRadioButton3 = new System.Windows.Forms.RadioButton();
            this.ExampleRadioButton2 = new System.Windows.Forms.RadioButton();
            this.ExampleRadioButton1 = new System.Windows.Forms.RadioButton();
            this.AddressPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.ButtonsPanel.SuspendLayout();
            this.ImagePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // AddressPanel
            // 
            this.AddressPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AddressPanel.Controls.Add(this.MainPictureBox);
            this.AddressPanel.Controls.Add(this.Ipv4Label);
            this.AddressPanel.Controls.Add(this.pictureBox1);
            this.AddressPanel.Controls.Add(this.CityLabel);
            this.AddressPanel.Controls.Add(this.CountryLabel);
            this.AddressPanel.Controls.Add(this.AddressTextBox);
            this.AddressPanel.Location = new System.Drawing.Point(194, 18);
            this.AddressPanel.Name = "AddressPanel";
            this.AddressPanel.Size = new System.Drawing.Size(246, 245);
            this.AddressPanel.TabIndex = 0;
            this.AddressPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.AddressPanel_Paint);
            // 
            // MainPictureBox
            // 
            this.MainPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("MainPictureBox.Image")));
            this.MainPictureBox.Location = new System.Drawing.Point(13, 3);
            this.MainPictureBox.Name = "MainPictureBox";
            this.MainPictureBox.Size = new System.Drawing.Size(52, 60);
            this.MainPictureBox.TabIndex = 5;
            this.MainPictureBox.TabStop = false;
            // 
            // Ipv4Label
            // 
            this.Ipv4Label.AutoSize = true;
            this.Ipv4Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Ipv4Label.Location = new System.Drawing.Point(53, 162);
            this.Ipv4Label.Name = "Ipv4Label";
            this.Ipv4Label.Size = new System.Drawing.Size(40, 18);
            this.Ipv4Label.TabIndex = 4;
            this.Ipv4Label.Text = "IPv4:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(76, 60);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // CityLabel
            // 
            this.CityLabel.AutoSize = true;
            this.CityLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CityLabel.Location = new System.Drawing.Point(13, 120);
            this.CityLabel.Name = "CityLabel";
            this.CityLabel.Size = new System.Drawing.Size(37, 18);
            this.CityLabel.TabIndex = 2;
            this.CityLabel.Text = "City:";
            this.CityLabel.Visible = false;
            // 
            // CountryLabel
            // 
            this.CountryLabel.AutoSize = true;
            this.CountryLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CountryLabel.Location = new System.Drawing.Point(13, 89);
            this.CountryLabel.Name = "CountryLabel";
            this.CountryLabel.Size = new System.Drawing.Size(64, 18);
            this.CountryLabel.TabIndex = 1;
            this.CountryLabel.Text = "Country:";
            this.CountryLabel.Visible = false;
            // 
            // AddressTextBox
            // 
            this.AddressTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AddressTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.AddressTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.AddressTextBox.Location = new System.Drawing.Point(53, 197);
            this.AddressTextBox.MaxLength = 20;
            this.AddressTextBox.Name = "AddressTextBox";
            this.AddressTextBox.Size = new System.Drawing.Size(162, 24);
            this.AddressTextBox.TabIndex = 0;
            this.AddressTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.AddressTextBox.TextChanged += new System.EventHandler(this.OnAddressBoxTextChanged);
            // 
            // ButtonsPanel
            // 
            this.ButtonsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonsPanel.Controls.Add(this.MainButton);
            this.ButtonsPanel.Location = new System.Drawing.Point(294, 269);
            this.ButtonsPanel.Name = "ButtonsPanel";
            this.ButtonsPanel.Size = new System.Drawing.Size(146, 57);
            this.ButtonsPanel.TabIndex = 1;
            // 
            // MainButton
            // 
            this.MainButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.MainButton.Location = new System.Drawing.Point(5, 15);
            this.MainButton.Name = "MainButton";
            this.MainButton.Size = new System.Drawing.Size(110, 30);
            this.MainButton.TabIndex = 0;
            this.MainButton.Text = "Check";
            this.MainButton.UseVisualStyleBackColor = true;
            this.MainButton.Click += new System.EventHandler(this.OnMainButtonClick);
            // 
            // ImagePanel
            // 
            this.ImagePanel.Controls.Add(this.ExampleLabel);
            this.ImagePanel.Controls.Add(this.ExampleRadioButton3);
            this.ImagePanel.Controls.Add(this.ExampleRadioButton2);
            this.ImagePanel.Controls.Add(this.ExampleRadioButton1);
            this.ImagePanel.Location = new System.Drawing.Point(12, 18);
            this.ImagePanel.Name = "ImagePanel";
            this.ImagePanel.Size = new System.Drawing.Size(178, 245);
            this.ImagePanel.TabIndex = 2;
            this.ImagePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.ImagePanel_Paint);
            // 
            // ExampleLabel
            // 
            this.ExampleLabel.AutoSize = true;
            this.ExampleLabel.Location = new System.Drawing.Point(12, 87);
            this.ExampleLabel.Name = "ExampleLabel";
            this.ExampleLabel.Size = new System.Drawing.Size(75, 20);
            this.ExampleLabel.TabIndex = 6;
            this.ExampleLabel.Text = "Examples:";
            // 
            // ExampleRadioButton3
            // 
            this.ExampleRadioButton3.AutoSize = true;
            this.ExampleRadioButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ExampleRadioButton3.Location = new System.Drawing.Point(12, 180);
            this.ExampleRadioButton3.Name = "ExampleRadioButton3";
            this.ExampleRadioButton3.Size = new System.Drawing.Size(113, 22);
            this.ExampleRadioButton3.TabIndex = 8;
            this.ExampleRadioButton3.TabStop = true;
            this.ExampleRadioButton3.Text = "radioButton3";
            this.ExampleRadioButton3.UseVisualStyleBackColor = true;
            this.ExampleRadioButton3.CheckedChanged += new System.EventHandler(this.OnExampleRadioButton3Checked);
            // 
            // ExampleRadioButton2
            // 
            this.ExampleRadioButton2.AutoSize = true;
            this.ExampleRadioButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ExampleRadioButton2.Location = new System.Drawing.Point(12, 150);
            this.ExampleRadioButton2.Name = "ExampleRadioButton2";
            this.ExampleRadioButton2.Size = new System.Drawing.Size(113, 22);
            this.ExampleRadioButton2.TabIndex = 7;
            this.ExampleRadioButton2.TabStop = true;
            this.ExampleRadioButton2.Text = "radioButton2";
            this.ExampleRadioButton2.UseVisualStyleBackColor = true;
            this.ExampleRadioButton2.CheckedChanged += new System.EventHandler(this.OnExampleRadioButton2Checked);
            // 
            // ExampleRadioButton1
            // 
            this.ExampleRadioButton1.AutoSize = true;
            this.ExampleRadioButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ExampleRadioButton1.Location = new System.Drawing.Point(12, 120);
            this.ExampleRadioButton1.Name = "ExampleRadioButton1";
            this.ExampleRadioButton1.Size = new System.Drawing.Size(113, 22);
            this.ExampleRadioButton1.TabIndex = 6;
            this.ExampleRadioButton1.TabStop = true;
            this.ExampleRadioButton1.Text = "radioButton1";
            this.ExampleRadioButton1.UseVisualStyleBackColor = true;
            this.ExampleRadioButton1.CheckedChanged += new System.EventHandler(this.OnExampleRadioButton1Checked);
            // 
            // InquiryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(467, 338);
            this.Controls.Add(this.ImagePanel);
            this.Controls.Add(this.ButtonsPanel);
            this.Controls.Add(this.AddressPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "InquiryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Address Inquiry";
            this.Load += new System.EventHandler(this.OnInquiryFormLoad);
            this.AddressPanel.ResumeLayout(false);
            this.AddressPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ButtonsPanel.ResumeLayout(false);
            this.ImagePanel.ResumeLayout(false);
            this.ImagePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel AddressPanel;
        private Panel ButtonsPanel;
        private Panel ImagePanel;
        private TextBox AddressTextBox;
        private Button MainButton;
        private PictureBox pictureBox1;
        private Label CityLabel;
        private Label CountryLabel;
        private Label Ipv4Label;
        private PictureBox StatusPictureBox;
        private PictureBox MainPictureBox;
        private RadioButton ExampleRadioButton1;
        private RadioButton ExampleRadioButton3;
        private RadioButton ExampleRadioButton2;
        private Label ExampleLabel;
    }
}