using DevExpress.XtraEditors;
using System;
using System.Runtime.InteropServices;
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
        public Crackdown2()
        {
            InitializeComponent();
        }

        private void Crackdown2_Load(object sender, EventArgs e)
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
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                Form1.xbCon.CallString(0x82771EB0, "fly");
                fly = !fly;
                simpleButton2.Text = fly ? "Fly: ON" : "Fly: OFF";
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
                Form1.xbCon.CallString(0x82771EB0, "god");
                godmode = !godmode;
                simpleButton3.Text = godmode ? "God: ON" : "God: OFF";
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
                Form1.xbCon.CallString(0x82771EB0, "infiniteammo");
                infiniteammo = !infiniteammo;
                simpleButton4.Text = infiniteammo ? "Infinite Ammo: ON" : "Infinite Ammo: OFF";
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
                Form1.xbCon.CallString(0x82771EB0, "fps");
                fps = !fps;
                simpleButton5.Text = fps ? "FPS: ON" : "FPS: OFF";
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
                Form1.xbCon.CallString(0x82771EB0, "toggleoutlines");
                toggleoutlines = !toggleoutlines;
                simpleButton6.Text = toggleoutlines ? "Outlines: OFF" : "Outlines: ON";
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred", "Error");
            }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                Form1.xbCon.CallString(0x82771EB0, textEdit1.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred", "Error");
            }
        }
    }
}