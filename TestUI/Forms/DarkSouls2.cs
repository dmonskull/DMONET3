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
    public partial class DarkSouls2 : DevExpress.XtraEditors.XtraForm
    {
        // connection stuff
        public IXboxManager xbManager = null;
        public IXboxConsole xbCon = null;
        public bool activeConnection = false;
        private uint ConnectionCode;
        public uint xboxConnection = 0;
        public string debuggerName = null;
        public string userName = null;
        // Dark Souls 2 stuff
        public uint PlayerLevel = 3324845279U;
        public uint Souls = 3324845307U;
        public uint Vigor = 3324845077U;
        public uint Endurance = 3324845079U;
        public uint Vitality = 3324845081U;
        public uint Attunement = 3324845083U;
        public uint Strength = 3324845085U;
        public uint Dexterity = 3324845087U;
        public uint Intelligence = 3324845089U;
        public uint Adaptability = 3324845093U;
        public uint Faith = 3324845091U;
        public DarkSouls2()
        {
            InitializeComponent();
        }

        private void DarkSouls2_Load(object sender, EventArgs e)
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
        private void WriteBytesToMemory(NumericUpDown numericUpDown, uint address)
        {
            byte value = Convert.ToByte(numericUpDown.Value);
            byte[] bytes = BitConverter.GetBytes(value);
            xbCon.WriteBytes(address, bytes);
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            WriteBytesToMemory(numericUpDown1, PlayerLevel);
        }
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            WriteBytesToMemory(numericUpDown3, Vigor);
        }
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            WriteBytesToMemory(numericUpDown4, Endurance);
        }
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            WriteBytesToMemory(numericUpDown5, Vitality);
        }
        private void simpleButton11_Click(object sender, EventArgs e)
        {
            WriteBytesToMemory(numericUpDown11, Faith);
        }
        private void simpleButton6_Click(object sender, EventArgs e)
        {
            WriteBytesToMemory(numericUpDown6, Attunement);
        }
        private void simpleButton7_Click(object sender, EventArgs e)
        {
            WriteBytesToMemory(numericUpDown7, Strength);
        }
        private void simpleButton8_Click(object sender, EventArgs e)
        {
            WriteBytesToMemory(numericUpDown8, Dexterity);
        }
        private void simpleButton9_Click(object sender, EventArgs e)
        {
            WriteBytesToMemory(numericUpDown9, Intelligence);
        }
        private void simpleButton10_Click(object sender, EventArgs e)
        {
            WriteBytesToMemory(numericUpDown10, Adaptability);
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            xbCon.WriteBytes(0xC62D1CDD, new byte[] { 0xFF, 0xFF, 0xFF });
            xbCon.WriteBytes(0xC62D1CF8, new byte[] { 0x0F, 0xFF, 0xFF, 0xFF });
            xbCon.WriteBytes(0xC62D1C15, new byte[] { 0x63, 0x63, 0x63, 0x63, 0x63, 0x63, 0x63, 0x63 });
            xbCon.WriteBytes(0xC62D1C2B, new byte[] { 0x06, 0x63, 0x63, 0x63, 0x63, 0x63, 0x63, 0x63, 0x63, 0x63 });
        }
    }
}