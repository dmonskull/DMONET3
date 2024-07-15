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
using XDevkit;
using XDRPC;

namespace DMONET3.Forms
{
    public partial class ConsoleInfo : DevExpress.XtraEditors.XtraForm
    {
        // connection stuff
        public IXboxManager xbManager = null;
        public IXboxConsole xbCon = null;
        public bool activeConnection = false;
        private uint ConnectionCode;
        public uint xboxConnection = 0;
        public string debuggerName = null;
        public string userName = null;
        public ConsoleInfo()
        {
            InitializeComponent();
        }

        private void ConsoleInfo_Load(object sender, EventArgs e)
        {
            try
            {
                if (ConnectToConsole2());
                {
                    PullInfo();
                }
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
        static string ReverseIP(string ip)
        {
            string[] parts = ip.Split('.');
            Array.Reverse(parts);
            return string.Join(".", parts);
        }
        public void PullInfo()
        {
            string gamertag2 = Encoding.BigEndianUnicode.GetString(xbCon.ReadBytes(2175412476U, 30U)).Trim().Trim(new char[1]);
            gamertag.Text = Encoding.BigEndianUnicode.GetString(xbCon.ReadBytes(2175412476U, 30U)).Trim().Trim(new char[1]);
            CPUKeyText.Text = xbCon.GetCPUKey();
            IPText.Text = ReverseIP(xbCon.GetConsoleIP());
            KernalVersion.Text = "";
            ConsoleType.Text = xbCon.ConsoleType.ToString();
            pictureBox1.ImageLocation = "https://mygamerprofile.net/card/nxe/" + gamertag2 + ".png";
            pictureBox2.ImageLocation = "http://avatar.xboxlive.com/avatar/" + gamertag2 + "/avatar-body.png";
        }
    }
}