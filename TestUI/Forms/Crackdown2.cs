using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.API.Native;
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
            try
            {
                ConnectToConsole2();
            }
            catch { }
        }
        public bool ConnectToConsole2()
        {
            if (activeConnection && xbCon.DebugTarget.IsDebuggerConnected(out debuggerName, out userName))
            {
                return true;
            }
            try
            {
                xbManager = (XboxManager)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("A5EB45D8-F3B6-49B9-984A-0D313AB60342")));
                xbCon = xbManager.OpenConsole(xbManager.DefaultConsole);
                ConnectionCode = xbCon.OpenConnection(null);
                xboxConnection = xbCon.OpenConnection(null);

                if (!xbCon.DebugTarget.IsDebuggerConnected(out debuggerName, out userName))
                {
                    xbCon.DebugTarget.ConnectAsDebugger("Xbox Toolbox", XboxDebugConnectFlags.Force);
                }

                activeConnection = xbCon.DebugTarget.IsDebuggerConnected(out debuggerName, out userName);
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
                xbCon.CallString(0x82771EB0, "fly");
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
                xbCon.CallString(0x82771EB0, "god");
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
                xbCon.CallString(0x82771EB0, "infiniteammo");
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
                xbCon.CallString(0x82771EB0, "fps");
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
                xbCon.CallString(0x82771EB0, "toggleoutlines");
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
                xbCon.CallString(0x82771EB0, textEdit1.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred", "Error");
            }
        }
    }
}