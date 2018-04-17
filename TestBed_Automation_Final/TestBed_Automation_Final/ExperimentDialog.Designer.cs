namespace ASEN
{
    partial class ExperimentDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExperimentDialog));
            this.DirectoryBrowse = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.ExperimentTitle = new System.Windows.Forms.Label();
            this.ExperimentDirectory = new System.Windows.Forms.Label();
            this.ScheduleTitle = new System.Windows.Forms.Label();
            this.ScheduleDirectory = new System.Windows.Forms.Label();
            this.ScheduleBrowse = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ExpDialog = new System.Windows.Forms.TextBox();
            this.StartButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.CameraSelection = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.teensySelection = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.PythonEXE = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.GainBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // DirectoryBrowse
            // 
            this.DirectoryBrowse.Location = new System.Drawing.Point(720, 10);
            this.DirectoryBrowse.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DirectoryBrowse.Name = "DirectoryBrowse";
            this.DirectoryBrowse.Size = new System.Drawing.Size(139, 28);
            this.DirectoryBrowse.TabIndex = 0;
            this.DirectoryBrowse.Text = "Browse";
            this.DirectoryBrowse.UseVisualStyleBackColor = true;
            this.DirectoryBrowse.Click += new System.EventHandler(this.button1_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(720, 403);
            this.ExitButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(139, 28);
            this.ExitButton.TabIndex = 1;
            this.ExitButton.Text = "Exit";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.Button2_Click);
            // 
            // ExperimentTitle
            // 
            this.ExperimentTitle.AutoSize = true;
            this.ExperimentTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExperimentTitle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ExperimentTitle.Location = new System.Drawing.Point(11, 14);
            this.ExperimentTitle.Name = "ExperimentTitle";
            this.ExperimentTitle.Size = new System.Drawing.Size(264, 29);
            this.ExperimentTitle.TabIndex = 2;
            this.ExperimentTitle.Text = "Experiment Directory:";
            this.ExperimentTitle.Click += new System.EventHandler(this.Label1_Click);
            // 
            // ExperimentDirectory
            // 
            this.ExperimentDirectory.AutoSize = true;
            this.ExperimentDirectory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExperimentDirectory.Location = new System.Drawing.Point(290, 18);
            this.ExperimentDirectory.Name = "ExperimentDirectory";
            this.ExperimentDirectory.Size = new System.Drawing.Size(105, 20);
            this.ExperimentDirectory.TabIndex = 3;
            this.ExperimentDirectory.Text = "Not Selected";
            this.ExperimentDirectory.Click += new System.EventHandler(this.ExperimentDirectory_Click);
            // 
            // ScheduleTitle
            // 
            this.ScheduleTitle.AutoSize = true;
            this.ScheduleTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScheduleTitle.Location = new System.Drawing.Point(11, 66);
            this.ScheduleTitle.Name = "ScheduleTitle";
            this.ScheduleTitle.Size = new System.Drawing.Size(182, 29);
            this.ScheduleTitle.TabIndex = 4;
            this.ScheduleTitle.Text = "Schedule File:";
            // 
            // ScheduleDirectory
            // 
            this.ScheduleDirectory.AutoSize = true;
            this.ScheduleDirectory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScheduleDirectory.Location = new System.Drawing.Point(290, 69);
            this.ScheduleDirectory.Name = "ScheduleDirectory";
            this.ScheduleDirectory.Size = new System.Drawing.Size(105, 20);
            this.ScheduleDirectory.TabIndex = 5;
            this.ScheduleDirectory.Text = "Not Selected";
            // 
            // ScheduleBrowse
            // 
            this.ScheduleBrowse.Location = new System.Drawing.Point(720, 64);
            this.ScheduleBrowse.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ScheduleBrowse.Name = "ScheduleBrowse";
            this.ScheduleBrowse.Size = new System.Drawing.Size(139, 28);
            this.ScheduleBrowse.TabIndex = 6;
            this.ScheduleBrowse.Text = "Browse";
            this.ScheduleBrowse.UseVisualStyleBackColor = true;
            this.ScheduleBrowse.Click += new System.EventHandler(this.ScheduleBrowse_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(228, 29);
            this.label1.TabIndex = 7;
            this.label1.Text = "Experiment Name:";
            // 
            // ExpDialog
            // 
            this.ExpDialog.Location = new System.Drawing.Point(295, 118);
            this.ExpDialog.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ExpDialog.Name = "ExpDialog";
            this.ExpDialog.Size = new System.Drawing.Size(173, 22);
            this.ExpDialog.TabIndex = 8;
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(16, 403);
            this.StartButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(139, 28);
            this.StartButton.TabIndex = 9;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 172);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 29);
            this.label2.TabIndex = 11;
            this.label2.Text = "Camera:";
            // 
            // CameraSelection
            // 
            this.CameraSelection.AccessibleDescription = "";
            this.CameraSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CameraSelection.FormattingEnabled = true;
            this.CameraSelection.Location = new System.Drawing.Point(292, 176);
            this.CameraSelection.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CameraSelection.Name = "CameraSelection";
            this.CameraSelection.Size = new System.Drawing.Size(176, 24);
            this.CameraSelection.TabIndex = 10;
            this.CameraSelection.SelectedIndexChanged += new System.EventHandler(this.CameraSelection_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(11, 238);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(172, 29);
            this.label3.TabIndex = 12;
            this.label3.Text = "Teensy COM:";
            // 
            // teensySelection
            // 
            this.teensySelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.teensySelection.FormattingEnabled = true;
            this.teensySelection.Location = new System.Drawing.Point(292, 238);
            this.teensySelection.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.teensySelection.Name = "teensySelection";
            this.teensySelection.Size = new System.Drawing.Size(176, 24);
            this.teensySelection.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(11, 291);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(158, 29);
            this.label4.TabIndex = 14;
            this.label4.Text = "Python EXE:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // PythonEXE
            // 
            this.PythonEXE.Location = new System.Drawing.Point(292, 289);
            this.PythonEXE.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PythonEXE.Name = "PythonEXE";
            this.PythonEXE.Size = new System.Drawing.Size(139, 28);
            this.PythonEXE.TabIndex = 15;
            this.PythonEXE.Text = "Browse";
            this.PythonEXE.UseVisualStyleBackColor = true;
            this.PythonEXE.Click += new System.EventHandler(this.PythonEXE_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(13, 345);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 29);
            this.label5.TabIndex = 16;
            this.label5.Text = "Gain:";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // GainBox
            // 
            this.GainBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GainBox.Location = new System.Drawing.Point(294, 344);
            this.GainBox.Name = "GainBox";
            this.GainBox.Size = new System.Drawing.Size(137, 30);
            this.GainBox.TabIndex = 17;
            this.GainBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // ExperimentDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 442);
            this.Controls.Add(this.GainBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.PythonEXE);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.teensySelection);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CameraSelection);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.ExpDialog);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ScheduleBrowse);
            this.Controls.Add(this.ScheduleDirectory);
            this.Controls.Add(this.ScheduleTitle);
            this.Controls.Add(this.ExperimentDirectory);
            this.Controls.Add(this.ExperimentTitle);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.DirectoryBrowse);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ExperimentDialog";
            this.Text = "AWESoMe";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button DirectoryBrowse;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.Label ExperimentTitle;
        private System.Windows.Forms.Label ExperimentDirectory;
        private System.Windows.Forms.Label ScheduleTitle;
        private System.Windows.Forms.Label ScheduleDirectory;
        private System.Windows.Forms.Button ScheduleBrowse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ExpDialog;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox CameraSelection;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox teensySelection;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button PythonEXE;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox GainBox;
    }
}