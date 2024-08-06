using DevExpress.XtraEditors;
using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using TestUI;
using XDevkit;
using XDRPC;

namespace DMONET3.Forms
{
    public partial class Halo3 : DevExpress.XtraEditors.XtraForm
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
        // Halo 3 stuff
        public bool AllMissions;
        public bool allskulls;
        public bool thirdperson;
        public bool gravity;

        public Halo3()
        {
            InitializeComponent();
        }

        private void Halo3_Load(object sender, EventArgs e)
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
        private byte[] GenerateByteArray(byte value, int length)
        {
            return Enumerable.Repeat(value, length).ToArray();
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            byte[] missionData = GenerateByteArray(AllMissions ? byte.MinValue : byte.MaxValue, 128);
            simpleButton1.ForeColor = AllMissions ? Color.Red : Color.Green;
            Form1.xbCon.WriteBytes(0x8319CBCB, missionData);
            AllMissions = !AllMissions;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            byte[] skullData = GenerateByteArray(allskulls ? byte.MinValue : byte.MaxValue, 7);
            simpleButton2.ForeColor = allskulls ? Color.Red : Color.Green;
            Form1.xbCon.WriteBytes(3256405026U, skullData);
            allskulls = !allskulls;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            simpleButton3.ForeColor = thirdperson ? Color.Red : Color.Green;
            byte[] thirdPersonData = thirdperson ? new byte[] { 0x4B, 0xF6, 0xF8, 0x7D } : new byte[] { 0x3C };
            Form1.xbCon.WriteBytes(0x8213A924, thirdPersonData);
            thirdperson = !thirdperson;
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            simpleButton4.ForeColor = gravity ? Color.Red : Color.Green;
            float gravityValue = gravity ? 4.1712594f : 192439f;
            Form1.xbCon.WriteFloat(2181258896U, gravityValue);
            gravity = !gravity;
        }
    }
}