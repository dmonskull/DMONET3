using DevExpress.Skins;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMONET3.Forms
{
    public partial class SettingsPage : DevExpress.XtraEditors.XtraForm
    {
        public SettingsPage()
        {
            InitializeComponent();
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

        private void navBarItem2_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            panelControl1.Visible = true;
            panelControl2.Visible = false;
        }

        private void navBarItem4_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            panelControl1.Visible = false;
            panelControl2.Visible = true;
        }
    }
}