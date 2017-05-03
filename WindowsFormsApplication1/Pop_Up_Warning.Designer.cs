namespace MicrophoneRecord
{
    partial class Pop_Up_Warning
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
            this.Yes_Button = new System.Windows.Forms.Button();
            this.No_Button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Path_Name = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Yes_Button
            // 
            this.Yes_Button.Location = new System.Drawing.Point(197, 192);
            this.Yes_Button.Name = "Yes_Button";
            this.Yes_Button.Size = new System.Drawing.Size(107, 39);
            this.Yes_Button.TabIndex = 0;
            this.Yes_Button.Text = "Yes";
            this.Yes_Button.UseVisualStyleBackColor = true;
            this.Yes_Button.Click += new System.EventHandler(this.Yes_Button_Click);
            // 
            // No_Button
            // 
            this.No_Button.Location = new System.Drawing.Point(466, 192);
            this.No_Button.Name = "No_Button";
            this.No_Button.Size = new System.Drawing.Size(107, 39);
            this.No_Button.TabIndex = 1;
            this.No_Button.Text = "No";
            this.No_Button.UseVisualStyleBackColor = true;
            this.No_Button.Click += new System.EventHandler(this.No_Button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(219, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "The Following Folder:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(185, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Has Files In Them";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(257, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(260, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "Do you want to overwrite?";
            // 
            // Path_Name
            // 
            this.Path_Name.AutoSize = true;
            this.Path_Name.Location = new System.Drawing.Point(14, 69);
            this.Path_Name.Name = "Path_Name";
            this.Path_Name.Size = new System.Drawing.Size(35, 13);
            this.Path_Name.TabIndex = 5;
            this.Path_Name.Text = "label4";
            // 
            // Pop_Up_Warning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 261);
            this.Controls.Add(this.Path_Name);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.No_Button);
            this.Controls.Add(this.Yes_Button);
            this.Name = "Pop_Up_Warning";
            this.Text = "Pop_Up_Warning";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Yes_Button;
        private System.Windows.Forms.Button No_Button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label Path_Name;
    }
}