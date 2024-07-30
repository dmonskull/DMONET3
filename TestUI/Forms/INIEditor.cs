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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using XDevkit;
using XDRPC;
using System.IO;
using TestUI;

namespace DMONET3.Forms
{
    public partial class INIEditor : DevExpress.XtraEditors.XtraForm
    {
        public IXboxManager xbManager = null;
        public IXboxConsole xbCon = null;
        public bool activeConnection = false;
        private uint ConnectionCode;
        public uint xboxConnection = 0;
        public string debuggerName = null;
        public string userName = null;
        public Form1 form1;
        string iniFilePath = AppDomain.CurrentDomain.BaseDirectory + "INIs/RecievedFromConsole/";
        public INIEditor()
        {
            InitializeComponent();
        }

        private void INIEditor_Load(object sender, EventArgs e)
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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Form1.xbCon.ReceiveFile(iniFilePath + this.textEdit1.Text, this.comboBoxEdit1.SelectedItem + this.textEdit1.Text);
            this.richTextBox2.Text = File.ReadAllText(iniFilePath + this.textEdit1.Text);
            bool flag2 = this.richTextBox2.Text.Contains("plugin1");
            if (flag2)
            {
                this.richTextBox2.SelectionStart = this.richTextBox2.Find("plugin1");
                this.richTextBox2.ScrollToCaret();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            File.WriteAllText(iniFilePath + this.textEdit1.Text, this.richTextBox2.Text);
            Form1.xbCon.SendFile(iniFilePath + this.textEdit1.Text, this.comboBoxEdit1.Text + this.textEdit1.Text);
            bool flag2 = this.richTextBox2.Text.Contains("plugin1");
            if (flag2)
            {
                this.richTextBox2.SelectionStart = this.richTextBox2.Find("plugin1");
                this.richTextBox2.ScrollToCaret();
            }
        }
    }
}