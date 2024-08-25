using DevExpress.Skins;
using DevExpress.XtraEditors;
using IniParser;
using IniParser.Model;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace DMONET3.Forms
{
    public partial class SettingsPage : DevExpress.XtraEditors.XtraForm
    {
        private readonly string iniFilePath = AppDomain.CurrentDomain.BaseDirectory + "INIs/Settings/settings.ini";
        private IniData data;
        public SettingsPage()
        {
            InitializeComponent();
            LoadSettings();
        }
        private void SettingsPage_Load(object sender, EventArgs e)
        {
            panelControl1.Visible = false;
            panelControl2.Visible = true;
            DevExpress.UserSkins.BonusSkins.Register();
            foreach (SkinContainer cnt in SkinManager.Default.Skins)
            {
                comboBoxEdit1.Properties.Items.Add(cnt.SkinName);
            }
        }
        private void LoadSettings()
        {
            var parser = new FileIniDataParser();
            data = parser.ReadFile(iniFilePath);
            bool autoConnect = bool.Parse(data["DMONSETTINGS"]["AutoConnect"]);
            AutoConnectCheckBox.Checked = autoConnect;
        }
        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxEdit comboBox = sender as ComboBoxEdit;
            string skinName = comboBox.Text;
            switch (skinName)
            {
                case "Default Skin":
                    DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = "DevExpress Style";
                    break;
                case "Default Dark Skin":
                    DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = "DevExpress Dark Style";
                    break;
                default:
                    DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = skinName;
                    break;
            }
        }
        public void OpenUrl(string url) => Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        private void navBarItem2_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            panelControl1.Visible = true;
            panelControl2.Visible = false;
            panelControl3.Visible = false;
        }
        private void navBarItem4_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            panelControl1.Visible = false;
            panelControl2.Visible = true;
            panelControl3.Visible = false;
        }
        private void navBarItem7_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            panelControl1.Visible = false;
            panelControl2.Visible = false;
            panelControl3.Visible = true;
        }
        private void AutoConnectCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bool autoConnect = AutoConnectCheckBox.Checked;
            data["DMONSETTINGS"]["AutoConnect"] = autoConnect.ToString();
            var parser = new FileIniDataParser();
            parser.WriteFile(iniFilePath, data);
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            OpenUrl("https://www.se7ensins.com/members/f11.569934/");
        }
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            OpenUrl("https://github.com/dmonskull?tab=repositories");
        }
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            OpenUrl("https://www.youtube.com/@DMONSKULL2");
        }
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            OpenUrl("https://dmonskull.github.io/DMONET/");
        }
        private void simpleButton6_Click(object sender, EventArgs e)
        {
            OpenUrl("https://discord.gg/gDJEYKUa8f");
        }
        private void simpleButton7_Click(object sender, EventArgs e)
        {
            OpenUrl("https://discord.gg/5rSkWCEpVf");
        }
    }
}