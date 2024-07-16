using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using XDevkit;
using DevExpress.Utils.Gesture;
using System.Threading;
using XDRPC;

namespace DMONET3.Forms
{
    public partial class SaintsRow : DevExpress.XtraEditors.XtraForm
    {
        // connection stuff
        public IXboxManager xbManager = null;
        public IXboxConsole xbCon = null;
        public bool activeConnection = false;
        private uint ConnectionCode;
        public uint xboxConnection = 0;
        public string debuggerName = null;
        public string userName = null;
        // Saints Row stuff
        private bool god, walk, cash, uammo, jump, weapon, recoil, invis, turret, fps;
        private bool rlevel, rsky, rshadows, ritems, rchar, rfog, gdebug, ldebug, pPOVdebug;
        private bool occludersdebug, occludersdebug2, fperson, freeze, timerbool, glow;
        public SaintsRow()
        {
            InitializeComponent();
        }
        private void SaintsRow_Load(object sender, EventArgs e)
        {
            try
            {
                ConnectToConsole2();
            }
            catch { }
        }
        public bool ConnectToConsole2()
        {
            if (!activeConnection)
            {
                xbManager = (XboxManager)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("A5EB45D8-F3B6-49B9-984A-0D313AB60342")));
                xbCon = xbManager.OpenConsole(xbManager.DefaultConsole);
                ConnectionCode = xbCon.OpenConnection(null);
                try
                {
                    xboxConnection = xbCon.OpenConnection(null);
                }
                catch (Exception)
                {
                    XtraMessageBox.Show("Could not connect to console: " + xbManager.DefaultConsole);
                    return false;
                }
                if (xbCon.DebugTarget.IsDebuggerConnected(out debuggerName, out userName))
                {
                    activeConnection = true;
                    return true;
                }
                xbCon.DebugTarget.ConnectAsDebugger("Xbox Toolbox", XboxDebugConnectFlags.Force);
                if (!xbCon.DebugTarget.IsDebuggerConnected(out debuggerName, out userName))
                {
                    XtraMessageBox.Show("Attempted to connect to console: " + xbCon.Name + " but failed");
                    return false;
                }
                activeConnection = true;
                return true;
            }
            if (xbCon.DebugTarget.IsDebuggerConnected(out debuggerName, out userName))
            {
                return true;
            }
            activeConnection = false;
            return ConnectToConsole2();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            string[] charColors = new string[]
            {
        "char_ambient 148 0 211",
        "char_ambient 75 0 130",
        "char_ambient 0 0 255",
        "char_ambient 0 255 0",
        "char_ambient 255 255 0",
        "char_ambient 255 127 0",
        "char_ambient 255 0 0"
            };
            ExecuteColorCommands(charColors);
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            string[] levelColors = new string[]
            {
        "level_ambient 255 0 0",
        "level_ambient 255 127 0",
        "level_ambient 255 255 0",
        "level_ambient 0 255 0",
        "level_ambient 0 0 255",
        "level_ambient 75 0 130",
        "level_ambient 148 0 211"
            };
            ExecuteColorCommands(levelColors);
        }
        private void ExecuteColorCommands(string[] commands)
        {
            foreach (string command in commands)
            {
                xbCon.CallString(0x8263cb10, new object[] { command });
                Thread.Sleep(1000);
            }
        }

        public uint[] PLAYERS_HEALTH_BARX = new uint[]
{
            3260816936U,
            3260839064U,
            3260881624U,
            3260903752U,
            3260925880U,
            3260946312U,
            3260968440U,
            3260990568U,
            3261011000U,
            3261033128U,
            3261055256U,
            3261119944U
};

        #region Button Clicks
        private void simpleButton7_Click(object sender, EventArgs e)
        {
            simpleButton7.ForeColor = god ? Color.Red : Color.Green;
            byte[] healthValue = new byte[] { god ? (byte)1 : (byte)100 };

            foreach (uint address in PLAYERS_HEALTH_BARX)
            {
                xbCon.WriteBytes(address, healthValue);
            }

            god = !god;
        }
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            simpleButton3.ForeColor = uammo ? Color.Red : Color.Green;
            xbCon.WriteBytes(2204058237U, new byte[] { uammo ? (byte)0 : (byte)1 });
            uammo = !uammo;
        }
        private void simpleButton9_Click(object sender, EventArgs e)
        {
            simpleButton9.ForeColor = recoil ? Color.Red : Color.Green;
            xbCon.CallString(0x8263cb10, new object[] { recoil ? "recoil 1" : "recoil 0" });
            recoil = !recoil;
        }
        private void simpleButton10_Click(object sender, EventArgs e)
        {
            simpleButton10.ForeColor = invis ? Color.Red : Color.Green;
            xbCon.CallString(0x8263cb10, new object[] { "Nathaniel_hack" });
            if (!invis)
            {
                xbCon.CallString(0x8263cb10, new object[] { "Nathaniel_hack_alpha 0" });
            }
            invis = !invis;
        }
        private void simpleButton31_Click(object sender, EventArgs e)
        {
            simpleButton23.ForeColor = freeze ? Color.Red : Color.Green;
            xbCon.WriteBytes(2204061010U, new byte[] { freeze ? (byte)0 : (byte)1 });
            freeze = !freeze;
        }
        private void simpleButton8_Click(object sender, EventArgs e)
        {
            xbCon.CallString(0x8263cb10, new object[] { "vomit" });
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            simpleButton2.ForeColor = weapon ? Color.Red : Color.Green;
            xbCon.CallString(0x8263cb10, new object[] { weapon ? "give_player_n_weapons 0" : "give_player_n_weapons 7" });
            weapon = !weapon;
        }
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            simpleButton4.ForeColor = cash ? Color.Red : Color.Green;
            xbCon.WriteBytes(3260839472U, cash ? new byte[] { byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue } : new byte[] { 119, 119, 119, 119 });
            cash = !cash;
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            simpleButton2.ForeColor = jump ? Color.Red : Color.Green;
            xbCon.CallString(0x8263cb10, new object[] { jump ? "Jump_height 1" : "Jump_height 21" });
            jump = !jump;
        }
        private void simpleButton11_Click(object sender, EventArgs e)
        {
            simpleButton11.ForeColor = turret ? Color.Red : Color.Green;
            xbCon.CallString(0x8263cb10, new object[] { "turret" });
            turret = !turret;
        }
        private void simpleButton32_Click(object sender, EventArgs e)
        {
            simpleButton32.ForeColor = timerbool ? Color.Red : Color.Green;
            if (!timerbool)
            {
                timer1.Start();
                timer2.Start();
            }
            else
            {
                timer1.Stop();
                timer2.Stop();
            }
            timerbool = !timerbool;
        }
        private void simpleButton27_Click(object sender, EventArgs e)
        {
            simpleButton27.ForeColor = fperson ? Color.Red : Color.Green;
            xbCon.CallString(0x8263cb10, new object[] { "Nathaniel_hack" });
            if (!fperson)
            {
                xbCon.CallString(0x8263cb10, new object[] { "Nathaniel_hack_alpha 0", "camera_radius 4" });
            }
            else
            {
                xbCon.CallString(0x8263cb10, new object[] { "camera_radius 1" });
            }
            fperson = !fperson;
        }
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            simpleButton5.ForeColor = glow ? Color.Red : Color.Green;
            xbCon.CallString(0x8263cb10, new object[] { "item_glows" });
            glow = !glow;
        }
        private void simpleButton13_Click(object sender, EventArgs e)
        {
            simpleButton13.ForeColor = rlevel ? Color.Red : Color.Green;
            xbCon.CallString(0x8263cb10, new object[] { "R_level" });
            rlevel = !rlevel;
        }
        private void simpleButton15_Click(object sender, EventArgs e)
        {
            simpleButton15.ForeColor = rshadows ? Color.Red : Color.Green;
            xbCon.CallString(0x8263cb10, new object[] { "r_shadows" });
            rshadows = !rshadows;
        }
        private void simpleButton17_Click(object sender, EventArgs e)
        {
            simpleButton17.ForeColor = rchar ? Color.Red : Color.Green;
            xbCon.CallString(0x8263cb10, new object[] { "R_chars" });
            rchar = !rchar;
        }
        private void simpleButton18_Click(object sender, EventArgs e)
        {
            simpleButton18.ForeColor = gdebug ? Color.Red : Color.Green;
            xbCon.CallString(0x8263cb10, new object[] { "glare_debug" });
            gdebug = !gdebug;
        }
        private void simpleButton19_Click(object sender, EventArgs e)
        {
            simpleButton19.ForeColor = pPOVdebug ? Color.Red : Color.Green;
            xbCon.CallString(0x8263cb10, new object[] { "r_player_pov" });
            pPOVdebug = !pPOVdebug;
        }
        private void simpleButton23_Click(object sender, EventArgs e)
        {
            simpleButton23.ForeColor = occludersdebug2 ? Color.Red : Color.Green;
            xbCon.CallString(0x8263cb10, new object[] { "r_rel_occluders" });
            occludersdebug2 = !occludersdebug2;
        }
        private void simpleButton12_Click(object sender, EventArgs e)
        {
            simpleButton12.ForeColor = fps ? Color.Red : Color.Green;
            xbCon.CallString(0x8263cb10, new object[] { "show_fps" });
            fps = !fps;
        }
        private void simpleButton14_Click(object sender, EventArgs e)
        {
            simpleButton14.ForeColor = rsky ? Color.Red : Color.Green;
            xbCon.CallString(0x8263cb10, new object[] { "R_skybox" });
            rsky = !rsky;
        }
        private void simpleButton16_Click(object sender, EventArgs e)
        {
            simpleButton16.ForeColor = ritems ? Color.Red : Color.Green;
            xbCon.CallString(0x8263cb10, new object[] { "R_static" });
            ritems = !ritems;
        }
        private void simpleButton21_Click(object sender, EventArgs e)
        {
            simpleButton21.ForeColor = rfog ? Color.Red : Color.Green;
            xbCon.CallString(0x8263cb10, new object[] { "fog" });
            rfog = !rfog;
        }
        private void simpleButton20_Click(object sender, EventArgs e)
        {
            simpleButton20.ForeColor = ldebug ? Color.Red : Color.Green;
            xbCon.CallString(0x8263cb10, new object[] { "R_lights_debug" });
            ldebug = !ldebug;
        }
        private void simpleButton22_Click(object sender, EventArgs e)
        {
            simpleButton22.ForeColor = occludersdebug ? Color.Red : Color.Green;
            xbCon.CallString(0x8263cb10, new object[] { "r_occluders" });
            occludersdebug = !occludersdebug;
        }
        private void simpleButton35_Click(object sender, EventArgs e)
        {
            xbCon.CallString(0x8263cb10, new object[] { "flip_car" });
        }
        private void simpleButton24_Click(object sender, EventArgs e)
        {
            string[] cmds = { "set_time_of_day 12", "set_time_of_day 1", "set_time_of_day 20" };
            int idx = simpleButton24.Tag != null ? (int)simpleButton24.Tag : 0;
            xbCon.CallString(0x8263cb10, new object[] { cmds[idx] });
            simpleButton24.Tag = (idx + 1) % cmds.Length;
        }
        private void simpleButton6_Click(object sender, EventArgs e)
        {
            xbCon.CallString(2187578128U, new object[] { textEdit1.Text });
        }
        private void simpleButton36_Click(object sender, EventArgs e)
        {
            if (comboBoxEdit1.Text == "Blunt")
            {
                xbCon.WriteBytes(0x827CF46F, new byte[]
                {
                    70
                });
            }
            if (comboBoxEdit1.Text == "Joint")
            {
                xbCon.WriteBytes(0x827CF46F, new byte[]
                {
                    66
                });
            }
            if (comboBoxEdit1.Text == "Beer")
            {
                xbCon.WriteBytes(0x827CF46F, new byte[]
                {
                    65
                });
            }
        }
        private void simpleButton28_Click(object sender, EventArgs e)
        {
            simpleButton28.ForeColor = Color.Green;
            xbCon.CallString(0x8263cb10, new object[]
            {
                "hood_explore_all"
            });
        }
        private void simpleButton29_Click(object sender, EventArgs e)
        {
            simpleButton29.ForeColor = Color.Green;
            xbCon.CallString(0x8263cb10, new object[]
            {
                "hood_win_all"
            });
        }
        private void simpleButton33_Click(object sender, EventArgs e)
        {
            simpleButton33.ForeColor = Color.Green;
            xbCon.CallString(0x8263cb10, new object[]
            {
                "activity_unlock_levels"
            });
        }
        private void simpleButton34_Click(object sender, EventArgs e)
        {
            simpleButton34.ForeColor = Color.Green;
            xbCon.CallString(0x8263cb10, new object[]
            {
                "cheats_unlock_all"
            });
        }
        private void simpleButton30_Click(object sender, EventArgs e)
        {
            xbCon.CallString(0x8263cb10, new object[]
            {
                "player_set_name " + textEdit2.Text
            });
        }
        #endregion
    }
}