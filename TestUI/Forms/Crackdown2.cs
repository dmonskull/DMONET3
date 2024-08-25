using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using TestUI;
using XDevkit;
using XDRPC;

namespace DMONET3.Forms
{
    public partial class Crackdown2 : DevExpress.XtraEditors.XtraForm
    {
        // connection stuff
        public IXboxManager xbManager = null;
        public IXboxConsole xbCon = null;
        public bool activeConnection = false;
        private uint ConnectionCode;
        public uint xboxConnection = 0;
        public string debuggerName = null;
        public string userName = null;
        public Form1 form1;
        //Crackdown 2 stuff
        public bool fly;
        public bool godmode;
        public bool infiniteammo;
        public bool fps;
        public bool toggleoutlines;
        private bool ray;
        private int currentLodLevel = 0;
        public Crackdown2()
        {
            InitializeComponent();
        }

        private void Crackdown2_Load(object sender, EventArgs e)
        {
            comboBoxEdit1.Properties.Items.Clear();
            comboBoxEdit1.Properties.Items.AddRange(weaponList);
        }
        public bool ConnectToConsole2()
        {
            if (activeConnection && Form1.xbCon.DebugTarget.IsDebuggerConnected(out debuggerName, out userName))
            {
                return true;
            }
            try
            {
                xbManager = (XboxManager)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("A5EB45D8-F3B6-49B9-984A-0D313AB60342")));
                Form1.xbCon = xbManager.OpenConsole(xbManager.DefaultConsole);
                ConnectionCode = Form1.xbCon.OpenConnection(null);
                xboxConnection = Form1.xbCon.OpenConnection(null);

                if (!Form1.xbCon.DebugTarget.IsDebuggerConnected(out debuggerName, out userName))
                {
                    Form1.xbCon.DebugTarget.ConnectAsDebugger("DMONET", XboxDebugConnectFlags.Force);
                }

