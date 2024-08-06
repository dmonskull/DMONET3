using DevExpress.XtraEditors;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
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
        public Form1 form1;
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

        #region ButtonClicks
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                Form1.xbCon.CallString(0x825D4548, "god");
                godmode = !godmode;
                simpleButton1.Text = godmode ? "Godmode: ON" : "Godmode: OFF";
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
                Form1.xbCon.CallString(0x825D4548, "infiniteammo");
                infiniteammo = !infiniteammo;
                simpleButton3.Text = infiniteammo ? "Infinite Ammo: ON" : "Infinite Ammo: OFF";
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
                Form1.xbCon.CallString(0x825D4548, "fly");
                fly = !fly;
                simpleButton2.Text = fly ? "Fly: ON" : "Fly: OFF";
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
                Form1.xbCon.CallString(0x825D4548, "fps");
                fps = !fps;
                simpleButton4.Text = fps ? "FPS: ON" : "FPS: OFF";
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
                Form1.xbCon.WriteBytes(0x820F634C, speedycars ? new byte[] { 0xC2, 0x48, 0x00, 0x00 } : new byte[] { 0x42, 0xC6, 0x00, 0x00 });
                speedycars = !speedycars;
                simpleButton6.Text = speedycars ? "Speedy Cars: ON" : "Speedy Cars: OFF";
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
                Form1.xbCon.WriteBytes(0x820F55F8, superrun ? new byte[] { 0x38 } : new byte[] { 0x3A });
                superrun = !superrun;
                simpleButton9.Text = superrun ? "Super Run: ON" : "Super Run: OFF";
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
                Form1.xbCon.WriteBytes(0x82053B44, disablebullets ? new byte[] { 0x34, 0x00, 0x00, 0x00 } : new byte[] { 0x3F, 0x80, 0x00, 0x00 });
                disablebullets = !disablebullets;
                simpleButton8.Text = disablebullets ? "Disable Bullets: ON" : "Disable Bullets: OFF";
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
                Form1.xbCon.WriteBytes(0x82070A64, widefov ? new byte[] { 0x3F, 0x40, 0x00, 0x00 } : new byte[] { 0x40, 0x40, 0x00, 0x00 });
                widefov = !widefov;
                simpleButton7.Text = widefov ? "Wide FOV: ON" : "Wide FOV: OFF";
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
                Form1.xbCon.WriteBytes(0x820F6380, orbsize ? new byte[] { 0x41, 0x7F, 0x00, 0x00 } : new byte[] { 0x42, 0xC6, 0x00, 0x00 });
                orbsize = !orbsize;
                simpleButton10.Text = orbsize ? "XXL Orb Size: ON" : "XXL Orb Size: OFF";
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
                Form1.xbCon.CallString(0x825D4548, textEdit1.Text);
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