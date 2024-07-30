﻿namespace DMONET3.Forms
{
    partial class QuickLaunch
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
            this.quickLaunchListView = new System.Windows.Forms.ListView();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // quickLaunchListView
            // 
            this.quickLaunchListView.HideSelection = false;
            this.quickLaunchListView.Location = new System.Drawing.Point(12, 12);
            this.quickLaunchListView.Name = "quickLaunchListView";
            this.quickLaunchListView.Size = new System.Drawing.Size(652, 365);
            this.quickLaunchListView.TabIndex = 4;
            this.quickLaunchListView.UseCompatibleStateImageBehavior = false;
            this.quickLaunchListView.DoubleClick += new System.EventHandler(this.quickLaunchListView_DoubleClick);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(12, 383);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(652, 34);
            this.simpleButton1.TabIndex = 3;
            this.simpleButton1.Text = "Open Xbox File Explorer";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // QuickLaunch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 429);
            this.Controls.Add(this.quickLaunchListView);
            this.Controls.Add(this.simpleButton1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "QuickLaunch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QuickLaunch";
            this.Load += new System.EventHandler(this.QuickLaunch_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView quickLaunchListView;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}