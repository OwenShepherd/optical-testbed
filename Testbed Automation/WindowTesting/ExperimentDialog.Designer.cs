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
            this.SuspendLayout();
            // 
            // DirectoryBrowse
            // 
            this.DirectoryBrowse.Location = new System.Drawing.Point(540, 8);
            this.DirectoryBrowse.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DirectoryBrowse.Name = "DirectoryBrowse";
            this.DirectoryBrowse.Size = new System.Drawing.Size(104, 23);
            this.DirectoryBrowse.TabIndex = 0;
            this.DirectoryBrowse.Text = "Browse";
            this.DirectoryBrowse.UseVisualStyleBackColor = true;
            this.DirectoryBrowse.Click += new System.EventHandler(this.button1_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(540, 246);
            this.ExitButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(104, 23);
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
            this.ExperimentTitle.Location = new System.Drawing.Point(8, 12);
            this.ExperimentTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ExperimentTitle.Name = "ExperimentTitle";
            this.ExperimentTitle.Size = new System.Drawing.Size(212, 24);
            this.ExperimentTitle.TabIndex = 2;
            this.ExperimentTitle.Text = "Experiment Directory:";
            this.ExperimentTitle.Click += new System.EventHandler(this.Label1_Click);
            // 
            // ExperimentDirectory
            // 
            this.ExperimentDirectory.AutoSize = true;
            this.ExperimentDirectory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExperimentDirectory.Location = new System.Drawing.Point(217, 14);
            this.ExperimentDirectory.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ExperimentDirectory.Name = "ExperimentDirectory";
            this.ExperimentDirectory.Size = new System.Drawing.Size(89, 17);
            this.ExperimentDirectory.TabIndex = 3;
            this.ExperimentDirectory.Text = "Not Selected";
            this.ExperimentDirectory.Click += new System.EventHandler(this.ExperimentDirectory_Click);
            // 
            // ScheduleTitle
            // 
            this.ScheduleTitle.AutoSize = true;
            this.ScheduleTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScheduleTitle.Location = new System.Drawing.Point(8, 54);
            this.ScheduleTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ScheduleTitle.Name = "ScheduleTitle";
            this.ScheduleTitle.Size = new System.Drawing.Size(146, 24);
            this.ScheduleTitle.TabIndex = 4;
            this.ScheduleTitle.Text = "Schedule File:";
            // 
            // ScheduleDirectory
            // 
            this.ScheduleDirectory.AutoSize = true;
            this.ScheduleDirectory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScheduleDirectory.Location = new System.Drawing.Point(217, 56);
            this.ScheduleDirectory.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ScheduleDirectory.Name = "ScheduleDirectory";
            this.ScheduleDirectory.Size = new System.Drawing.Size(89, 17);
            this.ScheduleDirectory.TabIndex = 5;
            this.ScheduleDirectory.Text = "Not Selected";
            // 
            // ScheduleBrowse
            // 
            this.ScheduleBrowse.Location = new System.Drawing.Point(540, 52);
            this.ScheduleBrowse.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ScheduleBrowse.Name = "ScheduleBrowse";
            this.ScheduleBrowse.Size = new System.Drawing.Size(104, 23);
            this.ScheduleBrowse.TabIndex = 6;
            this.ScheduleBrowse.Text = "Browse";
            this.ScheduleBrowse.UseVisualStyleBackColor = true;
            this.ScheduleBrowse.Click += new System.EventHandler(this.ScheduleBrowse_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 96);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 24);
            this.label1.TabIndex = 7;
            this.label1.Text = "Experiment Name:";
            // 
            // ExpDialog
            // 
            this.ExpDialog.Location = new System.Drawing.Point(221, 96);
            this.ExpDialog.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ExpDialog.Name = "ExpDialog";
            this.ExpDialog.Size = new System.Drawing.Size(131, 20);
            this.ExpDialog.TabIndex = 8;
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(12, 246);
            this.StartButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(104, 23);
            this.StartButton.TabIndex = 9;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 140);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 24);
            this.label2.TabIndex = 11;
            this.label2.Text = "Camera:";
            // 
            // CameraSelection
            // 
            this.CameraSelection.AccessibleDescription = "";
            this.CameraSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CameraSelection.FormattingEnabled = true;
            this.CameraSelection.Location = new System.Drawing.Point(219, 143);
            this.CameraSelection.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.CameraSelection.Name = "CameraSelection";
            this.CameraSelection.Size = new System.Drawing.Size(133, 21);
            this.CameraSelection.TabIndex = 10;
            this.CameraSelection.SelectedIndexChanged += new System.EventHandler(this.CameraSelection_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 193);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 24);
            this.label3.TabIndex = 12;
            this.label3.Text = "Teensy COM:";
            // 
            // teensySelection
            // 
            this.teensySelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.teensySelection.FormattingEnabled = true;
            this.teensySelection.Location = new System.Drawing.Point(219, 193);
            this.teensySelection.Name = "teensySelection";
            this.teensySelection.Size = new System.Drawing.Size(133, 21);
            this.teensySelection.TabIndex = 13;
            // 
            // ExperimentDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 280);
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
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
    }
}