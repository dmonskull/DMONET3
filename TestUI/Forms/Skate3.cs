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
        public Form1 form1;

        private bool backt;
        private bool FLIP;

        public Skate3()
        {
            InitializeComponent();
        }

        private void Skate3_Load(object sender, EventArgs e)
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
        public void WriteFloat4(uint Address, float Value)
        {
            byte[] bytes = BitConverter.GetBytes(Value);
            Array.Reverse(bytes);
            Form1.xbCon.WriteBytes(Address, bytes);
        }

        #region BasicMods
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                Form1.xbCon.WriteBytes(0x822F8B40, backt ? new byte[] { 0xA2 } : new byte[] { 0xF1 });
                if (backt)
                {
                    Thread.Sleep(500);
                    Form1.xbCon.WriteBytes(0x822F8B40, new byte[] { 0xC1 });
                }
                backt = !backt;
                simpleButton1.ForeColor = backt ? Color.Green : Color.Red;
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
                if (!FLIP)
                {
                    WriteFloat4(2184153920U, -90f);
                    WriteFloat4(2182565592U, 1250f);
                }
                else
                {
                    WriteFloat4(2184153920U, -9.8f);
                    WriteFloat4(2182565592U, 1456.35f);
                }
                FLIP = !FLIP;
                simpleButton9.ForeColor = FLIP ? Color.Green : Color.Red;
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred", "Error");
            }
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