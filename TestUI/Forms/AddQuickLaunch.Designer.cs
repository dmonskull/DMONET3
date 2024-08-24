namespace DMONET3.Forms
{
    partial class AddQuickLaunch
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
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.pictureEditIcon = new DevExpress.XtraEditors.PictureEdit();
            this.textEditLaunchPath = new DevExpress.XtraEditors.TextEdit();
            this.textEditGameName = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEditIcon.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditLaunchPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditGameName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(73, 132);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(145, 20);
            this.simpleButton2.TabIndex = 12;
            this.simpleButton2.Text = "Select Icon";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 56);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(98, 13);
            this.labelControl2.TabIndex = 10;
            this.labelControl2.Text = "Enter Directory Path";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 11);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(86, 13);
            this.labelControl1.TabIndex = 8;
            this.labelControl1.Text = "Enter Game Name";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(134, 158);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(84, 20);
            this.simpleButton1.TabIndex = 7;
            this.simpleButton1.Text = "Add Launch";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // pictureEditIcon
            // 
            this.pictureEditIcon.Location = new System.Drawing.Point(12, 101);
            this.pictureEditIcon.Name = "pictureEditIcon";
            this.pictureEditIcon.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEditIcon.Size = new System.Drawing.Size(55, 77);
            this.pictureEditIcon.TabIndex = 13;
            // 
            // textEditLaunchPath
            // 
            this.textEditLaunchPath.Location = new System.Drawing.Point(12, 75);
            this.textEditLaunchPath.Name = "textEditLaunchPath";
            this.textEditLaunchPath.Size = new System.Drawing.Size(206, 20);
            this.textEditLaunchPath.TabIndex = 11;
            // 
            // textEditGameName
            // 
            this.textEditGameName.Location = new System.Drawing.Point(12, 30);
            this.textEditGameName.Name = "textEditGameName";
            this.textEditGameName.Size = new System.Drawing.Size(206, 20);
            this.textEditGameName.TabIndex = 9;
            // 
            // AddQuickLaunch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(230, 188);
            this.Controls.Add(this.pictureEditIcon);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.textEditLaunchPath);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.textEditGameName);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.simpleButton1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IconOptions.Image = global::DMONET3.Properties.Resources.logo3;
            this.MaximizeBox = false;
            this.Name = "AddQuickLaunch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddQuickLaunch";
            this.Load += new System.EventHandler(this.AddQuickLaunch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEditIcon.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditLaunchPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditGameName.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PictureEdit pictureEditIcon;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.TextEdit textEditLaunchPath;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit textEditGameName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}