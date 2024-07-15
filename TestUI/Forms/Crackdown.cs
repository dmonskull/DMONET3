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
using XDRPC;
using System.Diagnostics;

namespace TestUI.Forms
{
    public partial class Crackdown : DevExpress.XtraEditors.XtraForm
    {
        // connection stuff
        public IXboxManager xbManager = null;
        public IXboxConsole xbCon = null;
        public bool activeConnection = false;
        private uint ConnectionCode;
        public uint xboxConnection = 0;
        public string debuggerName = null;
        public string userName = null;
        // crackdown stuff
        private bool godmode = false;
        private bool infiniteammo = false;
        private bool fly = false;
        private bool fps = false;
        private bool speedycars = false;
        private bool superrun = false;
        private bool disablebullets = false;
        private bool widefov = false;
        private bool orbsize = false;
        public Crackdown()
        {
            InitializeComponent();
        }

        private void Crackdown_Load(object sender, EventArgs e)
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

        #region ButtonClicks
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!godmode)
                {
                    xbCon.CallString(0x825D4548, "god");
                    simpleButton1.Text = "Godmode: ON";
                }
                else
                {
                    xbCon.CallString(0x825D4548, "god");
                    simpleButton1.Text = "Godmode: OFF";
                }
                godmode = !godmode;
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred", "Error");
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            try
            {
                if (!infiniteammo)
                {
                    xbCon.CallString(0x825D4548, "infiniteammo");
                    simpleButton3.Text = "Infinite Ammo: ON";
                }
                else
                {
                    xbCon.CallString(0x825D4548, "infiniteammo");
                    simpleButton3.Text = "Infinite Ammo: OFF";
                }
                infiniteammo = !infiniteammo;
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred", "Error");
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!fly)
                {
                    xbCon.CallString(0x825D4548, "fly");
                    simpleButton2.Text = "Fly: ON";
                }
                else
                {
                    xbCon.CallString(0x825D4548, "fly");
                    simpleButton2.Text = "Fly: OFF";
                }
                fly = !fly;
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred", "Error");
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            try
            {
                if (!fps)
                {
                    xbCon.CallString(0x825D4548, "fps");
                    simpleButton4.Text = "FPS: ON";
                }
                else
                {
                    xbCon.CallString(0x825D4548, "fps");
                    simpleButton4.Text = "FPS: OFF";
                }
                fps = !fps;
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred", "Error");
            }
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            try
            {
                if (!speedycars)
                {
                    simpleButton6.Text = "Speedy Cars: ON";
                    xbCon.WriteBytes(0x820F634C, new byte[] { 0x42, 0xC6, 0x00, 0x00 });
                }
                else
                {
                    simpleButton6.Text = "Speedy Cars: OFF";
                    xbCon.WriteBytes(0x820F634C, new byte[] { 0xC2, 0x48, 0x00, 0x00 });
                }
                speedycars = !speedycars;
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred", "Error");
            }
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            try
            {
                if (!superrun)
                {
                    simpleButton9.Text = "Super Run: ON";
                    xbCon.WriteBytes(0x820F55F8, new byte[] { 0x3A });
                }
                else
                {
                    simpleButton9.Text = "Super Run: OFF";
                    xbCon.WriteBytes(0x820F55F8, new byte[] { 0x38 });
                }
                superrun = !superrun;
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred", "Error");
            }
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            try
            {
                if (!disablebullets)
                {
                    simpleButton8.Text = "Disable Bullets: ON";
                    xbCon.WriteBytes(0x82053B44, new byte[] { 0x3F, 0x80, 0x00, 0x00 });
                }
                else
                {
                    simpleButton8.Text = "Disable Bullets: OFF";
                    xbCon.WriteBytes(0x82053B44, new byte[] { 0x34, 0x00, 0x00, 0x00 });
                }
                disablebullets = !disablebullets;
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred", "Error");
            }
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            try
            {
                if (!widefov)
                {
                    simpleButton7.Text = "Wide FOV: ON";
                    xbCon.WriteBytes(0x82070A64, new byte[] { 0x40, 0x40, 0x00, 0x00 });
                }
                else
                {
                    simpleButton7.Text = "Wide FOV: OFF";
                    xbCon.WriteBytes(0x82070A64, new byte[] { 0x3F, 0x40, 0x00, 0x00 });
                }
                widefov = !widefov;
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred", "Error");
            }
        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {
            try
            {
                if (!orbsize)
                {
                    simpleButton10.Text = "XXL Orb Size: ON";
                    xbCon.WriteBytes(0x820F6380, new byte[] { 0x42, 0xC6, 0x00, 0x00 });
                }
                else
                {
                    simpleButton10.Text = "XXL Orb Size: OFF";
                    xbCon.WriteBytes(0x820F6380, new byte[] { 0x41, 0x7F, 0x00, 0x00 });
                }
                orbsize = !orbsize;
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred", "Error");
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            try
            {
                xbCon.CallString(0x825D4548, textEdit1.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred", "Error");
            }
        }

        private void hyperlinkLabelControl1_Click(object sender, EventArgs e)
        {
            // my post with list of console commands and standalone crackdown tool
            Process.Start("https://www.se7ensins.com/forums/threads/crackdown-mod-tool-console-commands-working.1838388/");
        }
        #endregion
    }
}