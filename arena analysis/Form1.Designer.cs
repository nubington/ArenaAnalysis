namespace arena_analysis
{
    partial class Form1
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
            this.mainTextBox = new System.Windows.Forms.RichTextBox();
            this.packValueLabel = new System.Windows.Forms.Label();
            this.dustValueLabel = new System.Windows.Forms.Label();
            this.packValueTextBox = new System.Windows.Forms.TextBox();
            this.dustValueTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // mainTextBox
            // 
            this.mainTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.mainTextBox.Location = new System.Drawing.Point(12, 63);
            this.mainTextBox.Name = "mainTextBox";
            this.mainTextBox.ReadOnly = true;
            this.mainTextBox.Size = new System.Drawing.Size(276, 453);
            this.mainTextBox.TabIndex = 0;
            this.mainTextBox.Text = "";
            // 
            // packValueLabel
            // 
            this.packValueLabel.AutoSize = true;
            this.packValueLabel.Location = new System.Drawing.Point(13, 13);
            this.packValueLabel.Name = "packValueLabel";
            this.packValueLabel.Size = new System.Drawing.Size(65, 13);
            this.packValueLabel.TabIndex = 1;
            this.packValueLabel.Text = "Pack Value:";
            // 
            // dustValueLabel
            // 
            this.dustValueLabel.AutoSize = true;
            this.dustValueLabel.Location = new System.Drawing.Point(13, 36);
            this.dustValueLabel.Name = "dustValueLabel";
            this.dustValueLabel.Size = new System.Drawing.Size(62, 13);
            this.dustValueLabel.TabIndex = 2;
            this.dustValueLabel.Text = "Dust Value:";
            // 
            // packValueTextBox
            // 
            this.packValueTextBox.Location = new System.Drawing.Point(84, 10);
            this.packValueTextBox.Name = "packValueTextBox";
            this.packValueTextBox.Size = new System.Drawing.Size(100, 20);
            this.packValueTextBox.TabIndex = 3;
            this.packValueTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.packValueTextBox_KeyDown);
            // 
            // dustValueTextBox
            // 
            this.dustValueTextBox.Location = new System.Drawing.Point(84, 33);
            this.dustValueTextBox.Name = "dustValueTextBox";
            this.dustValueTextBox.Size = new System.Drawing.Size(100, 20);
            this.dustValueTextBox.TabIndex = 4;
            this.dustValueTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dustValueTextBox_KeyDown);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(874, 528);
            this.Controls.Add(this.dustValueTextBox);
            this.Controls.Add(this.packValueTextBox);
            this.Controls.Add(this.dustValueLabel);
            this.Controls.Add(this.packValueLabel);
            this.Controls.Add(this.mainTextBox);
            this.Name = "Form1";
            this.Text = "Arena Rewards Analysis";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox mainTextBox;
        private System.Windows.Forms.Label packValueLabel;
        private System.Windows.Forms.Label dustValueLabel;
        private System.Windows.Forms.TextBox packValueTextBox;
        private System.Windows.Forms.TextBox dustValueTextBox;
    }
}

