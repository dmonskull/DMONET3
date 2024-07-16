using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XDevkit;
using XDRPC;

namespace DMONET3.Forms
{
    public partial class ScreenshotPage : DevExpress.XtraEditors.XtraForm
    {
        public IXboxManager xbManager = null;
        public IXboxConsole xbCon = null;
        public bool activeConnection = false;
        private uint ConnectionCode;
        public uint xboxConnection = 0;
        public string debuggerName = null;
        public string userName = null;
        private int screenshotCounter = 0;
        private string screenshotsFolder = Path.Combine(Application.StartupPath, "Screenshots");
        public ScreenshotPage()
        {
            InitializeComponent();
        }
        private void ScreenshotPage_Load(object sender, EventArgs e)
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
        public void TakeScreenshot()
        {
            var screenshotsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Screenshots");
            Directory.CreateDirectory(screenshotsFolder);
            var screenshotPath = Path.Combine(screenshotsFolder, $"Xbox360ScreenShot{screenshotCounter++}.bmp");
            xbCon.ScreenShot(screenshotPath);
            pictureEdit1.Image = Image.FromFile(screenshotPath);
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            TakeScreenshot();
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Path.Combine(Directory.GetCurrentDirectory(), "Screenshots"));
        }
    }
}