using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using XDevkit;
using XDRPC;

namespace TestUI.Forms
{
    public partial class Skate3 : DevExpress.XtraEditors.XtraForm
    {
        // connection stuff
        public IXboxManager xbManager = null;
        public IXboxConsole xbCon = null;
        public bool activeConnection = false;
        private uint ConnectionCode;
        public uint xboxConnection = 0;
        public string debuggerName = null;
        public string userName = null;

        private bool backt;
        private bool FLIP;

        public Skate3()
        {
            InitializeComponent();
        }

        private void Skate3_Load(object sender, EventArgs e)
        {
            try
            {
                if (ConnectToConsole2())
                {
                    barButtonItem1.Caption = "Reconnect";
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
        public void WriteFloat4(uint Address, float Value)
        {
            byte[] bytes = BitConverter.GetBytes(Value);
            Array.Reverse(bytes);
            xbCon.WriteBytes(Address, bytes);
        }

        #region BasicMods
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (!backt)
            {
                simpleButton1.ForeColor = Color.Green;
                xbCon.WriteBytes(0x822F8B40, new byte[] { 0xF1 });
            }
            else
            {
                simpleButton1.ForeColor = Color.Red;
                xbCon.WriteBytes(0x822F8B40, new byte[] { 0xA2 });
                Thread.Sleep(500);
                xbCon.WriteBytes(0x822F8B40, new byte[] { 0xC1 });
            }
            backt = !backt;
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            if (!FLIP)
            {
                simpleButton9.ForeColor = Color.Green;
                WriteFloat4(2184153920U, -90f);
                WriteFloat4(2182565592U, 1250f);
            }
            else
            {
                simpleButton9.ForeColor = Color.Red;
                WriteFloat4(2184153920U, -9.8f);
                WriteFloat4(2182565592U, 1456.35f);
            }
            FLIP = !FLIP;
        }
        #endregion

        #region CharacterEdits
        private void simpleButton10_Click(object sender, EventArgs e)
        {
            textEdit1.Text = "-9.8";
            textEdit2.Text = "19.6";
            textEdit3.Text = "1023.5";
            textEdit4.Text = "1456.35";
            textEdit5.Text = "255";
            textEdit6.Text = "512";
            textEdit7.Text = "0.01745329";
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            float value = float.Parse(textEdit1.Text);
            WriteFloat4(2184153920U, value);
            float value2 = float.Parse(textEdit2.Text);
            WriteFloat4(2184154064U, value2);
            float value3 = float.Parse(textEdit4.Text);
            WriteFloat4(2182565592U, value3);
            float value4 = float.Parse(textEdit3.Text);
            WriteFloat4(2182565596U, value4);
            float value5 = float.Parse(textEdit5.Text);
            WriteFloat4(2181812836U, value5);
            float value6 = float.Parse(textEdit6.Text);
            WriteFloat4(2184156672U, value6);
            float value7 = float.Parse(textEdit7.Text);
            WriteFloat4(2181484816U, value7);
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            float value = float.Parse(textEdit1.Text);
            WriteFloat4(2184153920U, value);
        }

        private void simpleButton11_Click(object sender, EventArgs e)
        {
            float value = float.Parse(textEdit2.Text);
            WriteFloat4(2184154064U, value);
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            float value = float.Parse(textEdit4.Text);
            WriteFloat4(2182565592U, value);
        }

        private void simpleButton12_Click(object sender, EventArgs e)
        {
            float value = float.Parse(textEdit3.Text);
            WriteFloat4(2182565596U, value);
        }

        private void simpleButton13_Click(object sender, EventArgs e)
        {
            float value = float.Parse(textEdit5.Text);
            WriteFloat4(2181812836U, value);
        }

        private void simpleButton14_Click(object sender, EventArgs e)
        {
            float value = float.Parse(textEdit6.Text);
            WriteFloat4(2184156672U, value);
        }

        private void simpleButton15_Click(object sender, EventArgs e)
        {
            float value = float.Parse(textEdit7.Text);
            WriteFloat4(2181484816U, value);
        }
        #endregion

    }
}