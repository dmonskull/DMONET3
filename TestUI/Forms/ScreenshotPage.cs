using DevExpress.XtraEditors;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TestUI;
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
        public Form1 form1;
        public ScreenshotPage()
        {
            InitializeComponent();
        }
        private void ScreenshotPage_Load(object sender, EventArgs e)
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
        public void TakeScreenshot()
        {
            var screenshotsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Screenshots");
            Directory.CreateDirectory(screenshotsFolder);
            var screenshotPath = Path.Combine(screenshotsFolder, $"Xbox360ScreenShot{screenshotCounter++}.bmp");
            Form1.xbCon.ScreenShot(screenshotPath);
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