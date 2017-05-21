namespace TGFFConverter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.inputBrowseBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.convertBtn = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.autoGenBtn = new System.Windows.Forms.Button();
            this.sizeTxtBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.modeTxtBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.infoBtn = new System.Windows.Forms.PictureBox();
            this.coresTxtBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.generateBtn = new System.Windows.Forms.Button();
            this.trafficTypeBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.infoBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Input TGFF File:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(95, 20);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(243, 20);
            this.textBox1.TabIndex = 1;
            // 
            // inputBrowseBtn
            // 
            this.inputBrowseBtn.Location = new System.Drawing.Point(344, 20);
            this.inputBrowseBtn.Name = "inputBrowseBtn";
            this.inputBrowseBtn.Size = new System.Drawing.Size(33, 23);
            this.inputBrowseBtn.TabIndex = 2;
            this.inputBrowseBtn.Text = "...";
            this.inputBrowseBtn.UseVisualStyleBackColor = true;
            this.inputBrowseBtn.Click += new System.EventHandler(this.inputBrowseBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Myriad Pro Cond", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(118, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(259, 29);
            this.label3.TabIndex = 7;
            this.label3.Text = "Random Coregraph Generator";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.inputBrowseBtn);
            this.groupBox1.Controls.Add(this.convertBtn);
            this.groupBox1.Location = new System.Drawing.Point(12, 114);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(383, 98);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "TGFF Converter";
            // 
            // convertBtn
            // 
            this.convertBtn.Image = global::TGFFConverter.Properties.Resources.Arrow_Up_icon;
            this.convertBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.convertBtn.Location = new System.Drawing.Point(292, 49);
            this.convertBtn.Name = "convertBtn";
            this.convertBtn.Size = new System.Drawing.Size(85, 40);
            this.convertBtn.TabIndex = 6;
            this.convertBtn.Text = "Convert";
            this.convertBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.convertBtn.UseVisualStyleBackColor = true;
            this.convertBtn.Click += new System.EventHandler(this.convertBtn_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.autoGenBtn);
            this.groupBox2.Controls.Add(this.sizeTxtBox);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.modeTxtBox);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.infoBtn);
            this.groupBox2.Controls.Add(this.coresTxtBox);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.generateBtn);
            this.groupBox2.Controls.Add(this.trafficTypeBox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(12, 218);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(383, 105);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Coregraph Generator";
            // 
            // autoGenBtn
            // 
            this.autoGenBtn.Image = global::TGFFConverter.Properties.Resources.rocket_launch_run_icon;
            this.autoGenBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.autoGenBtn.Location = new System.Drawing.Point(161, 59);
            this.autoGenBtn.Name = "autoGenBtn";
            this.autoGenBtn.Size = new System.Drawing.Size(115, 40);
            this.autoGenBtn.TabIndex = 16;
            this.autoGenBtn.Text = "Auto-Generate";
            this.autoGenBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.autoGenBtn.UseVisualStyleBackColor = true;
            this.autoGenBtn.Click += new System.EventHandler(this.autoGenBtn_Click);
            // 
            // sizeTxtBox
            // 
            this.sizeTxtBox.Location = new System.Drawing.Point(77, 76);
            this.sizeTxtBox.MaxLength = 10;
            this.sizeTxtBox.Name = "sizeTxtBox";
            this.sizeTxtBox.Size = new System.Drawing.Size(67, 20);
            this.sizeTxtBox.TabIndex = 14;
            this.sizeTxtBox.Text = "200";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 79);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Packet Size:";
            // 
            // modeTxtBox
            // 
            this.modeTxtBox.Location = new System.Drawing.Point(77, 50);
            this.modeTxtBox.MaxLength = 10;
            this.modeTxtBox.Name = "modeTxtBox";
            this.modeTxtBox.Size = new System.Drawing.Size(67, 20);
            this.modeTxtBox.TabIndex = 12;
            this.modeTxtBox.Text = "3";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 53);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "# of Modes:";
            // 
            // infoBtn
            // 
            this.infoBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.infoBtn.Image = global::TGFFConverter.Properties.Resources.Status_dialog_information_icon;
            this.infoBtn.Location = new System.Drawing.Point(211, 21);
            this.infoBtn.Name = "infoBtn";
            this.infoBtn.Size = new System.Drawing.Size(25, 25);
            this.infoBtn.TabIndex = 11;
            this.infoBtn.TabStop = false;
            this.infoBtn.Click += new System.EventHandler(this.infoBtn_Click);
            // 
            // coresTxtBox
            // 
            this.coresTxtBox.Location = new System.Drawing.Point(310, 23);
            this.coresTxtBox.MaxLength = 10;
            this.coresTxtBox.Name = "coresTxtBox";
            this.coresTxtBox.Size = new System.Drawing.Size(67, 20);
            this.coresTxtBox.TabIndex = 7;
            this.coresTxtBox.Text = "64";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(245, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "# of Cores:";
            // 
            // generateBtn
            // 
            this.generateBtn.Image = global::TGFFConverter.Properties.Resources.science_chemistry_icon;
            this.generateBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.generateBtn.Location = new System.Drawing.Point(282, 58);
            this.generateBtn.Name = "generateBtn";
            this.generateBtn.Size = new System.Drawing.Size(95, 40);
            this.generateBtn.TabIndex = 8;
            this.generateBtn.Text = "Generate";
            this.generateBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.generateBtn.UseVisualStyleBackColor = true;
            this.generateBtn.Click += new System.EventHandler(this.generateBtn_Click);
            // 
            // trafficTypeBox
            // 
            this.trafficTypeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.trafficTypeBox.FormattingEnabled = true;
            this.trafficTypeBox.Items.AddRange(new object[] {
            "Random (Uniform)",
            "Random Modal",
            "Bit Reversal",
            "Shuffle",
            "Transpose Matrix",
            "Tornado",
            "Neighbour",
            "Hot Spot",
            "Stencil"});
            this.trafficTypeBox.Location = new System.Drawing.Point(77, 23);
            this.trafficTypeBox.Name = "trafficTypeBox";
            this.trafficTypeBox.Size = new System.Drawing.Size(128, 21);
            this.trafficTypeBox.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Traffic Type:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Myriad Pro Light", 9.749999F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(120, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(155, 16);
            this.label5.TabIndex = 12;
            this.label5.Text = "By: Muhammad Obaidullah";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TGFFConverter.Properties.Resources.MadHatter_icon;
            this.pictureBox1.Location = new System.Drawing.Point(12, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 99);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 330);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Random Coregraph Generator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.infoBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button inputBrowseBtn;
        private System.Windows.Forms.Button convertBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button generateBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox coresTxtBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox trafficTypeBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox infoBtn;
        private System.Windows.Forms.TextBox modeTxtBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox sizeTxtBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button autoGenBtn;
    }
}