                activeConnection = Form1.xbCon.DebugTarget.IsDebuggerConnected(out debuggerName, out userName);
                return activeConnection;
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Could not connect to console: " + xbManager.DefaultConsole);
                return false;
            }
        }
        public void SendCommandWithToggle(uint address, string command, ref bool toggleState, SimpleButton button, string toggleOnText, string toggleOffText)
        {
            try
            {
                Form1.xbCon.CallString(address, command);
                toggleState = !toggleState;
                button.Text = toggleState ? toggleOnText : toggleOffText;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while sending command '{command}': {ex.Message}", "Error");
            }
        }
        public void SendCommand(uint address, string command)
        {
            try
            {
                Form1.xbCon.CallString(address, command);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while sending command '{command}': {ex.Message}", "Error");
            }
        }
        public void OpenUrl(string url) => Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        private List<Weapon> weaponList = new List<Weapon>
{
    new Weapon { FullID = "MP_WEPGEN03_Demp90A", DisplayName = "Demp90A" },
    new Weapon { FullID = "WEPGEN11_SticklerGrenade", DisplayName = "SticklerGrenade" },
    new Weapon { FullID = "WEPGEN04_MachHMG120", DisplayName = "MachHMG120" },
    new Weapon { FullID = "WEPGEN06_Glauncher", DisplayName = "Glauncher" },
    new Weapon { FullID = "WEPGEN07_RLauncher", DisplayName = "RLauncher" },
    new Weapon { FullID = "WEPGEN08_HRlauncher", DisplayName = "HRlauncher" },
    new Weapon { FullID = "WEPGEN05_SniperSX1A", DisplayName = "SniperSX1A" },
    new Weapon { FullID = "MP_WEPGEN03_Demp90A_Bullet", DisplayName = "Demp90A Bullet" },
    new Weapon { FullID = "MP_WEPGEN04_MachHMG120", DisplayName = "MachHMG120" },
    new Weapon { FullID = "MP_WEPGEN07_RLauncher", DisplayName = "RLauncher" },
    new Weapon { FullID = "WEPGEN07_RLauncher_Proj", DisplayName = "RLauncher Proj" },
    new Weapon { FullID = "MP_WEPGEN02_IngallsAL107", DisplayName = "IngallsAL107" },
    new Weapon { FullID = "MP_WEPGEN01_IngallsXGS", DisplayName = "IngallsXGS" },
    new Weapon { FullID = "WEPGEN03_Demp90A", DisplayName = "Demp90A" },
    new Weapon { FullID = "WEPGEN02_IngallsAL107", DisplayName = "IngallsAL107" },
    new Weapon { FullID = "WEPGEN01_IngallsXGS", DisplayName = "IngallsXGS" },
    new Weapon { FullID = "WEPGEN10_ClusterGrenade", DisplayName = "ClusterGrenade" },
    new Weapon { FullID = "WEPGEN10_Grenade", DisplayName = "Grenade" },
    new Weapon { FullID = "WEPGEN10_ShrapnelGrenade", DisplayName = "ShrapnelGrenade" },
    new Weapon { FullID = "MP_WEPAGY15_UVShotgun", DisplayName = "UVShotgun" },
    new Weapon { FullID = "MP_WEPAGY05_Sniper", DisplayName = "Sniper" },
    new Weapon { FullID = "MP_WEPAGY05_AMSniper", DisplayName = "AMSniper" },
    new Weapon { FullID = "MP_WEPAGY04_MachGun", DisplayName = "MachGun" },
    new Weapon { FullID = "MP_WEPAGY03_UltraShotgun", DisplayName = "UltraShotgun" },
    new Weapon { FullID = "MP_WEPAGY02_UltraAssault", DisplayName = "UltraAssault" },
    new Weapon { FullID = "MP_WEPAGY01_UltraSMG", DisplayName = "UltraSMG" },
    new Weapon { FullID = "WEPAGY05_Sniper", DisplayName = "Sniper" },
    new Weapon { FullID = "WEPAGY10_LimpetCharge", DisplayName = "LimpetCharge" },
    new Weapon { FullID = "WEPAGY10_ProxMine", DisplayName = "ProxMine" },
    new Weapon { FullID = "WEPAGY06_Flocket", DisplayName = "Flocket" },
    new Weapon { FullID = "WEPTUR11_TagLauncher", DisplayName = "TagLauncher" },
    new Weapon { FullID = "WEPTUR06_BreachRocket", DisplayName = "BreachRocket" },
    new Weapon { FullID = "WEPTUR01_MachGun", DisplayName = "MachGun" },
    new Weapon { FullID = "WEPTUR03_Flack", DisplayName = "Flack" },
    new Weapon { FullID = "WEPTUR04_Laser", DisplayName = "Laser" },
    new Weapon { FullID = "WEPTUR05_UV", DisplayName = "UV" },
    new Weapon { FullID = "WEPFRK12_GoliathRockBomb", DisplayName = "GoliathRockBomb" },
    new Weapon { FullID = "WEPFRK10_AcidClusterBomb", DisplayName = "AcidClusterBomb" },
    new Weapon { FullID = "WEPGAD10_BoostPack", DisplayName = "BoostPack" },
    new Weapon { FullID = "WEPGAD11_PortableLaunchPad", DisplayName = "PortableLaunchPad" },
    new Weapon { FullID = "WEPGAD03_Mags", DisplayName = "Mags" },
    new Weapon { FullID = "WEPGAD01_Harpoon", DisplayName = "Harpoon" }
};

        #region Buttons
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            SendCommandWithToggle(0x82771EB0, "fly", ref fly, simpleButton2, "Fly: ON", "Fly: OFF");
        }
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            SendCommandWithToggle(0x82771EB0, "god", ref godmode, simpleButton3, "God: ON", "God: OFF");
        }
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            SendCommandWithToggle(0x82771EB0, "infiniteammo", ref infiniteammo, simpleButton4, "Infinite Ammo: ON", "Infinite Ammo: OFF");
        }
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            SendCommandWithToggle(0x82771EB0, "fps", ref fps, simpleButton5, "FPS: ON", "FPS: OFF");
        }
        private void simpleButton6_Click(object sender, EventArgs e)
        {
            SendCommandWithToggle(0x82771EB0, "toggleoutlines", ref toggleoutlines, simpleButton6, "Outlines: OFF", "Outlines: ON");
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            SendCommand(0x82771EB0, textEdit1.Text);
        }
        private void simpleButton7_Click(object sender, EventArgs e)
        {
            SendCommand(0x82771EB0, "Duck!");
        }
        private void simpleButton9_Click(object sender, EventArgs e)
        {
            SendCommand(0x82771EB0, "maxagentskills");
            Thread.Sleep(1000);
            SendCommand(0x82771EB0, "allskills");
        }
        private void simpleButton8_Click(object sender, EventArgs e)
        {
            simpleButton8.ForeColor = ray ? Color.Red : Color.Green;
            SendCommand(0x82771EB0, ray ? "kill_deity_ray" : "test_deity_ray");
            ray = !ray;
        }
        private void simpleButton10_Click(object sender, EventArgs e)
        {
            try
            {
                Form1.xbCon.CallString(0x82771EB0, $"forcelod {currentLodLevel}");
                simpleButton10.Text = $"LOD Level: {currentLodLevel}";
                currentLodLevel = (currentLodLevel + 1) % 6;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error");
            }
        }
        private void simpleButton11_Click(object sender, EventArgs e)
        {
            SendCommand(0x82771EB0, "tode3advance");
        }
        private void simpleButton12_Click(object sender, EventArgs e)
        {
            SendCommand(0x82771EB0, "loaded");
        }
        private void simpleButton13_Click(object sender, EventArgs e)
        {
            if (comboBoxEdit1.SelectedItem is Weapon selectedWeapon)
            {
                SendCommand(0x82771EB0, $"addweapon \"{selectedWeapon.FullID}\"");
            }
            else
            {
                XtraMessageBox.Show("Please select a weapon.");
            }
        }
        private void simpleButton14_Click(object sender, EventArgs e)
        {
            SendCommand(0x82771EB0, "apocalypse");
        }
        private void simpleButton15_Click(object sender, EventArgs e)
        {
            SendCommand(0x82771EB0, "tsmdebugtrapezoid");
        }
        private void hyperlinkLabelControl1_Click(object sender, EventArgs e)
        {
            OpenUrl("https://github.com/dmonskull/CrackdownSeriesXbox360Documentation?tab=readme-ov-file#crackdown2");
        }
        #endregion
    }
    public class Weapon
    {
        public string FullID { get; set; }
        public string DisplayName { get; set; }
        public override string ToString()
        {
            return DisplayName; // This ensures that the ComboBox displays the DisplayName.
        }
    }
}