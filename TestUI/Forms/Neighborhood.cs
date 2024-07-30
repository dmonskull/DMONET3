using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestUI;
using XDevkit;

namespace DMONET3.Forms
{
    public partial class Neighborhood : DevExpress.XtraEditors.XtraForm
    {
        public IXboxManager xbManager = null;
        public IXboxConsole xbCon = null;
        public bool activeConnection = false;
        private uint ConnectionCode;
        public uint xboxConnection = 0;
        public string debuggerName = null;
        public string userName = null;
        private string res;
        public Form1 form1;
        public Neighborhood()
        {
            InitializeComponent();
            xbManager = (XboxManager)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("A5EB45D8-F3B6-49B9-984A-0D313AB60342")));
            ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem("Connect");
            toolStripMenuItem.DropDownOpening += Item_MouseEnter;
            ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem();
            for (int i = 0; i <= xbManager.Consoles.Count - 1; i++)
            {
                toolStripMenuItem2 = new ToolStripMenuItem(xbManager.Consoles[i].ToString(), null, new EventHandler(Connect));
                toolStripMenuItem2.ForeColor = SystemColors.GradientActiveCaption;
                toolStripMenuItem2.BackColor = Color.FromArgb(70, 70, 70);
                toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
            }
            toolStripMenuItem.ForeColor = SystemColors.GradientActiveCaption;
            toolStripMenuItem.BackColor = Color.FromArgb(90, 90, 90);
            menuStrip1.Items.Add(toolStripMenuItem);
        }
        public void Connect(object sender, EventArgs e)
        {
            try
            {
                Form1.xbCon = xbManager.OpenConsole(sender.ToString());
                ConnectionCode = Form1.xbCon.OpenConnection(null);
            }
            catch (COMException ex)
            {
                MessageBox.Show(xbManager.TranslateError(ex.ErrorCode));
            }
        }
        private void Item_MouseEnter(object sender, EventArgs e)
        {
            ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)menuStrip1.Items[0];
            toolStripMenuItem.DropDownItems.Clear();
            ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem();
            for (int i = 0; i <= xbManager.Consoles.Count - 1; i++)
            {
                toolStripMenuItem2 = new ToolStripMenuItem(xbManager.Consoles[i].ToString(), null, new EventHandler(Connect));
                toolStripMenuItem2.ForeColor = SystemColors.GradientActiveCaption;
                toolStripMenuItem2.BackColor = Color.FromArgb(70, 70, 70);
                toolStripMenuItem.DropDownItems.Add(toolStripMenuItem2);
            }
            toolStripMenuItem.ForeColor = SystemColors.GradientActiveCaption;
            toolStripMenuItem.BackColor = Color.FromArgb(90, 90, 90);
            menuStrip1.Items.Insert(0, toolStripMenuItem);
        }

        private void buttonNoSidecar_Click(object sender, EventArgs e)
        {
            Form1.xbCon.SendTextCommand(ConnectionCode, "setcolor name=nosidecar", out res);
        }

        private void buttonBlack_Click(object sender, EventArgs e)
        {
            Form1.xbCon.SendTextCommand(ConnectionCode, "setcolor name=black", out res);
        }

        private void buttonBlue_Click(object sender, EventArgs e)
        {
            Form1.xbCon.SendTextCommand(ConnectionCode, "setcolor name=blue", out res);
        }

        private void buttonBlueGray_Click(object sender, EventArgs e)
        {
            Form1.xbCon.SendTextCommand(ConnectionCode, "setcolor name=bluegray", out res);
        }

        private void buttonWhite_Click(object sender, EventArgs e)
        {
            Form1.xbCon.SendTextCommand(ConnectionCode, "setcolor name=white", out res);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Form1.xbCon.SendTextCommand(ConnectionCode, "dbgname name=" + textEdit1.Text, out res);
        }
    }
}