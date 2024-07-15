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
        public INIEditor()
        {
            InitializeComponent();
        }

        private void INIEditor_Load(object sender, EventArgs e)
        {
            try
            {
                ConnectToConsole2();
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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            xbCon.ReceiveFile(AppDomain.CurrentDomain.BaseDirectory + this.textEdit1.Text, this.comboBoxEdit1.SelectedItem + this.textEdit1.Text);
            this.richTextBox2.Text = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + this.textEdit1.Text);
            bool flag2 = this.richTextBox2.Text.Contains("plugin1");
            if (flag2)
            {
                this.richTextBox2.SelectionStart = this.richTextBox2.Find("plugin1");
                this.richTextBox2.ScrollToCaret();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + this.textEdit1.Text, this.richTextBox2.Text);
            xbCon.SendFile(AppDomain.CurrentDomain.BaseDirectory + this.textEdit1.Text, this.comboBoxEdit1.Text + this.textEdit1.Text);
            bool flag2 = this.richTextBox2.Text.Contains("plugin1");
            if (flag2)
            {
                this.richTextBox2.SelectionStart = this.richTextBox2.Find("plugin1");
                this.richTextBox2.ScrollToCaret();
            }
        }
    }
}