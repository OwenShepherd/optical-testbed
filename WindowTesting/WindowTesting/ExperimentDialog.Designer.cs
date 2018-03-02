namespace WindowTesting
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
            this.SuspendLayout();
            // 
            // DirectoryBrowse
            // 
            this.DirectoryBrowse.Location = new System.Drawing.Point(810, 12);
            this.DirectoryBrowse.Name = "DirectoryBrowse";
            this.DirectoryBrowse.Size = new System.Drawing.Size(156, 35);
            this.DirectoryBrowse.TabIndex = 0;
            this.DirectoryBrowse.Text = "Browse";
            this.DirectoryBrowse.UseVisualStyleBackColor = true;
            this.DirectoryBrowse.Click += new System.EventHandler(this.button1_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(810, 225);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(156, 35);
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
            this.ExperimentTitle.Location = new System.Drawing.Point(12, 19);
            this.ExperimentTitle.Name = "ExperimentTitle";
            this.ExperimentTitle.Size = new System.Drawing.Size(308, 32);
            this.ExperimentTitle.TabIndex = 2;
            this.ExperimentTitle.Text = "Experiment Directory:";
            this.ExperimentTitle.Click += new System.EventHandler(this.Label1_Click);
            // 
            // ExperimentDirectory
            // 
            this.ExperimentDirectory.AutoSize = true;
            this.ExperimentDirectory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExperimentDirectory.Location = new System.Drawing.Point(326, 22);
            this.ExperimentDirectory.Name = "ExperimentDirectory";
            this.ExperimentDirectory.Size = new System.Drawing.Size(124, 25);
            this.ExperimentDirectory.TabIndex = 3;
            this.ExperimentDirectory.Text = "Not Selected";
            this.ExperimentDirectory.Click += new System.EventHandler(this.ExperimentDirectory_Click);
            // 
            // ScheduleTitle
            // 
            this.ScheduleTitle.AutoSize = true;
            this.ScheduleTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScheduleTitle.Location = new System.Drawing.Point(12, 83);
            this.ScheduleTitle.Name = "ScheduleTitle";
            this.ScheduleTitle.Size = new System.Drawing.Size(211, 32);
            this.ScheduleTitle.TabIndex = 4;
            this.ScheduleTitle.Text = "Schedule File:";
            // 
            // ScheduleDirectory
            // 
            this.ScheduleDirectory.AutoSize = true;
            this.ScheduleDirectory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScheduleDirectory.Location = new System.Drawing.Point(326, 86);
            this.ScheduleDirectory.Name = "ScheduleDirectory";
            this.ScheduleDirectory.Size = new System.Drawing.Size(124, 25);
            this.ScheduleDirectory.TabIndex = 5;
            this.ScheduleDirectory.Text = "Not Selected";
            // 
            // ScheduleBrowse
            // 
            this.ScheduleBrowse.Location = new System.Drawing.Point(810, 80);
            this.ScheduleBrowse.Name = "ScheduleBrowse";
            this.ScheduleBrowse.Size = new System.Drawing.Size(156, 35);
            this.ScheduleBrowse.TabIndex = 6;
            this.ScheduleBrowse.Text = "Browse";
            this.ScheduleBrowse.UseVisualStyleBackColor = true;
            this.ScheduleBrowse.Click += new System.EventHandler(this.ScheduleBrowse_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 147);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(265, 32);
            this.label1.TabIndex = 7;
            this.label1.Text = "Experiment Name:";
            // 
            // ExpDialog
            // 
            this.ExpDialog.Location = new System.Drawing.Point(331, 147);
            this.ExpDialog.Name = "ExpDialog";
            this.ExpDialog.Size = new System.Drawing.Size(195, 26);
            this.ExpDialog.TabIndex = 8;
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(18, 225);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(156, 35);
            this.StartButton.TabIndex = 9;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // ExperimentDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 272);
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
    }
}