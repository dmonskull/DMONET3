using DevExpress.XtraEditors;
using IniParser.Model;
using IniParser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMONET3.Forms
{
    public partial class AddQuickLaunch : DevExpress.XtraEditors.XtraForm
    {
        public string GameName => textEditGameName.Text;
        public string LaunchPath => textEditLaunchPath.Text;
        public string IconPath => pictureEditIcon.Tag?.ToString();
        private string iniFilePath;
        public AddQuickLaunch(string launchPath)
        {
            InitializeComponent();
            textEditLaunchPath.Text = launchPath;
            iniFilePath = Path.Combine(Application.StartupPath, "INIs/Quicklaunch/quicklaunch.ini");
        }

        private void AddQuickLaunch_Load(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(GameName) || string.IsNullOrWhiteSpace(LaunchPath))
            {
                XtraMessageBox.Show("Game name and launch path are required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Save quick launch data to ini file using IniParser
            var parser = new FileIniDataParser();
            IniData data = File.Exists(iniFilePath) ? parser.ReadFile(iniFilePath) : new IniData();

            data[GameName]["LaunchPath"] = LaunchPath;
            data[GameName]["IconPath"] = IconPath ?? "default.png";

            parser.WriteFile(iniFilePath, data);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureEditIcon.Image = Image.FromFile(openFileDialog.FileName);
                    pictureEditIcon.Tag = openFileDialog.FileName;
                }
            }
        }
    }
}