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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.inputBrowseBtn = new System.Windows.Forms.Button();
            this.convertBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Input TGFF File:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(97, 103);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(332, 20);
            this.textBox1.TabIndex = 1;
            // 
            // inputBrowseBtn
            // 
            this.inputBrowseBtn.Location = new System.Drawing.Point(435, 101);
            this.inputBrowseBtn.Name = "inputBrowseBtn";
            this.inputBrowseBtn.Size = new System.Drawing.Size(65, 23);
            this.inputBrowseBtn.TabIndex = 2;
            this.inputBrowseBtn.Text = "Browse";
            this.inputBrowseBtn.UseVisualStyleBackColor = true;
            this.inputBrowseBtn.Click += new System.EventHandler(this.inputBrowseBtn_Click);
            // 
            // convertBtn
            // 
            this.convertBtn.Location = new System.Drawing.Point(391, 130);
            this.convertBtn.Name = "convertBtn";
            this.convertBtn.Size = new System.Drawing.Size(109, 49);
            this.convertBtn.TabIndex = 6;
            this.convertBtn.Text = "Convert";
            this.convertBtn.UseVisualStyleBackColor = true;
            this.convertBtn.Click += new System.EventHandler(this.convertBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(498, 73);
            this.label3.TabIndex = 7;
            this.label3.Text = "TGFF Converter";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 191);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.convertBtn);
            this.Controls.Add(this.inputBrowseBtn);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "TGFF Converter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button inputBrowseBtn;
        private System.Windows.Forms.Button convertBtn;
        private System.Windows.Forms.Label label3;
    }
}

